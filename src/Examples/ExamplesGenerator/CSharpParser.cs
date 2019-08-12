using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExamplesGenerator
{
	/// <summary>
	/// Reads the implemented examples from the Examples project
	/// </summary>
	internal static class CSharpParser
	{
		public static IEnumerable<ImplementedExample> ParseImplementedExamples()
		{
			foreach (var path in Directory.EnumerateFiles(ExampleLocation.ExamplesCSharpProject.FullName, "*Page.cs", SearchOption.AllDirectories))
			{
				var compilationUnit = CSharpSyntaxTree.ParseText(File.ReadAllText(path)).GetCompilationUnitRoot();

				var classDeclaration = compilationUnit
					.Members.OfType<NamespaceDeclarationSyntax>()
					.Single()
					.Members.OfType<ClassDeclarationSyntax>()
					.Single();

				var methodDeclarations = classDeclaration.Members.OfType<MethodDeclarationSyntax>();

				foreach (var methodDeclaration in methodDeclarations)
				{
					var uAttribute = methodDeclaration.AttributeLists.Single().Attributes.Single();

					// No Skip property present on [U] attribute
					if (uAttribute.ArgumentList == null)
					{
						// opening tag comment is leading trivia on the first statement
						var openTagComment = methodDeclaration.Body.Statements.First().GetLeadingTrivia()
							.Single(t => t.IsKind(SyntaxKind.SingleLineCommentTrivia) && Tag.IsMatch(t.ToFullString()));

						var hash = Tag.Match(openTagComment.ToFullString()).Groups["hash"].Value;

						yield return new ImplementedExample(path, hash);
					}
				}
			}
		}

		private static Regex Tag = new Regex(@"// tag::(?<hash>.*?)\[\]");
	}
}
