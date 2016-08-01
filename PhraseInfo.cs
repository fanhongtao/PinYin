/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/5/6
 * Time: 12:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PinYin
{
	/// <summary>
	/// 记录一个词组.
	/// </summary>
	public class PhraseInfo
	{
		/// <summary>
		/// 词组对应的汉字（根据XML中的定义，词组可能只有一个汉字，也可能包括有标点符号等）
		/// </summary>
		public string hanzi { get; set; }
		
		/// <summary>
		/// <para>词组对应的拼音：</para>
		/// <para>1. 数组中的每个字符串对应一个汉字的拼音（根据XML中的定义，可能是空串""）</para>
		/// <para>2. 按照 hanzi 中汉字的顺序排列（hanzi中第一个汉字，对应数组中第一个拼音 …… 以此类推）</para>
		/// </summary>
		public string[] pinyin { get; set; }
	}
}
