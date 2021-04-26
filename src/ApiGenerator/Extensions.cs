/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

		private static readonly Dictionary<string, string> ReservedNames = new() { { "namespace", "@namespace" } };

		public static string ReservedKeywordReplacer(this string name) => ReservedNames.ContainsKey(name) ? ReservedNames[name] : name;
		
		public static string SplitPascalCase(this string s) => Regex.Replace(s, "([A-Z]+[a-z]*)", " $1").Trim();
	}
}
