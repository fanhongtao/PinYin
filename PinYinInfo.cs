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
			XmlDocument doc = new XmlDocument();
			doc.Load("base.xml");    //加载Xml文件
			XmlElement rootElem = doc.DocumentElement;   //获取根节点
			XmlNodeList personNodes = rootElem.GetElementsByTagName("item"); //获取person子节点集合
			foreach (XmlNode node in personNodes)
			{
				string ch = ((XmlElement)node).GetAttribute("ch");   //获取ch属性值
				string py = ((XmlElement)node).GetAttribute("py");   //获取py属性值
				hashTable.Add(ch, py);
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
