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
	}
}
