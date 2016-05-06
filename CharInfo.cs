/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/5/6
 * Time: 12:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace PinYin
{
	/// <summary>
	/// Description of CharInfo.
	/// </summary>
	public class CharInfo
	{
		public string hanzi { get; set; }  // 汉字
		public bool   multi { get; set; }  // 是否是多音字
		public bool   trans { get; set; }  // 是否是繁体字（异休字）
		public string info { get; set; }   // 注释信息
		public List<String> pinyins;       // 汉字的发音
		public List<PhraseInfo> phrases;   // 汉字对应的词组
		public CharInfo()
		{
			pinyins = new List<string>();
			phrases = new List<PhraseInfo>();
			multi = false;
			trans = false;
		}
	}
}
