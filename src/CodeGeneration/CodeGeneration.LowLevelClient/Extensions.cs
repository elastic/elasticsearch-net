using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeGeneration.LowLevelClient
{
	public static class Extensions
	{
		public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
		{
			return items.GroupBy(property).Select(x => x.First());
		}

		/// <summary>
		/// Removes _ . but not an underscore at the start of the string, unless the string is _all or removeLeadingUnderscore == true.
		/// </summary>
		private static readonly Regex RemovePunctuationExceptFirstUnderScore = new Regex(@"(?!^_(?!All$))[_\.]");
		public static string ToPascalCase(this string s, bool removeLeadingUnderscore = false)
		{
			if (string.IsNullOrEmpty(s)) return s;
			var textInfo = new CultureInfo("en-US").TextInfo;
			var titleCased = textInfo.ToTitleCase(s.ToLowerInvariant());
			var result = RemovePunctuationExceptFirstUnderScore.Replace(titleCased, "");
			if (removeLeadingUnderscore)
				result = result.TrimStart('_');
			return result;
		}
	}
}