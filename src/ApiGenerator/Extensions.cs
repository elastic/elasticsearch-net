// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using CsQuery.ExtensionMethods.Internal;

namespace ApiGenerator
{
	public static class Extensions
	{
		/// <summary>
		/// Removes _ . but not an underscore at the start of the string, unless the string is _all or removeLeadingUnderscore == true.
		/// </summary>
		private static readonly Regex RemovePunctuationExceptFirstUnderScore = new Regex(@"(?!^_(?!All$))[_\.]");

		public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property) =>
			items.GroupBy(property).Select(x => x.First());

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

		public static string ToCamelCase(this string s)
		{
			if (string.IsNullOrEmpty(s)) return s;

			var pascal = s.ToPascalCase(true);
			if (pascal.Length <= 1) return pascal;

			return pascal[0].ToLower() + pascal.Substring(1);
		}

		public static string SplitPascalCase(this string s) =>
			Regex.Replace(s, "([A-Z]+[a-z]*)", " $1").Trim();
	}
}
