// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;

namespace DocGenerator.Documentation.Blocks
{
	public class JavaScriptBlock : CodeBlock
	{
		public JavaScriptBlock(string text, int startingLine, int depth, string memberName = null)
			: base(text, startingLine, depth, "javascript", memberName) { }

		public string Title { get; set; }
		
		public override string ToAsciiDoc()
		{
			var builder = new StringBuilder();

			if (!string.IsNullOrEmpty(Title))
				builder.AppendLine("." + Title);
			builder.AppendLine(!string.IsNullOrEmpty(MemberName)
				? $"[source, {Language.ToLowerInvariant()}, method=\"{MemberName.ToLowerInvariant()}\"]"
				: $"[source, {Language.ToLowerInvariant()}]");
			builder.AppendLine("----");

			var (code, callOuts) = BlockCallOutHelper.ExtractCallOutsFromCode(Value);

			builder.AppendLine(code);

			builder.AppendLine("----");
			foreach (var callOut in callOuts) builder.AppendLine(callOut);
			return builder.ToString();
		}
	}
}
