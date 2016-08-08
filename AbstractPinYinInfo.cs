/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/8/1
 * Time: 15:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace PinYin
{
	/// <summary>
	/// 词库的基类.
	/// </summary>
	public class AbstractPinYinInfo
	{
		/// <summary>
		/// <para>读取的词库中的内容</para>
		/// <para>1. Hashtable的key 是单个汉字</para>
		/// <para>2. Hashtable的value 是 CharInfo 类型的数据</para>
		/// </summary>
		protected Hashtable hashTable;
		
		/// <summary>
		/// 保存所有的词组，供“汉字查询”界面显示使用
		/// </summary>
		protected List<PhraseInfo> allPhrases;
		
		public AbstractPinYinInfo()
		{
		}
		
		/// <summary>
		/// 根据一个汉字，获取词库中该汉字的信息
		/// </summary>
		/// <param name="ch">一个汉字</param>
		/// <returns>汉字对应的信息（如果词库中没有收录该汉字，则返回 null）</returns>
		public CharInfo GetCharInfo(string ch)
		{
			CharInfo charInfo = (CharInfo) hashTable[ch];
			return charInfo;
		}
		
		/// <summary>
		/// 返回包含某一个汉字的所有词组
		/// </summary>
		/// <param name="hanzi">待查询的汉字</param>
		/// <returns>包括该汉字的所有词组。如果该汉字没有词组，则返回一个没有元素的List</returns>
		public List<PhraseInfo> GetPhraseList(string hanzi)
		{
			List<PhraseInfo> list = new List<PhraseInfo>();
			foreach (PhraseInfo phraseInfo in allPhrases) {
				if (phraseInfo.hanzi.Contains(hanzi)) {
					list.Add(phraseInfo);
				}
			}
			return list;
		}
	}
}
