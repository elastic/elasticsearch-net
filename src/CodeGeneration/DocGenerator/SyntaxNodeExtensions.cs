using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace DocGenerator
{
	public static class SyntaxNodeExtensions
	{
		public static bool ShouldBeHidden(this SyntaxNode node)
		{
			return node.HasLeadingTrivia && Regex.IsMatch(node.GetLeadingTrivia().ToFullString(), @"\/\/\s*hide");
		}
	}
}