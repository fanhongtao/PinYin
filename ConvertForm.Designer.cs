﻿/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/3/29
 * Time: 15:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PinYin
{
	partial class ConvertForm
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
			this.components = new System.ComponentModel.Container();
			this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.inputText = new System.Windows.Forms.RichTextBox();
			this.outputText = new System.Windows.Forms.RichTextBox();
			this.optionPanel = new System.Windows.Forms.Panel();
			this.pinyinCheckBox = new System.Windows.Forms.CheckBox();
			this.writeFileCheckBox = new System.Windows.Forms.CheckBox();
			this.extraCheckBox = new System.Windows.Forms.CheckBox();
			this.baseCheckBox = new System.Windows.Forms.CheckBox();
			this.phraseCheckBox = new System.Windows.Forms.CheckBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
			this.mainSplitContainer.Panel1.SuspendLayout();
			this.mainSplitContainer.Panel2.SuspendLayout();
			this.mainSplitContainer.SuspendLayout();
			this.optionPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainSplitContainer
			// 
			this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainSplitContainer.Location = new System.Drawing.Point(0, 30);
			this.mainSplitContainer.Name = "mainSplitContainer";
			// 
			// mainSplitContainer.Panel1
			// 
			this.mainSplitContainer.Panel1.Controls.Add(this.inputText);
			// 
			// mainSplitContainer.Panel2
			// 
			this.mainSplitContainer.Panel2.Controls.Add(this.outputText);
			this.mainSplitContainer.Size = new System.Drawing.Size(639, 231);
			this.mainSplitContainer.SplitterDistance = 293;
			this.mainSplitContainer.TabIndex = 0;
			// 
			// inputText
			// 
			this.inputText.AllowDrop = true;
			this.inputText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputText.Location = new System.Drawing.Point(0, 0);
			this.inputText.Name = "inputText";
			this.inputText.Size = new System.Drawing.Size(293, 231);
			this.inputText.TabIndex = 0;
			this.inputText.Text = "";
			this.inputText.DragDrop += new System.Windows.Forms.DragEventHandler(this.InputText_DragDrop);
			this.inputText.DragEnter += new System.Windows.Forms.DragEventHandler(this.InputText_DragEnter);
			this.inputText.TextChanged += new System.EventHandler(this.InputTextTextChanged);
			// 
			// outputText
			// 
			this.outputText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputText.Location = new System.Drawing.Point(0, 0);
			this.outputText.Name = "outputText";
			this.outputText.ReadOnly = true;
			this.outputText.Size = new System.Drawing.Size(342, 231);
			this.outputText.TabIndex = 0;
			this.outputText.Text = "";
			// 
			// optionPanel
			// 
			this.optionPanel.Controls.Add(this.pinyinCheckBox);
			this.optionPanel.Controls.Add(this.writeFileCheckBox);
			this.optionPanel.Controls.Add(this.extraCheckBox);
			this.optionPanel.Controls.Add(this.baseCheckBox);
			this.optionPanel.Controls.Add(this.phraseCheckBox);
			this.optionPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.optionPanel.Location = new System.Drawing.Point(0, 0);
			this.optionPanel.Name = "optionPanel";
			this.optionPanel.Size = new System.Drawing.Size(639, 30);
			this.optionPanel.TabIndex = 1;
			// 
			// pinyinCheckBox
			// 
			this.pinyinCheckBox.Location = new System.Drawing.Point(297, 3);
			this.pinyinCheckBox.Name = "pinyinCheckBox";
			this.pinyinCheckBox.Size = new System.Drawing.Size(125, 24);
			this.pinyinCheckBox.TabIndex = 5;
			this.pinyinCheckBox.Text = "拼音显示在字后面";
			this.toolTip.SetToolTip(this.pinyinCheckBox, "勾选时，拼音写在汉字后面；不勾选时，拼音写在汉字上方");
			this.pinyinCheckBox.UseVisualStyleBackColor = true;
			// 
			// writeFileCheckBox
			// 
			this.writeFileCheckBox.Location = new System.Drawing.Point(428, 3);
			this.writeFileCheckBox.Name = "writeFileCheckBox";
			this.writeFileCheckBox.Size = new System.Drawing.Size(104, 24);
			this.writeFileCheckBox.TabIndex = 6;
			this.writeFileCheckBox.Text = "写文件";
			this.toolTip.SetToolTip(this.writeFileCheckBox, "转换拖放的文件时，将转换结果写入文件。 写入的文件名为： 原文件名 + \"_zhuyin\" + 原文件后缀");
			this.writeFileCheckBox.UseVisualStyleBackColor = true;
			// 
			// extraCheckBox
			// 
			this.extraCheckBox.Location = new System.Drawing.Point(202, 3);
			this.extraCheckBox.Name = "extraCheckBox";
			this.extraCheckBox.Size = new System.Drawing.Size(104, 24);
			this.extraCheckBox.TabIndex = 4;
			this.extraCheckBox.Text = "使用扩展词库";
			this.toolTip.SetToolTip(this.extraCheckBox, "在转换过程中，使用 pinyin/extra 目录下记录的词进行匹配。如果是转换拖放的文件，还包括该文件对应的扩展词库文件中的内容。");
			this.extraCheckBox.UseVisualStyleBackColor = true;
			// 
			// baseCheckBox
			// 
			this.baseCheckBox.Location = new System.Drawing.Point(101, 3);
			this.baseCheckBox.Name = "baseCheckBox";
			this.baseCheckBox.Size = new System.Drawing.Size(104, 24);
			this.baseCheckBox.TabIndex = 3;
			this.baseCheckBox.Text = "使用基本词库";
			this.toolTip.SetToolTip(this.baseCheckBox, "在转换过程中，使用 pinyin/base 目录下记录的字、词进行匹配。");
			this.baseCheckBox.UseVisualStyleBackColor = true;
			// 
			// phraseCheckBox
			// 
			this.phraseCheckBox.Location = new System.Drawing.Point(0, 6);
			this.phraseCheckBox.Name = "phraseCheckBox";
			this.phraseCheckBox.Size = new System.Drawing.Size(106, 18);
			this.phraseCheckBox.TabIndex = 2;
			this.phraseCheckBox.Text = "显示匹配词组";
			this.toolTip.SetToolTip(this.phraseCheckBox, "在转换过程中，如果匹配到词库中的词组，是否需要在日志中记录匹配结果。");
			this.phraseCheckBox.UseVisualStyleBackColor = true;
			// 
			// ConvertForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(639, 261);
			this.Controls.Add(this.mainSplitContainer);
			this.Controls.Add(this.optionPanel);
			this.Name = "ConvertForm";
			this.Text = "ConvertForm";
			this.mainSplitContainer.Panel1.ResumeLayout(false);
			this.mainSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
			this.mainSplitContainer.ResumeLayout(false);
			this.optionPanel.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox pinyinCheckBox;
		private System.Windows.Forms.CheckBox writeFileCheckBox;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.CheckBox extraCheckBox;
		private System.Windows.Forms.CheckBox baseCheckBox;
		private System.Windows.Forms.CheckBox phraseCheckBox;
		private System.Windows.Forms.Panel optionPanel;
		private System.Windows.Forms.RichTextBox outputText;
		private System.Windows.Forms.RichTextBox inputText;
		private System.Windows.Forms.SplitContainer mainSplitContainer;
	}
}
