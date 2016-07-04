/*
 * Created by SharpDevelop.
 * User: fht
 * Date: 2016/5/5
 * Time: 15:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PinYin
{
	/// <summary>
	/// Description of StringTools.
	/// </summary>
	public static class StringTools
	{
		public static bool ToBoolean(this string value)
		{
			switch (value.ToLower())
			{
				case  "true":
					return true;
				case "t":
					return true;
				case "1":
					return true;
				case "0":
					return false;
				case "false":
					return false;
				case "f":
					return false;
				default:
					throw new InvalidCastException("Can't convert [" + value + "] to a bool!");
			}
		}
		
		/// <summary>
		/// 判断一个字符是否是有效的汉字
		/// </summary>
		/// <param name="ch">待判断的字符</param>
		/// <returns>true: 是有效的汉字（需要查询）; false: 不是有效的汉字（不需要查询）</returns>
		public static bool IsChineseCharacter(string ch)
		{
			char[] chArr = ch.ToCharArray();
			if (chArr.Length != 1) {
				return false;
			}
			
			// 中文汉字的unicode编码范围为4e00到9fee
			// 这里通过比较汉字的 unicode 判断是否是汉字
			if (chArr[0] < 0x4e00 || chArr[0] > 0x9fbb) {
				return false;
			} else {
				return true;
			}
		}
	}
}
