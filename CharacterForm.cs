/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/6/29
 * Time: 20:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PinYin
{
	/// <summary>
	/// Description of CharacterForm.
	/// </summary>
	public partial class CharacterForm : Form
	{
		public CharacterForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		private void InputComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
				inputCombox_Enter(sender, e);
            }
        }
		
		private void inputCombox_Enter(object sender, KeyEventArgs e)
		{
			// 清空outputText，但保留其ZoomFactor
			// 注意需要两次设置ZoomFactor，参考：
			// https://social.msdn.microsoft.com/Forums/windows/en-US/8b61eef0-b712-4b8b-9f5f-c9bbf75abb53/richtextbox-zoomfactor-problems?forum=winforms
			float factor = outputText.ZoomFactor;
			outputText.ResetText();
			outputText.ZoomFactor = 1f;
			outputText.ZoomFactor = factor;
			
			string line = inputComboBox.Text.Trim();
			if (line.Length == 0) {
				return;
			}
			
			// 将查询历史记录添加到 ComboBox
			inputComboBox.Items.Remove(line);
			inputComboBox.Items.Insert(0, line);
			inputComboBox.Text = line;
			inputComboBox.SelectionStart = inputComboBox.Text.Length;
			
			// 显示可能的词组
			tryShowPhrase(line);
			
			// 逐个显示每个字的信息
			PinYinInfo info = PinYinInfo.Instance;
			for (int i = 0; i < line.Length; i++) {
				string ch = line.Substring(i, 1);
				CharInfo charInfo = info.GetCharInfo(ch);
				if (charInfo == null) {
					if (StringTools.IsChineseCharacter(ch)) {
						// 找不到相应的字，给出相应的提示
						showMissCharacter(ch);
						Logger.error("Can't find character [" + ch + "]");
					}
					continue;
				}
				showCharacter(charInfo);
			}
		}
		
		// 试图将输入的内容当成一个词组来显示
		private void tryShowPhrase(string line)
		{
			PinYinInfo info = PinYinInfo.Instance;
			PhraseInfo phraseInfo = info.GetPhraseInfo(line);
			if (phraseInfo != null) {
				outputText.AppendText("已有词组： ");
				int start = outputText.Text.Length;
				outputText.AppendText(phraseInfo.hanzi + " - ");
				outputText.AppendText(string.Join(",", phraseInfo.pinyin));
				outputText.AppendText("\n");
				outputText.Select(start, outputText.Text.Length - start);
				outputText.SelectionFont = new Font("宋体", 20F, FontStyle.Italic, GraphicsUnit.Point);
				outputText.AppendText("\n");
			}
		}
		
		// 显示找不到字的相关信息
		private void showMissCharacter(string ch)
		{
			int start = outputText.Text.Length;
			outputText.AppendText(ch + "\t---- 没有收录此字\n");
			outputText.Select(start, outputText.Text.Length - start);
			outputText.SelectionColor = Color.Red;
		}
		
		// 显示一个字的解释
		private void showCharacter(CharInfo charInfo)
		{
			outputText.AppendText(charInfo.hanzi);
			if (charInfo.suppressLog) {
				outputText.AppendText("\t(单独匹配时不提示)");
			}
			outputText.AppendText("\n");
			for (int i = 0; i < charInfo.pinyins.Count; i++) {
				outputText.AppendText("\t" + charInfo.pinyins[i]);
				if (charInfo.definitions[i] == null) {
					outputText.AppendText("\n");
				} else {
					showDefinition(charInfo.definitions[i], charInfo.pinyins[i].Equals(charInfo.main));
				}
				outputText.AppendText("\n");
			}
			showPhrase("基本词库", PinYinInfo.Instance.GetPhraseList(charInfo.hanzi));
			showPhrase("扩展词库", ExtraInfo.Instance.GetPhraseList(charInfo.hanzi));
		}
		
		// 显示一个字的某个定义
		private void showDefinition(DefinitionInfo definition, bool main)
		{
			if (definition.id.Length != 0) {
				outputText.AppendText("  (" + definition.id + ")");
				if (main) {
					outputText.AppendText(" (主音) ");
				}
			}
			outputText.AppendText("\n");
			for (int i = 0; i < definition.items.Length; i++) {
				outputText.AppendText("\t\t");
				if (!definition.items[i].id.Equals("")) {
					outputText.AppendText(definition.items[i].id + " ");
				}
				outputText.AppendText(definition.items[i].text + "\n");
			}
		}
		
		/// <summary>
		/// 显示词组列表
		/// </summary>
		/// <param name="title">列表的标题</param>
		/// <param name="list">所要显示的词组列表</param>
		private void showPhrase(string title, List<PhraseInfo> list)
		{
			if (list.Count > 0) {
				outputText.AppendText("\t" + title + "：\n");
				foreach (PhraseInfo phraseInfo in list) {
					outputText.AppendText("\t\t" + phraseInfo.hanzi + " - ");
					outputText.AppendText(string.Join(",", phraseInfo.pinyin));
					outputText.AppendText("\n");
				}
				outputText.AppendText("\n");
			}
		}
	}
}
