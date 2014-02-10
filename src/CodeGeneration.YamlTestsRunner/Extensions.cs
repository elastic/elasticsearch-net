using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeGeneration.YamlTestsRunner
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
			s = textInfo.ToTitleCase(s.ToLowerInvariant()).Replace("_", string.Empty).Replace(".", string.Empty);
			s = Regex.Replace(s, @"\W+", string.Empty);
			return s;
		}
	}
}