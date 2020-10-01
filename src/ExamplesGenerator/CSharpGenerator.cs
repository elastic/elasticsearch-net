// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.CSharp.SyntaxKind;
using static Microsoft.CodeAnalysis.Formatting.FormattingOptions;
using static Microsoft.CodeAnalysis.LanguageNames;

namespace ExamplesGenerator
{
	/// <summary>
	/// Generates C# classes from the Elasticsearch reference docs
	/// </summary>
	internal static class CSharpGenerator
	{
		private static readonly Regex Content = new Regex("\r?\n\r?\n|\r?\n(?=HEAD|GET|PUT|POST|DELETE)");

		public static void GenerateExampleClasses(IList<ReferencePage> pages)
		{
			var workspace = new AdhocWorkspace();
			workspace.Options = workspace.Options
				.WithChangedOption(NewLine, CSharp, "\n")
				.WithChangedOption(IndentationSize, CSharp, 4)
				.WithChangedOption(SmartIndent, CSharp, IndentStyle.Smart)
				.WithChangedOption(UseTabs, CSharp, true)
				.WithChangedOption(TabSize, CSharp, 4);

			for (var i = 0; i < pages.Count; i++)
			{
				var page = pages[i];

				var sourcePath = page.FullPath(ExampleLocation.ExamplesCSharpProject);
				CompilationUnitSyntax existingCompilationUnit = null;

				if (File.Exists(sourcePath))
				{
					var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(sourcePath));
					existingCompilationUnit = syntaxTree.GetCompilationUnitRoot();
				}

				string source;
				if (existingCompilationUnit == null)
				{
					source = CreateSource(page, workspace);

					var directoryName = Path.GetDirectoryName(sourcePath);
					if (!Directory.Exists(directoryName))
						Directory.CreateDirectory(directoryName);
				}
				else
					source = UpdateSource(page, existingCompilationUnit, workspace);

				File.WriteAllText(sourcePath, source);
			}
		}

		private static NameSyntax Name(string name) => ParseName(name);

		private static string UpdateSource(ReferencePage referencePage, CompilationUnitSyntax existingCompilationUnit, Workspace workspace)
		{
			if (existingCompilationUnit == null)
				throw new ArgumentNullException(nameof(existingCompilationUnit));

			if (existingCompilationUnit.Usings.All(u => u.Name.ToString() != "System.ComponentModel"))
				existingCompilationUnit = existingCompilationUnit.AddUsings(UsingDirective(Name("System.ComponentModel")));

			var classDeclaration = existingCompilationUnit
				.Members.OfType<NamespaceDeclarationSyntax>()
				.Single()
				.Members.OfType<ClassDeclarationSyntax>()
				.Single();

			var methodDeclarations = classDeclaration.Members.OfType<MethodDeclarationSyntax>().ToList();

			// clear members from the class, to add only those that exist in the docs
			var newClassDeclaration = classDeclaration.WithMembers(default);

			foreach (var example in referencePage.Examples)
			{
				var methodDeclaration = methodDeclarations.SingleOrDefault(m => m.ContainsSingleLineComment(example.StartTag));

				if (methodDeclaration == null)
				{
					methodDeclaration = CreateMethodDeclaration(example);
					newClassDeclaration = newClassDeclaration.AddMembers(methodDeclaration);
				}
				else
				{
					// add or update the Description attribute
					var descriptionAttribute = methodDeclaration.AttributeLists.SingleOrDefault(a => a.ToString().Contains("Description"));
					if (descriptionAttribute == null)
						methodDeclaration = methodDeclaration.AddAttributeLists(AttributeList(GetReferencePageAttribute(example)));
					else
						methodDeclaration = methodDeclaration
							.ReplaceNode(descriptionAttribute, AttributeList(GetReferencePageAttribute(example)));

					// ensure that the method name is the same i.e. same line number
					if (methodDeclaration.Identifier.Text != example.Name)
						newClassDeclaration = newClassDeclaration.AddMembers(methodDeclaration.WithIdentifier(Identifier(example.Name)));
					else
						newClassDeclaration = newClassDeclaration.AddMembers(methodDeclaration);
				}
			}

			return Formatter.Format(existingCompilationUnit.ReplaceNode(classDeclaration, newClassDeclaration), workspace)
				.ToFullString();
		}

		private static string CreateSource(ReferencePage referencePage, Workspace workspace)
		{
			var compilationUnit = CompilationUnit()
				.AddUsings(
					UsingDirective(Name("Elastic.Elasticsearch.Xunit.XunitPlumbing")),
					UsingDirective(Name("System.ComponentModel")),
					UsingDirective(Name("Nest"))
				);

			var @namespace = NamespaceDeclaration(Name(referencePage.Namespace));
			var className = referencePage.ClassName;

			var classDeclaration = ClassDeclaration(className)
				.AddModifiers(Token(PublicKeyword))
				.AddBaseListTypes(SimpleBaseType(ParseTypeName("ExampleBase")));

			foreach (var example in referencePage.Examples)
			{
				var methodDeclaration = CreateMethodDeclaration(example);
				classDeclaration = classDeclaration.AddMembers(methodDeclaration);
			}

			@namespace = @namespace.AddMembers(classDeclaration);
			compilationUnit = compilationUnit.AddMembers(@namespace);

			return Formatter.Format(compilationUnit, workspace).ToFullString();
		}

		private static MethodDeclarationSyntax CreateMethodDeclaration(ReferenceExample referenceExample)
		{
			// split content by blank lines and lines that start with HTTP verbs. Remove comments starting with #
			var content = GetContent(referenceExample.Content);
			var statements = new List<StatementSyntax>();

			for (var i = 0; i < content.Length; i++)
			{
				var statement = ParseStatement($"var response{i} = new SearchResponse<object>();");
				if (i == 0)
					statement = statement.WithLeadingTrivia(
						Comment(referenceExample.StartTag),
						EndOfLine("\n"));

				if (i == content.Length - 1)
					statement = statement.WithTrailingTrivia(
						EndOfLine("\n"),
						Comment(referenceExample.EndTag),
						EndOfLine("\n"),
						EndOfLine("\n"));
				else
					statement = statement.WithTrailingTrivia(
						EndOfLine("\n"),
						EndOfLine("\n"));

				statements.Add(statement);
			}

			for (var i = 0; i < content.Length; i++)
			{
				var exampleContent = content[i];

				// indent the multi line string literal example, escaping double quotes
				var r = exampleContent.EscapeDoubleQuotes().Indent("\t\t\t").TrimEnd();

				var statement = ParseStatement($"response{i}.MatchesExample(@\"{r}\");")
					.WithTrailingTrivia(EndOfLine("\n"), EndOfLine("\n"));

				statements.Add(statement);
			}

			// mark as skipped unit test
			var unitTestSkip = SingletonSeparatedList(
				Attribute(Name("U"),
					AttributeArgumentList(SingletonSeparatedList(
						AttributeArgument(ParseExpression("Skip = \"Example not implemented\""))))));

			var methodDeclaration = MethodDeclaration(PredefinedType(Token(VoidKeyword)), referenceExample.Name)
				.AddAttributeLists(AttributeList(unitTestSkip))
				.AddAttributeLists(AttributeList(GetReferencePageAttribute(referenceExample)))
				.AddModifiers(Token(PublicKeyword))
				.WithBody(Block(statements));

			return methodDeclaration;
		}

		private static string[] GetContent(string content)
		{
			var c = Content.Split(content).ToList();
			c.RemoveAll(l => l.StartsWith("#"));
			return c.ToArray();
		}

		private static SeparatedSyntaxList<AttributeSyntax> GetReferencePageAttribute(ReferenceExample referenceExample) =>
			SingletonSeparatedList(
				Attribute(Name("Description"),
					AttributeArgumentList(SingletonSeparatedList(
						AttributeArgument(ParseExpression($"\"{referenceExample.File}:{referenceExample.LineNumber}\""))))));
	}
}
