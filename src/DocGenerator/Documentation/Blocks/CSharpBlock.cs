// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DocGenerator.Documentation.Blocks
{
	public class CSharpBlock : CodeBlock
	{
		public CSharpBlock(SyntaxNode node, int depth, string memberName = null)
			: base(node.WithoutLeadingTrivia().ToFullStringWithoutPragmaWarningDirectiveTrivia(),
				node.StartingLine(),
				node.IsKind(SyntaxKind.ClassDeclaration) ? depth : depth + 2,
				"csharp",
				memberName) { }
		
		public void AddNode(SyntaxNode node) => Lines.Add(node.WithLeadingEndOfLineTrivia().ToFullStringWithoutPragmaWarningDirectiveTrivia());

		public override string ToAsciiDoc()
		{
			var builder = new StringBuilder();

			// method attribute is used to add section titles in GeneratedAsciidocVisitor
			builder.AppendLine(!string.IsNullOrEmpty(MemberName)
				? $"[source, {Language.ToLowerInvariant()}, method=\"{MemberName.ToLowerInvariant()}\"]"
				: $"[source, {Language.ToLowerInvariant()}]");

			builder.AppendLine("----");
			
			var (code, callOuts) = BlockCallOutHelper.ExtractCallOutsFromCode(Value);

			code = code.RemoveNumberOfLeadingTabsOrSpacesAfterNewline(Depth);
			builder.AppendLine(code);

			builder.AppendLine("----");
			foreach (var callOut in callOuts) builder.AppendLine(callOut);
			return builder.ToString();
		}
	}
}
