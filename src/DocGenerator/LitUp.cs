// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Buildalyzer;
using Buildalyzer.Workspaces;
using DocGenerator.Documentation.Files;
using Microsoft.CodeAnalysis;

namespace DocGenerator
{
	public static class LitUp
	{
		private static readonly string[] SkipFolders = { "Debug", "Release" };

		private static string GetTestProjectDir(string projectName) => Path.Combine(Program.InputDirPath, "..", "tests", projectName);

		public static IEnumerable<DocumentationFile> InputFiles(string path) =>
			from f in Directory.GetFiles(GetTestProjectDir("Tests"), $"{path}", SearchOption.AllDirectories)
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

		public static HashSet<string> ProjectsWeWant { get; } = new HashSet<string>
		{
			"Elasticsearch.Net", "Nest", "Tests"
		};

		public static async Task<int> GoAsync(string[] args)
		{
			//.NET core csprojects are not supported all that well.
			// https://github.com/dotnet/roslyn/issues/21660 :sadpanda:
			// Use Buildalyzer to get a workspace from the solution.
			var options = new AnalyzerManagerOptions()
			{
				ProjectFilter = p => ProjectsWeWant.Contains(p.ProjectName)
			};

			var analyzer = new AnalyzerManager(Path.Combine(Program.InputDirPath, "..", "Elasticsearch.sln"), options);

			var workspace = analyzer.GetWorkspace();

			var seenFailures = false;
			workspace.WorkspaceFailed += (s, e) =>
			{
				Console.Error.WriteLine($"Workplace failure: {e.Diagnostic.Message}");
				seenFailures = true;
			};

			var projects = workspace.CurrentSolution.Projects
				.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

			if (seenFailures)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Error.WriteLine("Documentation failed to generate.");
				Console.ResetColor();
				return 2;
			}

			DeleteExistingTmpDocs();

			foreach (var file in GetDocumentFiles(projects).SelectMany(s => s))
				await file.SaveToDocumentationFolderAsync();

			if (seenFailures)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Error.WriteLine("Documentation failed to generate.");
				Console.ResetColor();
				return 2;
			}



			CopyBreakingChangesDocs();
			DeleteExistingDocsAndSwap();

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Documentation generated.");
			Console.ResetColor();

			if (Debugger.IsAttached)
			{
				Console.WriteLine("Press any key to continue...");
				Console.ReadKey();
			}
			return 0;
		}


		private static void DeleteExistingTmpDocs()
		{
			var outputDir = new DirectoryInfo(Program.TmpOutputDirPath);
			if (!outputDir.Exists) return;

			foreach (var file in outputDir.EnumerateFiles())
				file.Delete();

		}
		private static void CopyBreakingChangesDocs()
		{
			var outputDir = new DirectoryInfo(Program.OutputDirPath);
			var tmpDir = new DirectoryInfo(Program.TmpOutputDirPath);
			if (!outputDir.Exists) throw new Exception($"Docs folder should be present in repos but does not exist at: {Program.OutputDirPath}");
			if (!tmpDir.Exists)
				throw new Exception($"Temp docs folder should be present in repos after generation ran but does not exist at: {Program.TmpOutputDirPath}");

			foreach (var dir in outputDir.EnumerateDirectories())
			{
				if (!dir.Name.EndsWith("breaking-changes")) continue;

				var newLocation = Path.Combine(tmpDir.FullName, dir.Name);
				Console.WriteLine($"Moving {dir.Name} to: {tmpDir.FullName}");
				dir.MoveTo(newLocation);
			}
		}

		private static void DeleteExistingDocsAndSwap()
		{
			var outputDir = new DirectoryInfo(Program.OutputDirPath);
			var tmpDir = new DirectoryInfo(Program.TmpOutputDirPath);

			foreach (var file in outputDir.EnumerateFiles())
				file.Delete();

			foreach (var dir in outputDir.EnumerateDirectories())
				dir.Delete(true);

			WaitForActualDelete(outputDir);
			Console.WriteLine($"Swapping {tmpDir.FullName} to {outputDir.FullName}");
			tmpDir.MoveTo(Program.OutputDirPath);

			static void WaitForActualDelete(FileSystemInfo toDelete)
			{
				Console.WriteLine($"Attempting to delete {toDelete.FullName}");
				toDelete.Delete();
				var x = 0;
				for (x = 0; toDelete.Exists && x < 100; x++)
				{
					Thread.Sleep(100);
					x++;
				}
			}
		}
	}
}
