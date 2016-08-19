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
using System.IO;

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
		
		/// <summary>
		/// 根据输入的字符串，获取对应的词组信息
		/// </summary>
		/// <param name="str">等查询的字符串</param>
		/// <returns>如果该字符串是一个完整的词，则返回对应的词组信息。 如果不是，则返回 null</returns>
		public PhraseInfo GetPhraseInfo(string str)
		{
			PhraseInfo ret = null;
			foreach (PhraseInfo phraseInfo in allPhrases) {
				if (phraseInfo.hanzi.Equals(str)) {
					ret = phraseInfo;
					break;
				}
			}
			return ret;
		}
		
		/// <summary>
		/// 向字符中添加一个词组：
		/// <para>  1. 添加前，会对重复的词组进行过滤</para>
		/// <para>  2. 添加时，会按照词组的长度降序排序（长的词在前面）</para>
		/// </summary>
		/// <param name="charInfo">词组中第一个汉字的信息</param>
		/// <param name="phraseInfo">待添加的词组</param>
		protected void AddPhrase(CharInfo charInfo, PhraseInfo phraseInfo)
		{
			// 检查是否和有的词组重复
			foreach (PhraseInfo old in charInfo.phrases) {
				if (old.hanzi.Equals(phraseInfo.hanzi)) {
					string oldPinyin = string.Join(",", old.pinyin);
					string newPinyin = string.Join(",", phraseInfo.pinyin);
					if (oldPinyin.Equals(newPinyin)) {
						return;  // 汉字和拼音都相同，则不加入，直接返回
					} else {
						throw new InvalidDataException("Different pinyin for [" + phraseInfo.hanzi + "], old (" + oldPinyin + "), new (" + newPinyin + ")");
					}
				}
			}
			
			// 按词组的长度排序（长的在前面，短的在后面）
			int idx = 0;
			for (; idx < charInfo.phrases.Count; idx++) {
				if (phraseInfo.hanzi.Length >= charInfo.phrases[idx].hanzi.Length) {
					break;
				}
			}
			charInfo.phrases.Insert(idx, phraseInfo);
		}
	}
}
