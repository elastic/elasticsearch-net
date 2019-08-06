using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExamplesGenerator
{
	internal static class CSharpParser
	{
		public static object ParseImplementedExamples()
		{
			foreach (var file in Directory.EnumerateFiles(ExampleLocation.ExamplesDir, "*Page.cs", SearchOption.AllDirectories))
			{
				var compilationUnit = CSharpSyntaxTree.ParseText(File.ReadAllText(file)).GetCompilationUnitRoot();

				var classDeclaration = compilationUnit
					.Members.OfType<NamespaceDeclarationSyntax>()
					.Single()
					.Members.OfType<ClassDeclarationSyntax>()
					.Single();

				var methodDeclarations = classDeclaration.Members.OfType<MethodDeclarationSyntax>();

				foreach (var methodDeclaration in methodDeclarations)
				{
					var uAttribute = methodDeclaration.AttributeLists.Single().Attributes.Single();

					var uAttributeArguments = uAttribute.ArgumentList.Arguments;

				}
			}

			return null;
		}
	}
}
