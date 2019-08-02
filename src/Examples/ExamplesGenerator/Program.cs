using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ExamplesGenerator
{
	public class Page
	{
		public string Name { get; set; }

		public List<Example> Examples { get; } = new List<Example>();
	}

	public class Example
	{
		public string LineNumber { get; set; }

		public string Hash { get; set; }

		public string Content { get; set; }

		public List<Language> Languages { get; set; } = new List<Language>();
	}

	public class Language
	{
		public string Name { get; set; }
		public bool Implemented { get; set; }
	}

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

				var parts = page.Name.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

				var names = parts
					.Select(p => LowercaseHyphenToPascal(p.Trim()))
					.ToList();

				var source = GenerateClassFile(page, names);
			}
		}

		private static NameSyntax Name(string name) => ParseName(name);

		public static string LowercaseHyphenToPascal(string lowercaseHyphenatedInput) =>
			Regex.Replace(lowercaseHyphenatedInput.Replace("-", " "), @"\b([a-z])", m => m.Captures[0].Value.ToUpper())
				.Replace(" ", string.Empty);

		private static string GenerateClassFile(Page page, IList<string> names)
		{
			var namespaceName = names.Count == 1
				? "Examples"
				: string.Join(".", new[] { "Examples" }.Concat(names.SkipLast(1)));

			var ns = NamespaceDeclaration(Name(namespaceName))
				.AddUsings(
					UsingDirective(Name("Elastic.Xunit.XunitPlumbing")),
					UsingDirective(Name("Nest"))
				);

			var className = names.Last();

			var classDeclaration = ClassDeclaration(className)
				.AddModifiers(Token(SyntaxKind.PublicKeyword))
				.AddBaseListTypes(SimpleBaseType(ParseTypeName("ExampleBase")));

			foreach (var example in page.Examples)
			{
				var content = example.Content
					.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

				var builder = new StringBuilder("// tag::" + example.Hash + "[]");
				builder.AppendLine();

				for (int i = 0; i < content.Length; i++)
				{
					builder.AppendLine($"var response{i} = new SearchResponse<T>(); // TODO: fix");
					builder.AppendLine();
				}

				builder.AppendLine("// end::" + example.Hash + "[]");
				builder.AppendLine();

				for (int i = 0; i < content.Length; i++)
				{
					var c = content[i];
					var r = Regex.Replace(c, @"(?<!\\)""", "\"\"");

					builder.AppendLine($"response{i}.MatchesExample(@\"{r}\");");
					builder.AppendLine();
				}

				var methodDeclaration = MethodDeclaration(Name("void"), "Line" + example.LineNumber)
					.AddModifiers(Token(SyntaxKind.PublicKeyword))
					.WithBody(Block(ParseStatement(builder.ToString())));

				classDeclaration = classDeclaration.AddMembers(methodDeclaration);
			}

			ns = ns.AddMembers(classDeclaration);

			return ns
				.NormalizeWhitespace()
				.ToFullString();
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
					var lineNumber = match.Groups["lineNumber"].Value;
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
						page = new Page { Name = name, };
						pages.Add(name, page);
					}

					page.Examples.Add(new Example
					{
						Content = content,
						Hash = hash,
						LineNumber = lineNumber,
						Languages = exampleLanguages
					});
				}
			}

			return pages.Values.ToList();
		}
	}
}
