/*
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
			this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.inputText = new System.Windows.Forms.RichTextBox();
			this.outputText = new System.Windows.Forms.RichTextBox();
			this.optionPanel = new System.Windows.Forms.Panel();
			this.phraseCheckBox = new System.Windows.Forms.CheckBox();
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
			this.mainSplitContainer.Size = new System.Drawing.Size(284, 231);
			this.mainSplitContainer.SplitterDistance = 131;
			this.mainSplitContainer.TabIndex = 0;
			// 
			// inputText
			// 
			this.inputText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputText.Location = new System.Drawing.Point(0, 0);
			this.inputText.Name = "inputText";
			this.inputText.Size = new System.Drawing.Size(131, 231);
			this.inputText.TabIndex = 0;
			this.inputText.Text = "";
			this.inputText.TextChanged += new System.EventHandler(this.InputTextTextChanged);
			// 
			// outputText
			// 
			this.outputText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputText.Location = new System.Drawing.Point(0, 0);
			this.outputText.Name = "outputText";
			this.outputText.ReadOnly = true;
			this.outputText.Size = new System.Drawing.Size(149, 231);
			this.outputText.TabIndex = 0;
			this.outputText.Text = "";
			// 
			// optionPanel
			// 
			this.optionPanel.Controls.Add(this.phraseCheckBox);
			this.optionPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.optionPanel.Location = new System.Drawing.Point(0, 0);
			this.optionPanel.Name = "optionPanel";
			this.optionPanel.Size = new System.Drawing.Size(284, 30);
			this.optionPanel.TabIndex = 1;
			// 
			// phraseCheckBox
			// 
			this.phraseCheckBox.Location = new System.Drawing.Point(0, 6);
			this.phraseCheckBox.Name = "phraseCheckBox";
			this.phraseCheckBox.Size = new System.Drawing.Size(106, 18);
			this.phraseCheckBox.TabIndex = 2;
			this.phraseCheckBox.Text = "显示匹配词组";
			this.phraseCheckBox.UseVisualStyleBackColor = true;
			// 
			// ConvertForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
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
		private System.Windows.Forms.CheckBox phraseCheckBox;
		private System.Windows.Forms.Panel optionPanel;
		private System.Windows.Forms.RichTextBox outputText;
		private System.Windows.Forms.RichTextBox inputText;
		private System.Windows.Forms.SplitContainer mainSplitContainer;
	}
}
