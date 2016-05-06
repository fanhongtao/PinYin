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
		}
		
		void InputTextTextChanged(object sender, EventArgs e)
		{
			outputText.ResetText();
			
			PinYinInfo info = PinYinInfo.Instance;
			string[] lines = inputText.Lines;
			foreach (string line in lines) {
				for (int i = 0; i < line.Length; i++) {
					string ch = line.Substring(i, 1);
					CharInfo charInfo = info.getCharInfo(ch);
					if (charInfo == null) {
						outputText.AppendText(ch);
						continue;
					}
					
					bool matchPhrase = false;  // 是否和某个词组匹配成功
					foreach (PhraseInfo phrase in charInfo.phrases) {
						int len = phrase.hanzi.Length;
						if (i + len > line.Length) { // 剩余字数少于词组的字数，直接跳过该词
							continue;
						}
						string temp = line.Substring(i, len);
						if (temp.Equals(phrase.hanzi)) {
							for (int idx = 0; idx < len; idx++) {
								outputText.AppendText(phrase.hanzi.Substring(idx, 1));
								outputText.AppendText("(" + phrase.pinyin[idx] + ")");
							}
							i = i + (len - 1); // 跳过已经匹配的字
							matchPhrase = true;
							break;
						}
					}
					
					if (!matchPhrase) {
						outputText.AppendText(ch);
						outputText.AppendText("(" + charInfo.pinyins[0] + ")");
					}
				}
				outputText.AppendText("\n");
			}
		}
	}
}
