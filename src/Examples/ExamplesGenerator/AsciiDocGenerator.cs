using System.Collections.Generic;
using System.IO;
using System.Text;
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

		public static void GenerateExampleAsciiDoc(IEnumerable<ImplementedExample> examples)
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

				if (!exampleFile.Directory.Exists)
					exampleFile.Directory.Create();

				var source = Formatter.Format(example.Body, Workspace)
					.ToFullString()
					.RemoveOpeningBraceAndNewLines()
					.RemoveClosingBraceAndNewLines()
					.ExtractCallouts(out var callouts);

				var builder = new StringBuilder()
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
