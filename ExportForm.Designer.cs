/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2018/3/13
 * Time: 9:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PinYin
{
	partial class ExportForm
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
			this.hzpyBtn = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.codeBox = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// hzpyBtn
			// 
			this.hzpyBtn.Location = new System.Drawing.Point(156, 16);
			this.hzpyBtn.Name = "hzpyBtn";
			this.hzpyBtn.Size = new System.Drawing.Size(119, 30);
			this.hzpyBtn.TabIndex = 0;
			this.hzpyBtn.Text = "导出汉字/拼音对";
			this.hzpyBtn.UseVisualStyleBackColor = true;
			this.hzpyBtn.Click += new System.EventHandler(this.HzpyBtnClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.codeBox);
			this.groupBox1.Controls.Add(this.hzpyBtn);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(281, 58);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "导出文件可用于简单的类库";
			// 
			// codeBox
			// 
			this.codeBox.Location = new System.Drawing.Point(6, 20);
			this.codeBox.Name = "codeBox";
			this.codeBox.Size = new System.Drawing.Size(116, 24);
			this.codeBox.TabIndex = 2;
			this.codeBox.Text = "代码格式";
			this.codeBox.UseVisualStyleBackColor = true;
			// 
			// ExportForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(448, 232);
			this.Controls.Add(this.groupBox1);
			this.Name = "ExportForm";
			this.Text = "ExportForm";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox codeBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button hzpyBtn;
	}
}
