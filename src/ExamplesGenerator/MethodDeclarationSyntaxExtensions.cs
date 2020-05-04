// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExamplesGenerator
{
	public static class MethodDeclarationSyntaxExtensions
	{
		public static bool ContainsSingleLineComment(this MethodDeclarationSyntax methodDeclaration, string comment) =>
			methodDeclaration.DescendantTrivia().Any(t => t.IsKind(SyntaxKind.SingleLineCommentTrivia) && t.ToFullString() == comment);
	}
}
