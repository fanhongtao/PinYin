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
			
			PinYinInfo info = PinYinInfo.Instance;
			for (int i = 0; i < line.Length; i++) {
				string ch = line.Substring(i, 1);
				CharInfo charInfo = info.getCharInfo(ch);
				if (charInfo == null) {
					continue;
				}
				showCharacter(charInfo);
			}
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
					showDefinition(charInfo.definitions[i]);
				}
				outputText.AppendText("\n");
			}
		}
		
		private void showDefinition(DefinitionInfo definition)
		{
			if (definition.id.Length != 0) {
				outputText.AppendText("  (" + definition.id + ")");
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
