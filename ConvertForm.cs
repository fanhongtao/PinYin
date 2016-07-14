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
using System.Windows.Forms;

namespace PinYin
{
	/// <summary>
	/// Description of ConvertForm.
	/// </summary>
	public partial class ConvertForm : Form
	{
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
					
					outputText.AppendText(ch);
				}
				outputText.AppendText("\n");
			}
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
						outputText.AppendText(phrase.hanzi.Substring(idx, 1));
						if (phrase.pinyin[idx].Length > 0) {
							outputText.AppendText("(" + phrase.pinyin[idx] + ")");
						}
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
						outputText.AppendText(phrase.hanzi.Substring(idx, 1));
						outputText.AppendText("(" + phrase.pinyin[idx] + ")");
					}
					return len;
				}
			}
			
			outputText.AppendText(ch);
			outputText.AppendText("(" + charInfo.pinyins[0] + ")");
			return 1;
		}
	}
}
