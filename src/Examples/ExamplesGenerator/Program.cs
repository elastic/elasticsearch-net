using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ExamplesGenerator
{
	class Program
	{
		static int Main(string[] args)
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
			for (int i = 0; i < pages.Count; i++)
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
					source = CreateClassFile(page);

					var directoryName = Path.GetDirectoryName(sourcePath);
					if (!Directory.Exists(directoryName))
						Directory.CreateDirectory(directoryName);
				}
				else
					source = UpdateClassFile(page, existingCompilationUnit);

				File.WriteAllText(sourcePath, source);
			}
		}

		private static NameSyntax Name(string name) => ParseName(name);

		private static string UpdateClassFile(Page page, CompilationUnitSyntax existingCompilationUnit)
		{
			if (existingCompilationUnit == null)
				throw new ArgumentNullException(nameof(existingCompilationUnit));

			var classDeclaration = existingCompilationUnit
				.Members.OfType<NamespaceDeclarationSyntax>().Single()
				.Members.OfType<ClassDeclarationSyntax>().Single();

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

			return existingCompilationUnit.ReplaceNode(classDeclaration, newClassDeclaration)
				.NormalizeWhitespace()
				.ToFullString();
		}

		private static string CreateClassFile(Page page)
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

			return compilationUnit
				.NormalizeWhitespace()
				.ToFullString();

		}

		private static MethodDeclarationSyntax CreateMethodDeclaration(Example example)
		{
			var content = example.Content
				.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

			var builder = new StringBuilder(example.StartTag)
				.AppendLine();

			for (int i = 0; i < content.Length; i++)
			{
				builder.AppendLine("// TODO: implement correct client call")
					.AppendLine("var response{i} = new SearchResponse<object>();")
					.AppendLine();
			}

			builder.AppendLine(example.EndTag)
				.AppendLine();

			for (int i = 0; i < content.Length; i++)
			{
				var c = content[i];
				var r = Regex.Replace(c, @"(?<!\\)""", "\"\"").TrimEnd();

				builder.AppendLine($"response{i}.MatchesExample(@\"{r}\");")
					.AppendLine();
			}

			var methodDeclaration = MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), example.Name)
				.AddModifiers(Token(SyntaxKind.PublicKeyword))
				.WithLeadingTrivia(Comment("// TODO: implement"))
				.WithBody(Block(ParseStatement(builder.ToString())));

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
		public static string ExamplesDir { get; } = $@"{Root}../../../src/Examples/Examples";

		private static string _root;
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

	public static class MethodDeclarationSyntaxExtensions
	{
		public static bool ContainsSingleLineComment(this MethodDeclarationSyntax methodDeclaration, string comment) =>
			methodDeclaration.ChildTokens().Any(t => t.IsKind(SyntaxKind.SingleLineCommentTrivia) && t.ToFullString() == comment);
	}
}
