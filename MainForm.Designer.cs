/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/3/29
 * Time: 14:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PinYin
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.logGroupBox = new System.Windows.Forms.GroupBox();
			this.logTextBox = new System.Windows.Forms.RichTextBox();
			this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.mainTabControl = new System.Windows.Forms.TabControl();
			this.convertPage = new System.Windows.Forms.TabPage();
			this.characterPage = new System.Windows.Forms.TabPage();
			this.logGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
			this.mainSplitContainer.Panel1.SuspendLayout();
			this.mainSplitContainer.Panel2.SuspendLayout();
			this.mainSplitContainer.SuspendLayout();
			this.mainTabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// logGroupBox
			// 
			this.logGroupBox.Controls.Add(this.logTextBox);
			this.logGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logGroupBox.Location = new System.Drawing.Point(0, 0);
			this.logGroupBox.Name = "logGroupBox";
			this.logGroupBox.Size = new System.Drawing.Size(314, 61);
			this.logGroupBox.TabIndex = 0;
			this.logGroupBox.TabStop = false;
			this.logGroupBox.Text = "Log";
			// 
			// logTextBox
			// 
			this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logTextBox.Location = new System.Drawing.Point(3, 17);
			this.logTextBox.Name = "logTextBox";
			this.logTextBox.ReadOnly = true;
			this.logTextBox.Size = new System.Drawing.Size(308, 41);
			this.logTextBox.TabIndex = 0;
			this.logTextBox.Text = "";
			// 
			// mainSplitContainer
			// 
			this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.mainSplitContainer.Name = "mainSplitContainer";
			this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// mainSplitContainer.Panel1
			// 
			this.mainSplitContainer.Panel1.Controls.Add(this.mainTabControl);
			// 
			// mainSplitContainer.Panel2
			// 
			this.mainSplitContainer.Panel2.Controls.Add(this.logGroupBox);
			this.mainSplitContainer.Size = new System.Drawing.Size(314, 296);
			this.mainSplitContainer.SplitterDistance = 231;
			this.mainSplitContainer.TabIndex = 0;
			// 
			// mainTabControl
			// 
			this.mainTabControl.Controls.Add(this.convertPage);
			this.mainTabControl.Controls.Add(this.characterPage);
			this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabControl.Location = new System.Drawing.Point(0, 0);
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.SelectedIndex = 0;
			this.mainTabControl.Size = new System.Drawing.Size(314, 231);
			this.mainTabControl.TabIndex = 0;
			// 
			// convertPage
			// 
			this.convertPage.Location = new System.Drawing.Point(4, 22);
			this.convertPage.Name = "convertPage";
			this.convertPage.Padding = new System.Windows.Forms.Padding(3);
			this.convertPage.Size = new System.Drawing.Size(306, 205);
			this.convertPage.TabIndex = 0;
			this.convertPage.Text = "文本转换";
			this.convertPage.UseVisualStyleBackColor = true;
			// 
			// characterPage
			// 
			this.characterPage.Location = new System.Drawing.Point(4, 22);
			this.characterPage.Name = "characterPage";
			this.characterPage.Padding = new System.Windows.Forms.Padding(3);
			this.characterPage.Size = new System.Drawing.Size(306, 205);
			this.characterPage.TabIndex = 1;
			this.characterPage.Text = "汉字查询";
			this.characterPage.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(314, 296);
			this.Controls.Add(this.mainSplitContainer);
			this.Name = "MainForm";
			this.Text = "PinYin";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.logGroupBox.ResumeLayout(false);
			this.mainSplitContainer.Panel1.ResumeLayout(false);
			this.mainSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
			this.mainSplitContainer.ResumeLayout(false);
			this.mainTabControl.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.TabPage characterPage;
		private System.Windows.Forms.TabControl mainTabControl;
		private System.Windows.Forms.TabPage convertPage;
		private System.Windows.Forms.RichTextBox logTextBox;
		private System.Windows.Forms.GroupBox logGroupBox;
		private System.Windows.Forms.SplitContainer mainSplitContainer;
	}
}
