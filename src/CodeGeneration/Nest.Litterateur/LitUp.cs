using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nest.Litterateur.Documentation.Files;

namespace Nest.Litterateur
{
	public static class LitUp
	{
		private static readonly string[] SkipFolders = { "Nest.Tests.Literate", "Debug", "Release" };
		public static IEnumerable<DocumentationFile> InputFiles(string extension) =>
			from f in Directory.GetFiles(Program.InputFolder, $"*.{extension}", SearchOption.AllDirectories)
			let dir = new DirectoryInfo(f)
			where dir?.Parent != null && !SkipFolders.Contains(dir.Parent.Name)
			select DocumentationFile.Load(new FileInfo(f));

		public static IEnumerable<IEnumerable<DocumentationFile>> Input
		{
			get
			{
				yield return InputFiles("doc.cs");
				yield return InputFiles("asciidoc");
				yield return InputFiles("ping");
				yield return InputFiles("gif");
			}
		}	

		public static void Go(string[] args)
		{
			foreach (var file in Input.SelectMany(s=>s))
				file.SaveToDocumentationFolder();
		}
	}
}