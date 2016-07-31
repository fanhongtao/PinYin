/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/6/29
 * Time: 20:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
			outputText.ResetText();
			
			string line = inputComboBox.Text.Trim();
			if (line.Length == 0) {
				return;
			}
			
			// 将查询历史记录添加到 ComboBox
			inputComboBox.Items.Remove(line);
			inputComboBox.Items.Insert(0, line);
			inputComboBox.Text = line;
			inputComboBox.SelectionStart = inputComboBox.Text.Length;
			
			// 逐个显示每个字的信息
			PinYinInfo info = PinYinInfo.Instance;
			for (int i = 0; i < line.Length; i++) {
				string ch = line.Substring(i, 1);
				CharInfo charInfo = info.getCharInfo(ch);
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
			outputText.AppendText(charInfo.hanzi + "\n");
			for (int i = 0; i < charInfo.pinyins.Count; i++) {
				outputText.AppendText("\t" + charInfo.pinyins[i]);
				if (charInfo.definitions[i] == null) {
					outputText.AppendText("\n");
				} else {
					showDefinition(charInfo.definitions[i], charInfo.pinyins[i].Equals(charInfo.main));
				}
				outputText.AppendText("\n");
			}
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
	}
}
