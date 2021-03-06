﻿/*
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
			this.logPanel = new System.Windows.Forms.Panel();
			this.cleanLogButton = new System.Windows.Forms.Button();
			this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.mainTabControl = new System.Windows.Forms.TabControl();
			this.convertPage = new System.Windows.Forms.TabPage();
			this.characterPage = new System.Windows.Forms.TabPage();
			this.exportPage = new System.Windows.Forms.TabPage();
			this.logGroupBox.SuspendLayout();
			this.logPanel.SuspendLayout();
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
			this.logGroupBox.Controls.Add(this.logPanel);
			this.logGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logGroupBox.Location = new System.Drawing.Point(0, 0);
			this.logGroupBox.Name = "logGroupBox";
			this.logGroupBox.Size = new System.Drawing.Size(350, 74);
			this.logGroupBox.TabIndex = 0;
			this.logGroupBox.TabStop = false;
			this.logGroupBox.Text = "Log";
			// 
			// logTextBox
			// 
			this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logTextBox.Location = new System.Drawing.Point(3, 42);
			this.logTextBox.Name = "logTextBox";
			this.logTextBox.ReadOnly = true;
			this.logTextBox.Size = new System.Drawing.Size(344, 29);
			this.logTextBox.TabIndex = 0;
			this.logTextBox.Text = "";
			// 
			// logPanel
			// 
			this.logPanel.Controls.Add(this.cleanLogButton);
			this.logPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.logPanel.Location = new System.Drawing.Point(3, 17);
			this.logPanel.Name = "logPanel";
			this.logPanel.Size = new System.Drawing.Size(344, 25);
			this.logPanel.TabIndex = 1;
			// 
			// cleanLogButton
			// 
			this.cleanLogButton.Location = new System.Drawing.Point(1, 3);
			this.cleanLogButton.Name = "cleanLogButton";
			this.cleanLogButton.Size = new System.Drawing.Size(75, 23);
			this.cleanLogButton.TabIndex = 0;
			this.cleanLogButton.Text = "清除日志";
			this.cleanLogButton.UseVisualStyleBackColor = true;
			this.cleanLogButton.Click += new System.EventHandler(this.CleanLogButtonClick);
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
			this.mainSplitContainer.Size = new System.Drawing.Size(350, 351);
			this.mainSplitContainer.SplitterDistance = 273;
			this.mainSplitContainer.TabIndex = 0;
			// 
			// mainTabControl
			// 
			this.mainTabControl.Controls.Add(this.convertPage);
			this.mainTabControl.Controls.Add(this.characterPage);
			this.mainTabControl.Controls.Add(this.exportPage);
			this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabControl.Location = new System.Drawing.Point(0, 0);
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.SelectedIndex = 0;
			this.mainTabControl.Size = new System.Drawing.Size(350, 273);
			this.mainTabControl.TabIndex = 0;
			// 
			// convertPage
			// 
			this.convertPage.Location = new System.Drawing.Point(4, 22);
			this.convertPage.Name = "convertPage";
			this.convertPage.Padding = new System.Windows.Forms.Padding(3);
			this.convertPage.Size = new System.Drawing.Size(342, 247);
			this.convertPage.TabIndex = 0;
			this.convertPage.Text = "文本转换";
			this.convertPage.UseVisualStyleBackColor = true;
			// 
			// characterPage
			// 
			this.characterPage.Location = new System.Drawing.Point(4, 22);
			this.characterPage.Name = "characterPage";
			this.characterPage.Padding = new System.Windows.Forms.Padding(3);
			this.characterPage.Size = new System.Drawing.Size(342, 247);
			this.characterPage.TabIndex = 1;
			this.characterPage.Text = "汉字查询";
			this.characterPage.UseVisualStyleBackColor = true;
			// 
			// exportPage
			// 
			this.exportPage.Location = new System.Drawing.Point(4, 22);
			this.exportPage.Name = "exportPage";
			this.exportPage.Padding = new System.Windows.Forms.Padding(3);
			this.exportPage.Size = new System.Drawing.Size(342, 247);
			this.exportPage.TabIndex = 2;
			this.exportPage.Text = "导出";
			this.exportPage.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(350, 351);
			this.Controls.Add(this.mainSplitContainer);
			this.Name = "MainForm";
			this.Text = "PinYin";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.logGroupBox.ResumeLayout(false);
			this.logPanel.ResumeLayout(false);
			this.mainSplitContainer.Panel1.ResumeLayout(false);
			this.mainSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
			this.mainSplitContainer.ResumeLayout(false);
			this.mainTabControl.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.TabPage exportPage;
		private System.Windows.Forms.Button cleanLogButton;
		private System.Windows.Forms.Panel logPanel;
		private System.Windows.Forms.TabPage characterPage;
		private System.Windows.Forms.TabControl mainTabControl;
		private System.Windows.Forms.TabPage convertPage;
		private System.Windows.Forms.RichTextBox logTextBox;
		private System.Windows.Forms.GroupBox logGroupBox;
		private System.Windows.Forms.SplitContainer mainSplitContainer;
	}
}
