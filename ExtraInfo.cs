/*
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
	/// 读取并保存 pinyin/extra 目录下的内容 及 所拖放文件对应的 extra 文件中的内容
	/// </summary>
	public class ExtraInfo
	{
		const string PHRASE = "phrase";
		const string HZ = "hz";
		const string PY = "py";
		
		private static ExtraInfo instance = new ExtraInfo();
		
		/// <summary>
		/// <para>读取的 pinyin/extra 目录下的内容 及 所拖放文件对应的 extra 文件中的内容</para>
		/// <para>1. Hashtable的key 是单个汉字</para>
		/// <para>2. Hashtable的value 是 CharInfo 类型的数据</para>
		/// <para>3. CharInfo 数据只有 hanzi 和 phrase 两个值是有效值</para>
		/// </summary>
		private Hashtable hashTable = new Hashtable();
		
		/// <summary>
		/// 保存所有的词组，供“汉字查询”界面显示使用
		/// </summary>
		private List<PhraseInfo> allPhrases;
		
		/// <summary>
		/// 最后一次读取的（拖放文件对应的） extra 文件的完整文件名
		/// </summary>
		private string lastExtraFile = "";
		
		public static ExtraInfo Instance {
			get {
				return instance;
			}
		}
		
		public void load(string inputExtraFileName)
		{
			if (string.Equals(lastExtraFile, inputExtraFileName)) {
				// Logger.debug("Skip loading extra.");
				return;
			}
			lastExtraFile = inputExtraFileName;
			
			hashTable = new Hashtable();
			allPhrases = new List<PhraseInfo>();
			DirectoryInfo dir = new DirectoryInfo("pinyin/extra");
			FileInfo[] files = dir.GetFiles("*.xml");
			foreach (FileInfo file in files) {
				loadOneFile(file.FullName);
			}
			if (inputExtraFileName != null) {
				loadOneFile(inputExtraFileName);
			}
			adjustPhrase();
			Logger.info("Extra dir load finished.");
		}
		
		private void loadOneFile(string fileName)
		{
			XmlDocument doc = new XmlDocument();
			Logger.info("Load " + fileName);
			doc.Load(fileName);    //加载Xml文件
			XmlElement rootElem = doc.DocumentElement;   //获取根节点
			XmlNodeList charNodes = rootElem.GetElementsByTagName(PHRASE); //获取char子节点集合
			foreach (XmlNode node in charNodes)
			{
				addPhrase(node);
			}
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
			allPhrases.Add(phraseInfo);
		}
		
		/// <summary>
		/// 在 extra 目录都读取完毕后，调整词组的位置，将所有词组都存放在该词组首个汉字的名下
		/// </summary>
		private void adjustPhrase()
		{
			CharInfo charInfo;
			foreach (PhraseInfo phraseInfo in allPhrases) {
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
		}
		
		public CharInfo getCharInfo(string ch)
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
