using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocGenerator.Documentation.Files;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace DocGenerator
{
	public static class LitUp
	{
		private static readonly string[] SkipFolders = { "Debug", "Release" };

        private static string GetProjectDir(string projectName) => Path.Combine(Program.InputDirPath, projectName);
        private static string GetProjectFile(string projectName) => Path.Combine(GetProjectDir(projectName), $"{projectName}.csproj");

        public static IEnumerable<DocumentationFile> InputFiles(string path) =>
			from f in Directory.GetFiles(GetProjectDir("Tests"), $"{path}", SearchOption.AllDirectories)
			let dir = new DirectoryInfo(f)
			where dir?.Parent != null && !SkipFolders.Contains(dir.Parent.Name)
			select DocumentationFile.Load(new FileInfo(f));

		public static IEnumerable<IEnumerable<DocumentationFile>> GetDocumentFiles(Dictionary<string, Project> projects)
		{
		    var testProject = projects["Tests"];

			yield return testProject.Documents
			   .Where(d => d.Name.EndsWith(".doc.cs", StringComparison.OrdinalIgnoreCase))
			   .Select(d => new CSharpDocumentationFile(d, projects));

			yield return testProject.Documents
				.Where(d => d.Name.EndsWith("UsageTests.cs", StringComparison.OrdinalIgnoreCase))
				.Select(d => new CSharpDocumentationFile(d, projects));

			yield return InputFiles("*.png");
			yield return InputFiles("*.gif");
			yield return InputFiles("*.jpg");
			// process asciidocs last as they may have generated
			// includes to other output asciidocs
			yield return InputFiles("*.asciidoc");
	    }

		public static async Task GoAsync(string[] args)
		{
			//.NET core csprojects are not supported all that well.
			// https://github.com/dotnet/roslyn/issues/21660 :sadpanda:

			var workspace = MSBuildWorkspace.Create();
			workspace.WorkspaceFailed += (s, e) =>
			{
				Console.Error.WriteLine(e.Diagnostic.Message);
			};
            var testProject = workspace.OpenProjectAsync(GetProjectFile("Tests"));
            var nestProject = workspace.OpenProjectAsync(GetProjectFile("Nest"));
            var elasticSearchNetProject = workspace.OpenProjectAsync(GetProjectFile("Elasticsearch.Net"));

		    var projects = new []
		    {
		        await testProject,
		        await nestProject,
		        await elasticSearchNetProject
		    }.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

            foreach (var file in GetDocumentFiles(projects).SelectMany(s => s))
			{
				await file.SaveToDocumentationFolderAsync();
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Documentation generated.");
			Console.ResetColor();

			if (Debugger.IsAttached)
			{
				Console.WriteLine("Press any key to continue...");
				Console.ReadKey();
			}
		}
	}
}
