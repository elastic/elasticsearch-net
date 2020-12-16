// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DocGenerator.Walkers;

namespace DocGenerator.Documentation.Blocks
{
	public class BlockCallOutHelper
	{
		private static readonly Regex CallOutMatcher = new(Constants.CallOutMatcherRegex, RegexOptions.Compiled);
		private static readonly Regex CallOutReplacer = new(@"//[ \t]*\<(\d+)\>[^\r\n]*", RegexOptions.Compiled);

		/// <summary>
		/// Extracts the callouts from code. The callout comment is defined inline within
		/// source code to play nicely with C# semantics, but needs to be extracted and placed after the
		/// source block delimiter to be valid asciidoc.
		/// </summary>
		public static (string code, IReadOnlyList<string> callOuts) ExtractCallOutsFromCode(string code)
		{
			var matches = CallOutMatcher.Matches(code);
			var callOuts = new List<string>();

			foreach (Match match in matches)
				callOuts.Add($"{match.Groups["callout"].Value} {match.Groups["text"].Value.TrimEnd()}");

			if (callOuts.Any())
				code = CallOutReplacer.Replace(code, "//<$1>");

			return (code.Trim(), callOuts);
		}
	}
}
