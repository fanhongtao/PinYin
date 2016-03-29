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
					outputText.AppendText(ch);
					if (info.hasHanZi(ch)) {
						outputText.AppendText("(" + info.getPinYin(ch) + ")");
					}
				}
			}
		}
	}
}
