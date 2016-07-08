﻿/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/7/7
 * Time: 14:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PinYin
{
	/// <summary>
	/// Description of ExtraInfo.
	/// </summary>
	public class ExtraInfo
	{
		string PHRASE = "phrase";
		string HZ = "hz";
		string PY = "py";
		
		private static ExtraInfo instance = new ExtraInfo();
		private Hashtable hashTable = new Hashtable();
		private List<PhraseInfo> tmpPhrases;   // 汉字对应的词组（仅在读取XML时临时存放）
		
		public static ExtraInfo Instance {
			get {
				return instance;
			}
		}
		
		private ExtraInfo()
		{
			tmpPhrases = new List<PhraseInfo>();
		}
		
		public void load()
		{
			DirectoryInfo dir = new DirectoryInfo("pinyin/extra");
			FileInfo[] files = dir.GetFiles("*.xml");
			foreach (FileInfo file in files) {
				XmlDocument doc = new XmlDocument();
				Logger.info("Load " + file.FullName);
				doc.Load(file.FullName);    //加载Xml文件
				XmlElement rootElem = doc.DocumentElement;   //获取根节点
				XmlNodeList charNodes = rootElem.GetElementsByTagName(PHRASE); //获取char子节点集合
				foreach (XmlNode node in charNodes)
				{
					addPhrase(node);
				}
			}
			adjustPhrase();
			Logger.info("Extra dir load finished.");
		}
		
		/// <summary>
		/// 添加一个词（Extra中的词，也可能是一个汉字，但处理方式不变）
		/// </summary>
		/// <param name="node"></param>
		private void addPhrase(XmlNode node)
		{
			XmlElement element = (XmlElement)node;
			string hz = element.GetAttribute(HZ);   //获取hz属性值
			string py = element.GetAttribute(PY);   //获取py属性值
			char[] separator = new char[] { ',' };
			
			PhraseInfo phraseInfo = new PhraseInfo();
			phraseInfo.hanzi = hz;
			string [] pinyin = py.Split(separator);
			if (pinyin.Length != hz.Length) {
				throw new InvalidDataException("Invalid pinyin, phrase [" + hz 
				                               + "]， pinyin [" + py + "]");
			}
			phraseInfo.pinyin = pinyin;
			tmpPhrases.Add(phraseInfo);
		}
		
		/// <summary>
		/// 在 extra 目录都读取完毕后，调整词组的位置，将所有词组都存放在该词组首个汉字的名下
		/// </summary>
		private void adjustPhrase()
		{
			CharInfo charInfo;
			foreach (PhraseInfo phraseInfo in tmpPhrases) {
				// 将词组存放在该词组首个汉字的名下
				string firstChar = phraseInfo.hanzi.Substring(0, 1);
				charInfo = (CharInfo) hashTable[firstChar];
				if (charInfo == null) {
					charInfo = new CharInfo();
					charInfo.hanzi = firstChar;
					hashTable.Add(firstChar, charInfo);
				}
				charInfo.phrases.Add(phraseInfo);
			}
			tmpPhrases = null; // 清除临时数据
		}
		
		public CharInfo getCharInfo(string ch)
		{
			CharInfo charInfo = (CharInfo) hashTable[ch];
			return charInfo;
		}
	}
}
