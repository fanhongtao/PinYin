/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/6/25
 * Time: 17:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PinYin
{
	/// <summary>
	/// 汉字定义中的某一个具体解释
	/// </summary>
	public class ItemInfo
	{
		/// <summary>
		/// 字的释意的ID（编号）
		/// </summary>
		public string id { get; set; }
		
		/// <summary>
		/// 字的释意的具体描述
		/// </summary>
		public string text { get; set; } 
	}
}
