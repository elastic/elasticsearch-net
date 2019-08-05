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
using static Microsoft.CodeAnalysis.LanguageNames;

namespace ExamplesGenerator
{
	internal static class CSharpGenerator
	{
		private static readonly Regex Content = new Regex("\r?\n\r?\n|\r?\n(?=GET|PUT|POST|DELETE)");

		public static void GenerateExampleClasses(IList<Page> pages)
		{
			var workspace = new AdhocWorkspace();
			workspace.Options = workspace.Options
				.WithChangedOption(FormattingOptions.NewLine, CSharp, "\n")
				.WithChangedOption(FormattingOptions.IndentationSize, CSharp, 4)
				.WithChangedOption(FormattingOptions.SmartIndent, CSharp, FormattingOptions.IndentStyle.Smart)
				.WithChangedOption(FormattingOptions.UseTabs, CSharp, true)
				.WithChangedOption(FormattingOptions.TabSize, CSharp, 4);

			for (var i = 0; i < pages.Count; i++)
			{
				var page = pages[i];

				var sourcePath = page.FullPath(ExampleLocation.ExamplesDir);
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

		private static string UpdateSource(Page page, CompilationUnitSyntax existingCompilationUnit, Workspace workspace)
		{
			if (existingCompilationUnit == null)
				throw new ArgumentNullException(nameof(existingCompilationUnit));

			var classDeclaration = existingCompilationUnit
				.Members.OfType<NamespaceDeclarationSyntax>()
				.Single()
				.Members.OfType<ClassDeclarationSyntax>()
				.Single();

			var methodDeclarations = classDeclaration.Members.OfType<MethodDeclarationSyntax>();

			// clear members from the class, to add only those that exist in the docs
			var newClassDeclaration = classDeclaration.WithMembers(default);

			foreach (var example in page.Examples)
			{
				var methodDeclaration = methodDeclarations.SingleOrDefault(m => m.ContainsSingleLineComment(example.StartTag));

				if (methodDeclaration == null)
				{
					methodDeclaration = CreateMethodDeclaration(example);
					newClassDeclaration = newClassDeclaration.AddMembers(methodDeclaration);
				}
				else
				{
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

		private static string CreateSource(Page page, Workspace workspace)
		{
			var compilationUnit = CompilationUnit()
				.AddUsings(
					UsingDirective(Name("Elastic.Xunit.XunitPlumbing")),
					UsingDirective(Name("Nest"))
				);

			var @namespace = NamespaceDeclaration(Name(page.Namespace));
			var className = page.ClassName;

			var classDeclaration = ClassDeclaration(className)
				.AddModifiers(Token(PublicKeyword))
				.AddBaseListTypes(SimpleBaseType(ParseTypeName("ExampleBase")));

			foreach (var example in page.Examples)
			{
				var methodDeclaration = CreateMethodDeclaration(example);
				classDeclaration = classDeclaration.AddMembers(methodDeclaration);
			}

			@namespace = @namespace.AddMembers(classDeclaration);
			compilationUnit = compilationUnit.AddMembers(@namespace);

			return Formatter.Format(compilationUnit, workspace).ToFullString();
		}

		private static MethodDeclarationSyntax CreateMethodDeclaration(Example example)
		{
			// split content by blank lines and lines that start with HTTP verbs
			var content = Content.Split(example.Content);
			var statements = new List<StatementSyntax>();

			for (var i = 0; i < content.Length; i++)
			{
				var statement = ParseStatement($"var response{i} = new SearchResponse<object>();");
				if (i == 0)
					statement = statement.WithLeadingTrivia(
						Comment(example.StartTag),
						EndOfLine("\n"));

				if (i == content.Length - 1)
					statement = statement.WithTrailingTrivia(
						EndOfLine("\n"),
						Comment(example.EndTag),
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

			var methodDeclaration = MethodDeclaration(PredefinedType(Token(VoidKeyword)), example.Name)
				.AddAttributeLists(
					// mark as unit test
					AttributeList(SingletonSeparatedList(Attribute(Name("U")))),
					// but skip for now, until implemented
					AttributeList(SingletonSeparatedList(Attribute(Name("SkipExample")))))
				.AddModifiers(Token(PublicKeyword))
				.WithBody(Block(statements));

			return methodDeclaration;
		}
	}
}
