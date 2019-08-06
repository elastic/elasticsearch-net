using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static ExamplesGenerator.ExampleLocation;

namespace ExamplesGenerator
{
	internal static class AsciiDocGenerator
	{
		public static void GenerateExampleAsciiDoc(IEnumerable<ImplementedExample> examples)
		{
			foreach (var file in ExamplesAsciiDocDir.EnumerateFiles())
				file.Delete();
			foreach (var dir in ExamplesAsciiDocDir.EnumerateDirectories())
				dir.Delete(true);

			foreach (var example in examples)
			{
				var cSharpFile = new FileInfo(example.Path);
				var relativeExampleDirectory = example.Path
					.Replace(ExamplesCSharpProject.FullName, string.Empty)
					.Replace(".cs", string.Empty)
					.TrimStart(Path.DirectorySeparatorChar);

				var exampleFile = new FileInfo(Path.Combine(ExamplesAsciiDocDir.FullName, relativeExampleDirectory, example.Hash + ".asciidoc"));

				var cSharpFileUri = new Uri(cSharpFile.FullName);
				var includeRelativePath = new Uri(exampleFile.FullName).MakeRelativeUri(cSharpFileUri);

				if (!exampleFile.Directory.Exists)
					exampleFile.Directory.Create();

				var builder = new StringBuilder()
					.AppendLine("[source, csharp]")
					.AppendLine("----")
					.AppendLine($"include::{includeRelativePath}[tag={example.Hash},indent=0]")
					.AppendLine("----");

				File.WriteAllText(Path.Combine(exampleFile.FullName), builder.ToString());
			}
		}
	}
}
