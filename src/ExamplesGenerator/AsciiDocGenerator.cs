// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;
using static ExamplesGenerator.ExampleLocation;
using static Microsoft.CodeAnalysis.Formatting.FormattingOptions;
using static Microsoft.CodeAnalysis.LanguageNames;

namespace ExamplesGenerator
{
	internal static class AsciiDocGenerator
	{
		private static readonly AdhocWorkspace Workspace;

		static AsciiDocGenerator()
		{
			Workspace = new AdhocWorkspace();
			Workspace.Options = Workspace.Options
				.WithChangedOption(NewLine, CSharp, "\n")
				.WithChangedOption(IndentationSize, CSharp, 4)
				.WithChangedOption(SmartIndent, CSharp, IndentStyle.Smart)
				.WithChangedOption(UseTabs, CSharp, false)
				.WithChangedOption(CSharpFormattingOptions.IndentBlock, false)
				.WithChangedOption(TabSize, CSharp, 4);
		}

		public static void GenerateExampleAsciiDoc(IEnumerable<ImplementedExample> examples, string branchName)
		{
			foreach (var file in ExamplesAsciiDocDir.EnumerateFiles())
				file.Delete();
			foreach (var dir in ExamplesAsciiDocDir.EnumerateDirectories())
				dir.Delete(true);

			foreach (var example in examples)
			{
				var relativeExampleDirectory = example.Path
					.Replace(".cs", string.Empty)
					.Replace(ExamplesCSharpProject.FullName, string.Empty)
					.TrimStart(Path.DirectorySeparatorChar);

				var exampleFile = new FileInfo(Path.Combine(ExamplesAsciiDocDir.FullName, relativeExampleDirectory, example.Hash + ".asciidoc"));

				var cSharpFile = new FileInfo(example.Path);
				var originalFile = Regex.Replace(cSharpFile.FullName.Replace("\\", "/"), @"^(.*Examples/)",
					$"https://github.com/elastic/elasticsearch-net/tree/{branchName}/tests/Examples/");

				if (!exampleFile.Directory!.Exists)
					exampleFile.Directory.Create();

				var source = Formatter.Format(example.Body, Workspace)
					.ToFullString()
					.RemoveOpeningBraceAndNewLines()
					.RemoveClosingBraceAndNewLines()
					.ExtractCallouts(out var callouts);

				var builder = new StringBuilder()
					.AppendLine($"// {example.ReferenceFileAndLineNumber}")
					.AppendLine()
					.AppendLine("////")
					.AppendLine("IMPORTANT NOTE")
					.AppendLine("==============")
					.AppendLine($"This file is generated from method {example.Method} in {originalFile}#L{example.StartLineNumber}-L{example.EndLineNumber}.")
					.AppendLine("If you wish to submit a PR to change this example, please change the source method above and run")
					.AppendLine()
					.AppendLine("dotnet run -- asciidoc")
					.AppendLine()
					.AppendLine("from the ExamplesGenerator project directory, and submit a PR for the change at")
					.AppendLine("https://github.com/elastic/elasticsearch-net/pulls")
					.AppendLine("////")
					.AppendLine()
					.AppendLine("[source, csharp]")
					.AppendLine("----")
					.AppendLine(source)
					.AppendLine("----");

				foreach (var callout in callouts)
					builder.AppendLine(callout);

				File.WriteAllText(Path.Combine(exampleFile.FullName), builder.ToString());
			}
		}
	}
}
