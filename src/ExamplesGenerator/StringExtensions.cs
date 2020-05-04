// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
