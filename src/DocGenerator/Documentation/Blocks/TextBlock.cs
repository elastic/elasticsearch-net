// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace DocGenerator.Documentation.Blocks
{
	public class TextBlock : IDocumentationBlock
	{
		public TextBlock(string text, int lineNumber)
		{
			Value = text;
			LineNumber = lineNumber;
		}

		public int LineNumber { get; }

		public string Value { get; }

		public string ToAsciiDoc() => Value;
	}
}
