/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/6/25
 * Time: 17:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PinYin
{
	/// <summary>
	/// Description of DefinitionInfo.
	/// </summary>
	public class DefinitionInfo
	{
		public string id { get; set; }     // 字的ID（编号）（非多音字，没有编号，字段值为""）
		public string text { get; set; }   // 字的通用描述（适用于所有 item）（也可能没有描述，，字段值为""）
		public ItemInfo[] items { get; set; }  // 具体的定义
	}
}
