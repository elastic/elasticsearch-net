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

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExamplesGenerator {
	internal static class StringExtensions
	{
		private static readonly Regex DoubleQuotes = new Regex("\"");

		private static readonly Regex NewLine = new Regex("\r?\n");

		private static readonly Regex LowercasePascal = new Regex(@"\b([a-z])");

		private static readonly Regex Callout = new Regex(@"//[ \t]*(?<callout>\<\d+\>)[ \t]*(?<text>\S.*)");

		private static readonly Regex CalloutReplacer = new Regex(@"//[ \t]*\<(\d+)\>.*");

		public static string EscapeDoubleQuotes(this string input) => DoubleQuotes.Replace(input, "\"\"");

		public static string Indent(this string input, string indent) => NewLine.Replace(input, "\n" + indent);

		public static string LowercaseHyphenUnderscoreToPascal(this string input) =>
			LowercasePascal.Replace(input
					.Replace("-", " ")
					.Replace("_", " "), m => m.Captures[0].Value.ToUpper())
				.Replace(" ", string.Empty);

		public static string RemoveOpeningBraceAndNewLines(this string input) =>
			input.TrimStart('{').TrimStart();

		public static string RemoveClosingBraceAndNewLines(this string input) =>
			input.TrimEnd('}').TrimEnd();

		public static string ExtractCallouts(this string input, out List<string> callouts)
		{
			var matches = Callout.Matches(input);
			callouts = new List<string>();

			if (matches.Count == 0)
				return input;

			foreach (Match match in matches)
				callouts.Add($"{match.Groups["callout"].Value} {match.Groups["text"].Value}");

			if (callouts.Any())
				input = CalloutReplacer.Replace(input, "//<$1>");

			return input;
		}
	}
}
