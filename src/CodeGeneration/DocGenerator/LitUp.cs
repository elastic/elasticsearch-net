using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocGenerator.Buildalyzer;
using DocGenerator.Documentation.Files;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace DocGenerator
{
	public static class LitUp
	{
		private static readonly string[] SkipFolders = {"Debug", "Release"};

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
			// Use Buildalyzer to get a workspace from the solution.
			var analyzer = new AnalyzerManager(Path.Combine(Program.InputDirPath, "Elasticsearch.sln"), new[]
			{
				"Elasticsearch.Net",
				"Nest",
				"Tests"
			});

			var workspace = analyzer.GetWorkspace();

			workspace.WorkspaceFailed += (s, e) => { Console.Error.WriteLine(e.Diagnostic.Message); };

			// Buildalyzer, similar to MsBuildWorkspace with the new csproj file format, does
			// not pick up source documents in the project directory. Manually add them
			AddDocumentsToWorkspace(workspace);

			var projects = workspace.CurrentSolution.Projects
				.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

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

		private static void AddDocumentsToWorkspace(AdhocWorkspace workspace)
		{
			// we only need source for the Tests project.
			var projects = workspace.CurrentSolution.Projects.Where(p => p.Name == "Tests").ToList();

			for (var i = 0; i < projects.Count; i++)
			{
				var project = projects[i];
				var files = (from f in Directory.GetFiles(Path.GetDirectoryName(project.FilePath), "*.cs", SearchOption.AllDirectories)
					let dir = new DirectoryInfo(f)
					where dir?.Parent != null && !SkipFolders.Contains(dir.Parent.Name)
					select new FileInfo(f)).ToList();

				for (var index = 0; index < files.Count; index++)
				{
					var file = files[index];
					var document = project.AddDocument(file.Name, File.ReadAllText(file.FullName), filePath: file.FullName);
					project = document.Project;
				}

				if (!workspace.TryApplyChanges(project.Solution))
				{
					Console.WriteLine($"failed to apply changes to workspace from project {project.Name}");
				}
			}
		}
	}
}
