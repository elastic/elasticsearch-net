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
