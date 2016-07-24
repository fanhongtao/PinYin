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
			
			public Output(RichTextBox outputText, string outputFileName)
			{
				this.outputText = outputText;
				if (outputFileName != null) {
					Logger.info("Write to file: " + outputFileName);
					writer = new StreamWriter(outputFileName, true, Encoding.UTF8);
				}
			}
			
			public void AppendCharacter(string ch, string py)
			{
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
			}
			
			public void WriteLine()
			{
				outputText.AppendText("\n");
				if (writer != null) {
					writer.WriteLine();
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
		}
		
		void InputTextTextChanged(object sender, EventArgs e)
		{
			outputText.ResetText();
			
			output = new Output(outputText, _outputFileName);
			string[] lines = inputText.Lines;
			foreach (string line in lines) {
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
					if (phraseCheckBox.Checked) {
						string pinyin = string.Join(",", phrase.pinyin);
						Logger.debug("Match extra [" + phrase.hanzi + "] - (" + pinyin + ")");
					}
					for (int idx = 0; idx < len; idx++) {
						output.AppendCharacter(phrase.hanzi.Substring(idx, 1), phrase.pinyin[idx]);
					}
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
					if (phraseCheckBox.Checked) {
						string pinyin = string.Join(",", phrase.pinyin);
						Logger.debug("Match [" + phrase.hanzi + "] - (" + pinyin + ")");
					}
					for (int idx = 0; idx < len; idx++) {
						output.AppendCharacter(phrase.hanzi.Substring(idx, 1), phrase.pinyin[idx]);
					}
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
	}
}
