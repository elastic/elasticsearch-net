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
