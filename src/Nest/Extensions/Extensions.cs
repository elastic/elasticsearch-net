using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public static class Extensions
	{
		public static bool JsonEquals(this string json, string otherjson)
		{
			var nJson = JObject.Parse(json).ToString();
			var nOtherJson = JObject.Parse(otherjson).ToString();
			return nJson == nOtherJson;
		}


		public static void ThrowIfNull<T>(this T value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}
		public static void ThrowIfNullOrEmpty(this string value, string name)
		{
			if (value.IsNullOrEmpty())
			{
				throw new ArgumentNullException(name);
			}
		}
		public static string F(this string format, params object[] args)
		{
			format.ThrowIfNull("format");
			return string.Format(format, args);
		}
		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}
		public static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> handler)
		{
			int idx = 0;
			foreach (T item in enumerable)
				handler(item, idx++);
		}
	}

	
	
}
