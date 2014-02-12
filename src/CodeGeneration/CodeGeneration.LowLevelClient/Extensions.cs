using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CodeGeneration.LowLevelClient
{
	public static class Extensions
	{
		public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
		{
			return items.GroupBy(property).Select(x => x.First());
		}

		public static string ToPascalCase(this string s)
		{
			var textInfo = new CultureInfo("en-US").TextInfo;
			return textInfo.ToTitleCase(s.ToLowerInvariant()).Replace("_", string.Empty).Replace(".", string.Empty);
		}
	}
}