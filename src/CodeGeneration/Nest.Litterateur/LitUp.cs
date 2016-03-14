using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nest.Litterateur.Documentation.Files;

#if !DOTNETCORE
using AsciiDoc;
#endif

namespace Nest.Litterateur
{
	public static class LitUp
	{
		private static readonly string[] SkipFolders = { "Nest.Tests.Literate", "Debug", "Release" };

		private static readonly Dictionary<string, decimal> Sections = new Dictionary<string, decimal>();

		public static IEnumerable<DocumentationFile> InputFiles(string extension) =>
			from f in Directory.GetFiles(Program.InputDirPath, $"*.{extension}", SearchOption.AllDirectories)
			let dir = new DirectoryInfo(f)
			where dir?.Parent != null && !SkipFolders.Contains(dir.Parent.Name)
			select DocumentationFile.Load(new FileInfo(f), Sections);

		public static IEnumerable<IEnumerable<DocumentationFile>> Input
		{
			get
			{
				yield return InputFiles("doc.cs");
				yield return InputFiles("asciidoc");
				yield return InputFiles("png");
				yield return InputFiles("gif");
				yield return InputFiles("jpg");
			}
		}	

		public static void Go(string[] args)
		{
			foreach (var file in Input.SelectMany(s => s))
			{
				file.SaveToDocumentationFolder();
			}

#if !DOTNETCORE
			// generate the index.asciidoc file from the sections
			var indexDoc = new Document
			{
				Title = new DocumentTitle("Elasticsearch.Net and NEST, the .NET Elasticsearch clients")
				{
					Attributes =
					{
						new Anchor("elasticsearch-net-reference", null),
					}
				}
			};

			indexDoc.Elements.Add(new Include("intro.asciidoc"));

			// TODO: Only the top level sections should be in the index where each top level section should link to other sections
			foreach (var section in Sections.GroupBy(s => s.Key).OrderBy(s => s.Min(kv => kv.Value)).ThenBy(s => s.Key))
			{
				indexDoc.Elements.Add(new Include(section.Key));
			}

			using (var visitor = new AsciiDocVisitor(Path.Combine(Program.OutputDirPath, "index.asciidoc")))
			{
				indexDoc.Accept(visitor);
			}
#endif
		}
	}
}