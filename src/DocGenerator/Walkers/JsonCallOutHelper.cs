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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DocGenerator.Walkers
{
	public class JsonCallOutHelper
	{
		private static readonly Regex CallOutMatcher = new(Constants.CallOutMatcherRegex, RegexOptions.Compiled);

		public string ApplyCallOuts(SyntaxNode node, string json)
		{
			if (node.DescendantNodes().FirstOrDefault(n => n is AnonymousObjectCreationExpressionSyntax) is not
				AnonymousObjectCreationExpressionSyntax syntax) return json;

			var syntaxString = syntax.ToFullString() ?? string.Empty;

			if (string.IsNullOrEmpty(syntaxString)) return json;

			var callOuts = new Dictionary<(string, string), string>();

			using var sr = new StringReader(syntaxString);

			while (true)
			{
				var line = sr.ReadLine();

				if (line is null)
					break;

				var matches = CallOutMatcher.Matches(line);

				foreach (Match match in matches)
				{
					var equalsPosition = line.IndexOf("=", StringComparison.Ordinal);

					if (equalsPosition < 0) break;

					var property = line.Substring(0, equalsPosition).Replace("\t", string.Empty).Trim();

					var commaPosition = line.IndexOf(",", equalsPosition, StringComparison.Ordinal);

					var value = line.Substring(equalsPosition + 1, commaPosition - (equalsPosition + 1)).Trim();
						
					callOuts.Add((property, value), match.Value);
				}
			}

			if (callOuts.Any())
			{
				var sb = new StringBuilder();

				using var jsonStringReader = new StringReader(json);

				while (true)
				{
					var line = jsonStringReader.ReadLine();

					if (line is null) break;

					sb.Append(line);

					foreach (var ((property, propertyValue), callOut) in callOuts)
					{
						if (line.Contains(property) && line.Contains(propertyValue))
						{
							sb.Append(" " + callOut);
						}
					}

					sb.AppendLine();
				}

				return sb.ToString();
			}

			return json;
		}
	}
}
