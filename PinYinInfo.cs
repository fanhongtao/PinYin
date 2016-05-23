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
		string PHRASE = "phrase";
		string HZ = "hz";
		string PY = "py";
		string MULTI = "multi";
		string MAIN = "main";
		string TRANS = "trans";
		
		private static PinYinInfo instance = new PinYinInfo();
		private Hashtable hashTable = new Hashtable();
		private List<PhraseInfo> tmpPhrases;   // 汉字对应的词组（仅在读取XML时临时存放）
		
		public static PinYinInfo Instance {
			get {
				return instance;
			}
		}
		
		private PinYinInfo()
		{
			tmpPhrases = new List<PhraseInfo>();
		}
		
		public void load()
		{
			loadBase();
			Logger.info("Load finished.");
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
					addCharacter(node);
				}
			}
			adjustPhrase();
			checkPolyphone();
		}
		
		// 添加一个汉字
		private void addCharacter(XmlNode node)
		{
			string hz = ((XmlElement)node).GetAttribute(HZ);   //获取hz属性值
			string py = ((XmlElement)node).GetAttribute(PY);   //获取py属性值
			CharInfo charInfo;
			
			// 不是多音字，直接构造一个CharInfo添加到 hashTable
			if (!((XmlElement)node).HasAttribute(MULTI)) {
				charInfo = new CharInfo();
				charInfo.hanzi = hz;
				charInfo.multi = false;
				if (((XmlElement)node).HasAttribute(TRANS)) {
					charInfo.trans = StringTools.ToBoolean(((XmlElement)node).GetAttribute(TRANS));
				}
				charInfo.pinyins.Add(py);
				hashTable.Add(hz, charInfo);
				return;
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
				if (charInfo.main != null) {
					throw new InvalidDataException("More than one [" + hz + "] is set to main.");
				}
				charInfo.main = py;
				charInfo.pinyins.Insert(0, py);
			} else {
				charInfo.pinyins.Add(py);
			}
			
			// 添加多音字可能的词组
			XmlNodeList phraseNodes = ((XmlElement)node).GetElementsByTagName(PHRASE); //获取phrase子节点集合
			if (phraseNodes != null) {
				addPhrase(charInfo, phraseNodes);
			}
		}
		
		// 添加汉字对应的词组
		private void addPhrase(CharInfo charInfo, XmlNodeList phraseNodes)
		{
			char[] separator = new char[] { ',' };
			foreach (XmlNode node in phraseNodes) {
				string hz = ((XmlElement)node).GetAttribute(HZ);   //获取hz属性值
				string py = ((XmlElement)node).GetAttribute(PY);   //获取py属性值
				
				PhraseInfo phraseInfo = new PhraseInfo();
				phraseInfo.hanzi = hz;
				string [] pinyin = py.Split(separator);
				if (pinyin.Length != hz.Length) {
					throw new InvalidDataException("Invalid pinyin for character [" + charInfo.hanzi 
					                               + "]， phrase [" + hz 
					                               + "]， pinyin [" + py + "]");
				}
				phraseInfo.pinyin = pinyin;
				tmpPhrases.Add(phraseInfo);
			}
		}
		
		// 在base目录都读取完毕后，调整词组的位置，将所有词组都存放在该词组首个汉字的名下
		private void adjustPhrase()
		{
			CharInfo charInfo;
			foreach (PhraseInfo phraseInfo in tmpPhrases) {
				// 检查词组中的字是否都已经存在
				for (int i = 0; i < phraseInfo.hanzi.Length; i++) {
					string character = phraseInfo.hanzi.Substring(i, 1);
					charInfo = (CharInfo) hashTable[character];
					if (charInfo == null) {
						throw new InvalidDataException("Can't find character [" + character
						                               + "] for phrase [" + phraseInfo.hanzi + "]");
					}
				}
				
				// 将词组存放在该词组首个汉字的名下
				string firstChar = phraseInfo.hanzi.Substring(0, 1);
				charInfo = (CharInfo) hashTable[firstChar];
				charInfo.phrases.Add(phraseInfo);
			}
			tmpPhrases = null; // 清除临时数据
		}
		
		// 检查多音字是否明确指定主音（有一个字 main 属性为 true）
		private void checkPolyphone()
		{
			foreach (DictionaryEntry de in hashTable) {
				CharInfo charInfo = (CharInfo) de.Value;
				if (charInfo.multi && (charInfo.main == null)) {
					throw new InvalidDataException("None of [" + charInfo.hanzi + "] is set to main.");
				}
			}
		}
		
		public CharInfo getCharInfo(string ch)
		{
			CharInfo charInfo = (CharInfo) hashTable[ch];
			return charInfo;
		}
	}
}
