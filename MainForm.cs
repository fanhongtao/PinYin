/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/3/29
 * Time: 14:28
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
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			showForm(new ConvertForm(), convertPage);
			showForm(new CharacterForm(), characterPage);
			showForm(new ExportForm(), exportPage);
			
			Logger.SetTextBox(logTextBox);
			Logger.info("Program start.");
			
			PinYinInfo.Instance.Load();
			ExtraInfo.Instance.Load(null);
		}
		
		// 在TabPage中显示对应的Form
		private void showForm(Form form, System.Windows.Forms.Control tabPage)
		{
			form.TopLevel = false;
			form.Parent = tabPage;
			form.ControlBox = false;
			form.Dock = System.Windows.Forms.DockStyle.Fill;
			form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			form.Show();
		}
		
		void CleanLogButtonClick(object sender, EventArgs e)
		{
			Logger.clean();
		}
	}
}
