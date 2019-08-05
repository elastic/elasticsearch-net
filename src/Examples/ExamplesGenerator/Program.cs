using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Options;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.Formatting.FormattingOptions;
using static Microsoft.CodeAnalysis.LanguageNames;

namespace ExamplesGenerator
{
	internal class Program
	{
		private static int Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.Error.Write("must provide a path to examples file");
				return 1;
			}

			var pages = ParsePages(args[0]);
			GenerateExamples(pages);

			return 0;
		}

		private static void GenerateExamples(IList<Page> pages)
		{
			var workspace = new AdhocWorkspace();
			workspace.Options = workspace
				.Options.WithChangedOption(NewLine, CSharp, "\n")
				.WithChangedOption(IndentationSize, CSharp, 4)
				.WithChangedOption(SmartIndent, CSharp, IndentStyle.Smart)
				.WithChangedOption(UseTabs, CSharp, true)
				.WithChangedOption(TabSize, CSharp, 4);

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

		private static string UpdateSource(Page page, CompilationUnitSyntax existingCompilationUnit, AdhocWorkspace workspace)
		{
			if (existingCompilationUnit == null)
				throw new ArgumentNullException(nameof(existingCompilationUnit));

			var classDeclaration = existingCompilationUnit
				.Members.OfType<NamespaceDeclarationSyntax>()
				.Single()
				.Members.OfType<ClassDeclarationSyntax>()
				.Single();

			var methodDeclarations = classDeclaration.Members.OfType<MethodDeclarationSyntax>();
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
					// ensure that the method name is the same
					if (methodDeclaration.Identifier.Text != example.Name)
						newClassDeclaration = newClassDeclaration.AddMembers(methodDeclaration.WithIdentifier(Identifier(example.Name)));
					else
						newClassDeclaration = newClassDeclaration.AddMembers(methodDeclaration);
				}
			}

			return Formatter.Format(existingCompilationUnit.ReplaceNode(classDeclaration, newClassDeclaration), workspace)
				.ToFullString();
		}

		private static string CreateSource(Page page, AdhocWorkspace workspace)
		{
			var compilationUnit = CompilationUnit()
				.AddUsings(
					UsingDirective(Name("Elastic.Xunit.XunitPlumbing")),
					UsingDirective(Name("Nest"))
				);

			var namespaceName = page.PascalNameParts.Length == 1
				? "Examples"
				: string.Join(".", new[] { "Examples" }.Concat(page.PascalNameParts.SkipLast(1)));

			var @namespace = NamespaceDeclaration(Name(namespaceName));
			var className = page.PascalNameParts.Last();

			var classDeclaration = ClassDeclaration(className)
				.AddModifiers(Token(SyntaxKind.PublicKeyword))
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
			var content = example.Content
				.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

			var statements = new List<StatementSyntax>();
			var tabs = "\t\t\t";

			for (var i = 0; i < content.Length; i++)
			{
				var statement = ParseStatement($"var response{i} = new SearchResponse<object>();");
				if (i == 0)
					statement = statement.WithLeadingTrivia(Comment(example.StartTag + "\n"));

				if (i == content.Length - 1)
					statement = statement.WithTrailingTrivia(Whitespace("\n"), Comment(tabs + example.EndTag + "\n\n"));

				statements.Add(statement);
			}

			for (var i = 0; i < content.Length; i++)
			{
				var c = content[i];

				// indent the multi line string literal example, escaping double quotes
				var r = Regex.Replace(
						Regex.Replace(c, @"(?<!\\)""", "\"\""),
						"\r?\n",
						"\n" + tabs)
					.TrimEnd();

				var statement = ParseStatement($"response{i}.MatchesExample(@\"{r}\");")
					.WithTrailingTrivia(Whitespace("\n"));

				statements.Add(statement);
			}

			var methodDeclaration = MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), example.Name)
				.AddModifiers(Token(SyntaxKind.PublicKeyword))
				.WithLeadingTrivia(Comment("// TODO: implement\n"))
				.WithBody(Block(statements));

			return methodDeclaration;
		}

		private static IList<Page> ParsePages(string path)
		{
			var file = File.ReadAllLines(path);
			var pages = new Dictionary<string, Page>();

			for (var index = 0; index < file.Length; index++)
			{
				var line = file[index];
				if (line.StartsWith("=== "))
				{
					// title
					var match = Regex.Match(line, @"=== (?<name>.*?)\.asciidoc: line (?<lineNumber>\d+): (?<hash>.*)$");
					if (!match.Success)
					{
						Console.WriteLine($"Could not find title match, line: {index}, input: {line}");
						continue;
					}

					var name = match.Groups["name"].Value;
					var lineNumber = int.Parse(match.Groups["lineNumber"].Value);
					var hash = match.Groups["hash"].Value;

					// skip to start of body
					index += 3;
					line = file[index];
					var builder = new StringBuilder();

					while (line != "----")
					{
						builder.AppendLine(line);
						index++;
						line = file[index];
					}

					var content = builder.ToString();

					index += 2;
					line = file[index];

					var languages = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
					index += 2;
					line = file[index];
					var implemented = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

					var exampleLanguages = languages
						.Zip(implemented, (l, i) => new Language { Name = l.Trim(), Implemented = i.Trim() == "&check;" })
						.ToList();

					index++;

					if (!pages.TryGetValue(name, out var page))
					{
						page = new Page(name);
						pages.Add(name, page);
					}

					var example = new Example(hash, lineNumber, content);
					example.Languages.AddRange(exampleLanguages);
					page.Examples.Add(example);
				}
			}

			return pages.Values.ToList();
		}
	}

	public static class ExampleLocation
	{
		private static string _root;
		public static string ExamplesDir { get; } = $@"{Root}../../../src/Examples/Examples";

		public static string Root
		{
			get
			{
				if (_root != null) return _root;

				var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

				var runningAsNetCore =
					directoryInfo.Name == "ExamplesGenerator" &&
					directoryInfo.Parent != null &&
					directoryInfo.Parent.Name == "Examples";

				_root = runningAsNetCore ? "" : @"../../../";
				return _root;
			}
		}
	}
}
