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
	/// 保存一个汉字（包括多音字）的相关信息
	/// </summary>
	public class CharInfo
	{
		/// <summary>
		/// 汉字本身（其内容只会是一个汉字）
		/// </summary>
		public string hanzi { get; set; }
		
		/// <summary>
		/// 是否是多音字。 true: 是多音字;  false: 不是多音字
		/// </summary>
		public bool   multi { get; set; }
		
		/// <summary>
		/// 是否是繁体字（异休字）。 true: 是繁体字（异休字）;  false: 不是繁体字（异休字），也就是说，是普通的简体字
		/// </summary>
		public bool   trans { get; set; }
		
		/// <summary>
		/// 注释信息
		/// </summary>
		public string info { get; set; }
		
		/// <summary>
		/// 多单字对应的主音（其值与 pinyins 中的第一个值相同）
		/// </summary>
		public string main { get; set; }
		
		/// <summary>
		/// 汉字的发音。非多音字只会有一个记录； 多音字则会有多个发音，并且第一个记录就是该字的主音。
		/// </summary>
		public List<String> pinyins;
		
		/// <summary>
		/// 汉字的解释。多单字时，和 pinyins 一一对应; 单音字时，数组可能无元素（XML文件中没有记录该字的定义）
		/// </summary>
		public List<DefinitionInfo> definitions;
		
		/// <summary>
		/// 汉字对应的词组（这里只记录以本汉字开头的词组，而不是所有包括本汉字的词组）
		/// </summary>
		public List<PhraseInfo> phrases;
		
		public CharInfo()
		{
			pinyins = new List<string>();
			definitions = new List<DefinitionInfo>();
			phrases = new List<PhraseInfo>();
			multi = false;
			trans = false;
		}
	}
}
