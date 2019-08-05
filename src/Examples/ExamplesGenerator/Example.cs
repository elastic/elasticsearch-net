using System;
using System.Collections.Generic;

namespace ExamplesGenerator
{
	public class Example
	{
		public Example(string hash, int lineNumber, string content)
		{
			Hash = hash ?? throw new ArgumentNullException(nameof(hash));
			LineNumber = lineNumber;
			Content = content ?? throw new ArgumentNullException(nameof(content));

			Name = "Line" + LineNumber;
			StartTag = "// tag::" + Hash + "[]";
			EndTag = "// end::" + Hash + "[]";
		}

		public string Content { get; set; }

		public string EndTag { get; }

		public string Hash { get; }

		public List<Language> Languages { get; set; } = new List<Language>();

		public int LineNumber { get; }

		public string Name { get; }

		public string StartTag { get; }
	}
}
