/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/3/29
 * Time: 15:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PinYin
{
	/// <summary>
	/// Description of ConvertForm.
	/// </summary>
	public partial class ConvertForm : Form
	{
		private string _outputFileName = null;
		private Output output = null;
		class Output
		{
			private RichTextBox outputText;
			private StreamWriter writer = null;
			/// <summary>
			/// 拼音是否写在汉字后面。 true: 拼音写在汉字后面;  false: 拼音写在汉字上方。
			/// </summary>
			private bool appendPinyin;
			private StringBuilder pyBuilder = new StringBuilder(10240);
			private StringBuilder hzBuilder = new StringBuilder(10240);
			/// <summary>
			/// 是否已经写了&lt;ruby&gt;标签。 true: 已经写过&lt;ruby&gt;标签;  false: 没有写过
			/// </summary>
			private bool writeRuby = false;
			/// <summary>
			/// 是否已经找到了输入文件中的&lt;body&gt;标签。 true: 已找到;  false: 没有找到
			/// </summary>
			public bool findBody { get; set; }
			
			private string prevHanzi = null;
			private string prevPinyin = null;
			
			public Output(RichTextBox outputText, string outputFileName, bool appendPinyin)
			{
				this.outputText = outputText;
				if (outputFileName != null) {
					Logger.info("Write to file: " + outputFileName);
					writer = new StreamWriter(outputFileName, false, Encoding.UTF8);
				}
				this.appendPinyin = appendPinyin;
				findBody = false;
			}
			
			/// <summary>
			/// 输出匹配的词组
			/// </summary>
			/// <param name="phrase">匹配的词组</param>
			/// <param name="writeLog">是否需要在日志中记录</param>
			/// <param name="isExtra">是否是 extra 中的词组</param>
			public void AppendPhrase(PhraseInfo phrase, bool writeLog, bool isExtra)
			{
				if (writeLog) {
					string pinyin = string.Join(",", phrase.pinyin);
					Logger.debug((isExtra ? "Match extra" : "Match") + " [" + phrase.hanzi + "] - (" + pinyin + ")");
				}
				if (prevHanzi != null) {
					CheckPrevHanzi(phrase.hanzi.Substring(0, 1), phrase.pinyin[0]);
				}
				for (int idx = 0, len = phrase.hanzi.Length; idx < len; idx++) {
					Append(phrase.hanzi.Substring(idx, 1), phrase.pinyin[idx]);
				}
			}
			
			public void AppendCharacter(string ch, string py)
			{
				if (prevHanzi != null) {
					CheckPrevHanzi(ch, py);
				}
				
				if (ch.Equals("不")) {
					prevHanzi = ch;
					prevPinyin = py;
				} else {
					Append(ch, py);
				}
			}
			
			/// <summary>
			/// 处理之前没有明确定下音调的汉字
			/// </summary>
			/// <param name="ch">当前处理的汉字</param>
			/// <param name="py">当前处理的汉字对应的拼音</param>
			private void CheckPrevHanzi(string ch, string py)
			{
				if (prevHanzi.Equals("不")) {
					int tone = StringTools.GetTone(py);
					if (tone == 4) {
						if (!prevPinyin.Equals("")) {
							Append("不", "bú");
						}
					} else {
						Append(prevHanzi, prevPinyin);
					}
				} else {
					Logger.error("Unsupported hanzi " + prevHanzi);
				}
				
				prevHanzi = null;
				prevPinyin = null;
			}
			
			private void Append(string ch, string py)
			{
				if (appendPinyin) {
					outputText.AppendText(ch);
					if (py.Length != 0) {
						outputText.AppendText("(" + py + ")");
					}
					if (writer != null) {
						writer.Write(ch);
						if (py.Length != 0) {
							writer.Write("(" + py + ")");
						}
					}
				} else {
					// 当拼音写在汉字上方时，写在文本框里的内容和写在文件中的内容是两套。
					// 文本框里的是自己进行简单拼接方式写出的
					if (py.Length != 0) {
						pyBuilder.Append(py);
						pyBuilder.Append(' ');
					} else {
						pyBuilder.Append(' ');
					}
					hzBuilder.Append(ch);
					
					// 写在文件中的内容则时默认是HTML文件，这里利用了<ruby>标签来实现将拼音写在汉字上方 
					if (writer != null) {
						if (!findBody) {
							writer.Write(ch);  // HTML文件中<body>之前的汉字，不需要加注拼音
						} else {
							if (py.Length != 0) {
								if (!writeRuby) {
									writer.Write("<ruby>");
									writeRuby = true;
								}
								writer.Write(ch);
								writer.Write("<rt>" + py + " </rt>");
							} else {
								if (writeRuby) {
									writer.Write("</ruby>");
									writeRuby = false;
								}
								writer.Write(ch);
							}
						}
					}
				}
			}
			
			public void WriteLine()
			{
				if (appendPinyin) {
					outputText.AppendText("\n");
					if (writer != null) {
						writer.WriteLine();
					}
				} else {
					outputText.AppendText(pyBuilder.ToString());
					outputText.AppendText("\n");
					outputText.AppendText(hzBuilder.ToString());
					outputText.AppendText("\n");
					pyBuilder.Clear();
					hzBuilder.Clear();
					
					if (writer != null) {
						if (writeRuby) {
							writer.Write("</ruby>");
							writeRuby = false;
						}
						writer.WriteLine();
					}
				}
			}
			
			public void Clean()
			{
				if (writer != null) {
					writer.Flush();
					writer.Close();
					writer.Dispose();
				}
			}
		}
		
		public ConvertForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			baseCheckBox.Checked = true;
			pinyinCheckBox.Checked = true;
			ShowHelpMessage();
		}
		
		void InputTextTextChanged(object sender, EventArgs e)
		{
			outputText.ResetText();
			
			output = new Output(outputText, _outputFileName, pinyinCheckBox.Checked);
			string[] lines = inputText.Lines;
			foreach (string line in lines) {
				if (!output.findBody && line.ToLower().StartsWith("<body>")) {
					output.findBody = true;
				}
				
				for (int i = 0; i < line.Length; i++) {
					string ch = line.Substring(i, 1);

					if (extraCheckBox.Checked) {
						int matchLen = matchExtra(line, i, ch);
						if (matchLen > 0) {
							i = i + (matchLen - 1); // 跳过已经匹配的字
							continue;
						}
					}
					
					if (baseCheckBox.Checked) {
						int matchLen = matchBase(line, i, ch);
						if (matchLen > 0) {
							i = i + (matchLen - 1); // 跳过已经匹配的字
							continue;
						}
						if (StringTools.IsChineseCharacter(ch)) {
							// 找不到相应的字，给出相应的提示
							Logger.error("Can't find character [" + ch + "]");
						}
					}
					
					output.AppendCharacter(ch, "");
				}
				output.WriteLine();
			}
			
			_outputFileName = null;
			output.Clean();
			output = null;
		}
		
		/// <summary>
		/// 尝试根据 extra 目录的内容来匹配
		/// </summary>
		/// <param name="line">当前要处理的行</param>
		/// <param name="i">当前要处理的字在该行中的位置</param>
		/// <param name="ch">当前要处理的字</param>
		/// <returns>匹配的字的个数（0表示没有匹配成功）</returns>
		private int matchExtra(string line, int i, string ch)
		{
			ExtraInfo extraInfo = ExtraInfo.Instance;
			CharInfo charInfo = extraInfo.getCharInfo(ch);
			if (charInfo == null) {
				return 0;
			}
			
			// 匹配对应的词
			foreach (PhraseInfo phrase in charInfo.phrases) {
				int len = phrase.hanzi.Length;
				if (i + len > line.Length) { // 剩余字数少于词组的字数，直接跳过该词
					continue;
				}
				
				string temp = line.Substring(i, len);
				if (temp.Equals(phrase.hanzi)) {
					output.AppendPhrase(phrase, phraseCheckBox.Checked, true);
					return len;
				}
			}
			
			return 0;
		}
		
		/// <summary>
		/// 尝试根据 base 目录的内容来匹配
		/// </summary>
		/// <param name="line">当前要处理的行</param>
		/// <param name="i">当前要处理的字在该行中的位置</param>
		/// <param name="ch">当前要处理的字</param>
		/// <returns>匹配的字的个数（0表示没有匹配成功）</returns>
		private int matchBase(string line, int i, string ch)
		{
			PinYinInfo baseInfo = PinYinInfo.Instance;
			CharInfo charInfo = baseInfo.getCharInfo(ch);
			if (charInfo == null) {
				return 0;
			}
			
			foreach (PhraseInfo phrase in charInfo.phrases) {
				int len = phrase.hanzi.Length;
				if (i + len > line.Length) { // 剩余字数少于词组的字数，直接跳过该词
					continue;
				}
				
				string temp = line.Substring(i, len);
				if (temp.Equals(phrase.hanzi)) {
					output.AppendPhrase(phrase, phraseCheckBox.Checked, false);
					return len;
				}
			}
			
			output.AppendCharacter(ch, charInfo.pinyins[0]);
			return 1;
		}
		
		private void InputText_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Link; // 表明是链接类型的数据，比如文件路径
			} else {
				e.Effect = DragDropEffects.None;
			}
		}
		
		private void InputText_DragDrop(object sender, DragEventArgs e)
		{
			string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			if (!File.Exists(path)) {
				Logger.error("File not exist: " + path);
				return;
			}
			if (!FileTools.IsTextFile(path)) {
				Logger.error("Not a text file: " + path);
				return;
			}
			
			if (writeFileCheckBox.Checked) {
				_outputFileName = getOutputFileName(path);
			}
			Logger.info("Open file: " + path);
			StreamReader reader = new StreamReader(path);
			inputText.ResetText();
			string line;
			while ((line = reader.ReadLine()) != null) {
				inputText.AppendText(line);
				inputText.AppendText("\n");
			}
			reader.Close();
			reader.Dispose();
			
			string inputExtraFileName = getInputExtraFileName(path);
			if (!File.Exists(inputExtraFileName)) {
				inputExtraFileName = null;
			}
			ExtraInfo.Instance.load(inputExtraFileName);
		}
		
		/// <summary>
		/// 根据输入的文件名，生成输出的文件名
		/// </summary>
		/// <param name="inputFileName">输入的文件名</param>
		/// <returns>输出的文件名</returns>
		private string getOutputFileName(string inputFileName)
		{
			string outputFileName = null;
			if (inputFileName != null) {
				string path = Path.GetDirectoryName(inputFileName);
				string name = Path.GetFileNameWithoutExtension(inputFileName);
				string ext = Path.GetExtension(inputFileName);
				outputFileName = path + Path.DirectorySeparatorChar + name + "_convert" + ext;
			}
			return outputFileName;
		}
		
		/// <summary>
		/// 根据输入的文件名，生成该文件对应的 extra 文件名
		/// </summary>
		/// <param name="inputFileName">输入的文件名</param>
		/// <returns>对应的 extra 文件名</returns>
		private string getInputExtraFileName(string inputFileName)
		{
			string inputExtraFileName = null;
			if (inputFileName != null) {
				string path = Path.GetDirectoryName(inputFileName);
				string name = Path.GetFileNameWithoutExtension(inputFileName);
				inputExtraFileName = path + Path.DirectorySeparatorChar + name + "_extra.xml";
			}
			return inputExtraFileName;
		}
		
		/// <summary>
		/// （程序启动时）在 outputText 中显示程序的帮助信息 
		/// </summary>
		private void ShowHelpMessage()
		{
			outputText.AppendText("拼音转换器 V1.0\n");
			outputText.AppendText("\n");
			
			outputText.AppendText("在左边的文本框中输入需要转换的文字，或者将一个文本文件（如 .txt, .html）拖放到左边的文本框，即可在本处显示转换后的结果。\n");
			outputText.AppendText("\n");
			
			outputText.AppendText("词库说明：\n");
			outputText.AppendText("* 基本词库：根据《新华字典》 整理而成，后又补充了一些常见的词语。\n");
			outputText.AppendText("* 扩展词库：扩展词库分为两部分\n");
			outputText.AppendText("\t其一，存放在 pinyin/extra 目录的内容，主要考虑存放一些专有名词，可根据学科、宗教等进行划分。\n");
			outputText.AppendText("\t其二，如果是转换拖放的文件，还会试图在文件所在目录下，读取与该文件匹配的扩展词库文件。\n");
			outputText.AppendText("\t\t文件名格式： 拖放的文件名 + \"_extra.xml\"。\n");
			outputText.AppendText("\t\t这个文件主要存放所转换文件中出现的一些特殊的注音。参考 demo 目录下的例子。\n");
			outputText.AppendText("* 转换过程中，如果有勾选“使用扩展词库”，则会优先根据扩展词库中的词来判断。\n");
			outputText.AppendText("\n");
			
			outputText.AppendText("关于“写文件”功能的说明：\n");
			outputText.AppendText("* 只有在左边的文本框中拖放文件时，才支持写文件，并且写入的文件名是固定的（见“写文件”的Tips）。\n");
			outputText.AppendText("* 根据是否勾选“拼音显示在字后面”，会影响写入的内容：。\n");
			outputText.AppendText("\t勾选，则写入文件的内容与在此文本框中显示的内容完全相同。\n");
			outputText.AppendText("\t不勾选，则写文件时，程序认为拖放的是一个HTML文件，仅对其 <body> 标签后面的内容进行转换。\n");
			outputText.AppendText("\t\t而文本框中的内容则是当作普通文件来转换，没有处理对齐，结果仅供参考。\n");
			outputText.AppendText("\n");
			
			outputText.AppendText("如果对程序有什么建议、意见、或Bug反馈，可发送邮件至 fanhongtao@gmail.com , 记得在标题中带上关键字“拼音”。");
		}
	}
}
