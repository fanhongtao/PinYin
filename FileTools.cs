/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/7/18
 * Time: 21:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace PinYin
{
	/// <summary>
	/// Description of FileTools.
	/// </summary>
	public static class FileTools
	{
		/// <summary>
		/// 判断一个文件是否是文本文件
		/// </summary>
		/// <param name="fileName">待判断的文件名</param>
		/// <returns>true: 是文本文件; false: 不是文本文件</returns>
		public static bool IsTextFile(string fileName)
		{
			FileStream fs = null;
			try {
				fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				bool isTextFile = true;
				int length = (int)fs.Length;
				byte data;
				for (int i = 0; i < length && isTextFile; i++) {
					data = (byte)fs.ReadByte();
					isTextFile = (data != 0);
				}
				return isTextFile;
			} catch (Exception ex) {
				throw ex;
			} finally {
				if (fs != null) {
					fs.Close();
				}
			}
		}
	}
}
