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
	/// Description of PhraseInfo.
	/// </summary>
	public class PhraseInfo
	{
		public string hanzi { get; set; }   // 词组对应的汉字
		public string[] pinyin { get; set; }  // 词组对应的拼音
	}
}
