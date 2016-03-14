using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nest.Litterateur.Documentation.Blocks
{
	public class CodeBlock : IDocumentationBlock
	{
		public CodeBlock(string lineOfCode, int lineNumber, Language language, string propertyOrMethodName)
		{
			Value = ExtractCallOutsFromText(lineOfCode);
			LineNumber = lineNumber;
			Language = language;
			PropertyName = propertyOrMethodName?.ToLowerInvariant();
		}

		public List<string> CallOuts { get; } = new List<string>();

		public Language Language { get; set; }

		public int LineNumber { get; }

		public string PropertyName { get; set; }

		public string Value { get; }

		private string ExtractCallOutsFromText(string lineOfCode)
		{
			var matches = Regex.Matches(lineOfCode, @"//[ \t]*(?<callout>\<\d+\>)[ \t]*(?<text>\S.*)");
			foreach (Match match in matches)
			{
				CallOuts.Add($"{match.Groups["callout"].Value} {match.Groups["text"].Value}");
			}

			if (CallOuts.Any())
			{
				lineOfCode = Regex.Replace(lineOfCode, @"//[ \t]*\<(\d+)\>.*", "//<$1>");
			}

			return lineOfCode.Trim();
		}
	}
}