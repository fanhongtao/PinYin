/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/3/29
 * Time: 15:48
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
	/// Description of PinYinInfo.
	/// </summary>
	public sealed class PinYinInfo
	{
		string CHAR = "char";
		string PHARSE = "pharse";
		string HZ = "hz";
		string PY = "py";
		string MULTI = "multi";
		string MAIN = "main";
		string TRANS = "trans";
		
		class PhraseInfo
		{
			public string hanzi { get; set; }   // 词组对应的汉字
			public string pinyin { get; set; }  // 词组对应的拼音
		}
		
		class CharInfo
		{
			public string hanzi { get; set; }  // 汉字
			public bool   multi { get; set; }  // 是否是多音字
			public bool   trans { get; set; }  // 是否是繁体字（异休字）
			public string info { get; set; }   // 注释信息
			public List<String> pinyins;       // 汉字的发音
			public List<PhraseInfo> pharses;   // 汉字对应的词组
			public CharInfo()
			{
				pinyins = new List<string>();
				pharses = null;
				multi = false;
				trans = false;
			}
		}
		
		private static PinYinInfo instance = new PinYinInfo();
		private Hashtable hashTable = new Hashtable();
		
		public static PinYinInfo Instance {
			get {
				return instance;
			}
		}
		
		private PinYinInfo()
		{
		}
		
		public void load()
		{
			loadBase();
		}
		
		// 加载  Base 目录
		private void loadBase()
		{
			DirectoryInfo dir = new DirectoryInfo("pinyin/base");
			FileInfo[] files = dir.GetFiles("*.xml");
			foreach (FileInfo file in files) {
				XmlDocument doc = new XmlDocument();
				Logger.info("Load " + file.FullName);
				doc.Load(file.FullName);    //加载Xml文件
				XmlElement rootElem = doc.DocumentElement;   //获取根节点
				XmlNodeList charNodes = rootElem.GetElementsByTagName(CHAR); //获取char子节点集合
				foreach (XmlNode node in charNodes)
				{
					string hz = ((XmlElement)node).GetAttribute(HZ);   //获取hz属性值
					string py = ((XmlElement)node).GetAttribute(PY);   //获取py属性值
					CharInfo charInfo;
					
					// 不是多音字，直接构造一个CharInfo添加到 hashTable
					if (!((XmlElement)node).HasAttribute(MULTI)) {
						charInfo = new CharInfo();
						charInfo.hanzi = hz;
						charInfo.pinyins.Add(py);
						charInfo.multi = false;
						if (((XmlElement)node).HasAttribute(TRANS)) {
							charInfo.trans = StringTools.ToBoolean(((XmlElement)node).GetAttribute(TRANS));
						}
						hashTable.Add(hz, charInfo);
						continue;
					}
					
					// 是多音字
					if (!hashTable.Contains(hz)) {
						charInfo = new CharInfo();
						charInfo.hanzi = hz;
						charInfo.multi = true;
						if (((XmlElement)node).HasAttribute(TRANS)) {
							charInfo.trans = StringTools.ToBoolean(((XmlElement)node).GetAttribute(TRANS));
						}
						hashTable.Add(hz, charInfo);
					} else {
						charInfo = (CharInfo)hashTable[hz];
					}
					
					// 添加多音字的拼音
					if (((XmlElement)node).HasAttribute(MAIN)
					    && StringTools.ToBoolean(((XmlElement)node).GetAttribute(MAIN))) {
						charInfo.pinyins.Insert(0, py);
					} else {
						charInfo.pinyins.Add(py);
					}
					
					// 添加多音字可能的词组
					XmlNodeList pharseNodes = ((XmlElement)node).GetElementsByTagName(PHARSE); //获取pharse子节点集合
					addPharse(charInfo, pharseNodes);
				}
			}
		}
		
		private void addPharse(CharInfo charInfo, XmlNodeList pharseNodes)
		{
			foreach (XmlNode node in pharseNodes) {
				string hz = ((XmlElement)node).GetAttribute(HZ);   //获取hz属性值
				string py = ((XmlElement)node).GetAttribute(PY);   //获取py属性值
				
				PhraseInfo pharseInfo = new PhraseInfo();
				pharseInfo.hanzi = hz;
				pharseInfo.pinyin = py;
				charInfo.pharses.Add(pharseInfo);
			}
		}
		
		public bool hasHanZi(string ch)
		{
			return hashTable.Contains(ch);
		}
		
		public string getPinYin(string ch)
		{
			CharInfo charInfo = (CharInfo) hashTable[ch];
			return charInfo.pinyins[0];
		}
	}
}
