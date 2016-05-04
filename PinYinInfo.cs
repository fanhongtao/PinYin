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
using System.IO;
using System.Xml;

namespace PinYin
{
	/// <summary>
	/// Description of PinYinInfo.
	/// </summary>
	public sealed class PinYinInfo
	{
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
				XmlNodeList personNodes = rootElem.GetElementsByTagName("char"); //获取char子节点集合
				foreach (XmlNode node in personNodes)
				{
					string hz = ((XmlElement)node).GetAttribute("hz");   //获取hz属性值
					string py = ((XmlElement)node).GetAttribute("py");   //获取py属性值
					if (!hashTable.Contains(hz)) {
						hashTable.Add(hz, py);
					}
				}
			}
		}
		
		public bool hasHanZi(string ch)
		{
			return hashTable.Contains(ch);
		}
		
		public string getPinYin(string ch)
		{
			return (string)hashTable[ch];
		}
	}
}
