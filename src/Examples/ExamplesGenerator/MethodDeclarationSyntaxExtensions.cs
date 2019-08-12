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
