/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2018/3/13
 * Time: 9:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PinYin
{
	/// <summary>
	/// Description of ExportForm.
	/// </summary>
	public partial class ExportForm : Form
	{
		public ExportForm()
		{
			InitializeComponent();
		}
		
		/// <summary>
		/// <br>导出 汉字/拼音 对。</br>
		/// <br>导出</br>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void HzpyBtnClick(object sender, EventArgs e)
		{
			PinYinInfo baseInfo = PinYinInfo.Instance;
			List<string> charList = baseInfo.GetAllChar();
			var hzMap = new Dictionary<string, string>();
			foreach (string hz in charList) {
				if (hz.Length != 1) { // 过滤 Missing 的汉字
					continue;
				}
				string py = baseInfo.GetCharInfo(hz).pinyins[0];
				hzMap.Add(hz, py);
			}
			
			// 排序，确保每次导出的内容顺序一致（方便比较导出的文件）
			var orderMap = hzMap.OrderBy(i => i.Value).ThenBy(i => i.Key);
			
			using (StreamWriter writer = new StreamWriter("export_char_py.txt", false, new UTF8Encoding(false)))
			{
				StringBuilder sb = new StringBuilder(64);
				foreach (var kv in orderMap) {
					sb.Clear();
					if (codeBox.Checked) {
						sb.Append("{\"");
						sb.Append(kv.Key);
						sb.Append("\", \"");
						sb.Append(kv.Value);
						sb.Append("\"},");
					} else {
						sb.Append(kv.Key);
						sb.Append("\t");
						sb.Append(kv.Value);
					}
					writer.WriteLine(sb.ToString());
				}
			}
			
			Logger.info("Export finished");
		}
	}
}
