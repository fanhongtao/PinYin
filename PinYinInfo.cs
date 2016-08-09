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
	/// 读取并保存 pinyin/base 目录下的内容
	/// </summary>
	public sealed class PinYinInfo : AbstractPinYinInfo
	{
		const string CHAR = "char";
		const string PHRASE = "phrase";
		const string HZ = "hz";
		const string PY = "py";
		const string MULTI = "multi";
		const string MAIN = "main";
		const string TRANS = "trans";
		const string DEFINITION = "definition";
		const string ID = "id";
		const string ITEM = "item";
		const string TEXT = "text";
		
		static string[] TARGET_ID =  {"一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十"};
		static Dictionary<string, int> ID_MAP = new Dictionary<string, int>();
		static PinYinInfo () {
			for (int i = 0; i < TARGET_ID.Length; i++) {
				ID_MAP.Add(TARGET_ID[i], i + 1);
			}
		}
		
		private class ID_Comparer : IComparer<string>
		{
			public int Compare(string idX, string idY)
			{
				int x = ID_MAP[idX];
				int y = ID_MAP[idY];
				return x - y;
			}
		}
		
		private static PinYinInfo instance = new PinYinInfo();
		
		public static PinYinInfo Instance {
			get {
				return instance;
			}
		}
		
		private PinYinInfo()
		{
			hashTable = new Hashtable();
			allPhrases = new List<PhraseInfo>();
		}
		
		public void Load()
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
			XmlElement element = (XmlElement)node;
			string hz = element.GetAttribute(HZ);   //获取hz属性值
			string py = element.GetAttribute(PY);   //获取py属性值
			XmlNodeList phraseNodes = element.GetElementsByTagName(PHRASE); //获取可能的 phrase子节点集合
			XmlNodeList defineNodes = element.GetElementsByTagName(DEFINITION); // 获取可能的 definition 子节点（最多一个）
			DefinitionInfo definition = readDefinition(hz, defineNodes);
			CharInfo charInfo;
			
			// 不是多音字，直接构造一个CharInfo添加到 hashTable
			if (!element.HasAttribute(MULTI)) {
				charInfo = new CharInfo();
				charInfo.hanzi = hz;
				charInfo.multi = false;
				if (element.HasAttribute(TRANS)) {
					charInfo.trans = StringTools.ToBoolean(element.GetAttribute(TRANS));
				}
				charInfo.pinyins.Add(py);
				if ((definition != null) && (!definition.id.Equals(""))) {
					throw new InvalidDataException("Definition ID for character [" + hz + "] is useless.");
				}
				charInfo.definitions.Add(definition);
				addPhrase(charInfo, phraseNodes);
				hashTable.Add(hz, charInfo);
				return;
			}
			
			// 是多音字
			if (!hashTable.Contains(hz)) {
				charInfo = new CharInfo();
				charInfo.hanzi = hz;
				charInfo.multi = true;
				if (element.HasAttribute(TRANS)) {
					charInfo.trans = StringTools.ToBoolean(element.GetAttribute(TRANS));
				}
				hashTable.Add(hz, charInfo);
			} else {
				charInfo = (CharInfo)hashTable[hz];
				if (charInfo.multi == false) {
					throw new InvalidDataException("Character [" + hz + "] is not polyphone.");
				}
			}
			
			// 添加多音字的拼音和定义
			if (definition == null) {
				throw new InvalidDataException("No definition for character [" + hz + "] (" + py +").");
			}
			if (element.HasAttribute(MAIN)
			    && StringTools.ToBoolean(element.GetAttribute(MAIN))) {
				if (charInfo.main != null) {
					throw new InvalidDataException("More than one [" + hz + "] is set to main.");
				}
				charInfo.main = py;
				charInfo.pinyins.Insert(0, py);
				charInfo.definitions.Insert(0, definition);
			} else {
				charInfo.pinyins.Add(py);
				charInfo.definitions.Add(definition);
			}
			
			// 添加可能的词组
			addPhrase(charInfo, phraseNodes);
		}
		
		// 添加汉字对应的词组
		private void addPhrase(CharInfo charInfo, XmlNodeList phraseNodes)
		{
			if (phraseNodes == null) {
				return;
			}
			
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
				allPhrases.Add(phraseInfo);
			}
		}
		
		// 从XML中读取汉字对应的定义
		private DefinitionInfo readDefinition(String hanzi, XmlNodeList defineNodes)
		{
			if ((defineNodes == null) || (defineNodes.Count == 0)) {
				return null;
			}
			
			if (defineNodes.Count != 1) {
				throw new InvalidDataException("Multi definition for character [" + hanzi + "]");
			}
			
			XmlElement element =  (XmlElement)defineNodes[0];
			XmlNodeList itemList = element.GetElementsByTagName(ITEM);
			
			DefinitionInfo definition = new DefinitionInfo();
			definition.id = element.GetAttribute(ID);
			definition.text = element.GetAttribute(TEXT);
			definition.items = new ItemInfo[itemList.Count];
			for (int i = 0; i < itemList.Count; i++) {
				XmlNode node = itemList[i];
				ItemInfo itemInfo = new ItemInfo();
				itemInfo.id = ((XmlElement)node).GetAttribute(ID);
				itemInfo.text = ((XmlElement)node).GetAttribute(TEXT);
				definition.items[i] = itemInfo;
			}
			
			// 检查 Item ID 的是否合法
			//   1) 如果有ID，从1开始顺序编号。 
			int pos = 0;
			for (; pos < itemList.Count; pos++) {
				string itemId = definition.items[pos].id;
				if (itemId.Equals("")) {
					break;
				}
				if (!itemId.Equals((pos+1).ToString())) {
					throw new InvalidDataException("Bad item ID for [" + hanzi + "]， expect [" + (pos+1) + "], actual [" + itemId + "].");
				}
			}
			
			//   2) 如果没有ID，则其后不能再出现有ID的情况。
			for (; pos < itemList.Count; pos++) {
				string itemId = definition.items[pos].id;
				if (!itemId.Equals("")) {
					throw new InvalidDataException("Bad item ID for [" + hanzi + "]， ID is not expected at item [" + (pos+1) + "].");
				}
			}
			
			return definition;
		}
		
		// 在base目录都读取完毕后，调整词组的位置，将所有词组都存放在该词组首个汉字的名下
		private void adjustPhrase()
		{
			CharInfo charInfo;
			foreach (PhraseInfo phraseInfo in allPhrases) {
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
				AddPhrase(charInfo, phraseInfo);
			}
		}
		
		// 检查多音字
		private void checkPolyphone()
		{
			foreach (DictionaryEntry de in hashTable) {
				CharInfo charInfo = (CharInfo) de.Value;
				
				// 不是多音字，则不需要执行后继的判断
				if (!charInfo.multi) {
					continue;
				}
				
				// 是否明确指定主音（有一个字 main 属性为 true）
				if (charInfo.main == null) {
					throw new InvalidDataException("None of [" + charInfo.hanzi + "] is set to main.");
				}
				
				// 多音字的每个音是否都有定义，以及定义的ID是否正确：ID应从一开始连续编号
				if (charInfo.pinyins.Count != charInfo.definitions.Count) {
					throw new InvalidDataException("Count of pinyin and definition not match for [" + charInfo.hanzi + "].");
				}
				string [] ids = new string[charInfo.definitions.Count];
				for (int i = 0; i < charInfo.definitions.Count; i++) {
					ids[i] = charInfo.definitions[i].id;
				}
				Array.Sort(ids, new ID_Comparer());
				for (int i = 0; i < ids.Length; i++) {
					if (!ids[i].Equals(TARGET_ID[i])) {
						throw new InvalidDataException("Bad definition ID for [" + charInfo.hanzi + "]， expect [" + TARGET_ID[i] + "], actual [" + ids[i] + "].");
					}
				}
				
				// 对主音ID不是“一”的多音字进行提示
				if (!charInfo.definitions[0].id.Equals("一")) {
					Logger.debug("Character [" + charInfo.hanzi + "] has set ID (" + charInfo.definitions[0].id + ") as MAIN");
				}
			}
		}
	}
}
