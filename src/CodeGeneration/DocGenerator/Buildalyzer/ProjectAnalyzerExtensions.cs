#region License
//MIT License
//
//Copyright (c) 2017 Dave Glick
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.VisualBasic;

namespace DocGenerator.Buildalyzer
{
	public static class ProjectAnalyzerExtensions
	{
		/// <summary>
		/// Gets a Roslyn workspace for the analyzed project.
		/// </summary>
		/// <param name="analyzer">The Buildalyzer project analyzer.</param>
		/// <param name="addProjectReferences"><c>true</c> to add projects to the workspace for project references that exist in the same <see cref="AnalyzerManager"/>.</param>
		/// <returns>A Roslyn workspace.</returns>
		public static AdhocWorkspace GetWorkspace(this ProjectAnalyzer analyzer, bool addProjectReferences = false)
		{
			if (analyzer == null)
			{
				throw new ArgumentNullException(nameof(analyzer));
			}
			var workspace = new AdhocWorkspace();
			AddToWorkspace(analyzer, workspace, addProjectReferences);
			return workspace;
		}

		/// <summary>
		/// Adds a project to an existing Roslyn workspace.
		/// </summary>
		/// <param name="analyzer">The Buildalyzer project analyzer.</param>
		/// <param name="workspace">A Roslyn workspace.</param>
		/// <param name="addProjectReferences"><c>true</c> to add projects to the workspace for project references that exist in the same <see cref="AnalyzerManager"/>.</param>
		/// <returns>The newly added Roslyn project.</returns>
		public static Project AddToWorkspace(this ProjectAnalyzer analyzer, AdhocWorkspace workspace, bool addProjectReferences = false)
		{
			if (analyzer == null)
			{
				throw new ArgumentNullException(nameof(analyzer));
			}
			if (workspace == null)
			{
				throw new ArgumentNullException(nameof(workspace));
			}

			// Get or create an ID for this project
			var projectGuid = analyzer.CompiledProject?.GetPropertyValue("ProjectGuid");
			var projectId = !string.IsNullOrEmpty(projectGuid)
			                      && Guid.TryParse(analyzer.CompiledProject?.GetPropertyValue("ProjectGuid"), out var projectIdGuid)
				? ProjectId.CreateFromSerialized(projectIdGuid)
				: ProjectId.CreateNewId();

			// Create and add the project
			var projectInfo = GetProjectInfo(analyzer, workspace, projectId);
			var solution = workspace.CurrentSolution.AddProject(projectInfo);

			// Check if this project is referenced by any other projects in the workspace
			foreach (var existingProject in solution.Projects.ToArray())
			{
				if (!existingProject.Id.Equals(projectId)
				    && analyzer.Manager.Projects.TryGetValue(existingProject.FilePath, out var existingAnalyzer)
				    && (existingAnalyzer.GetProjectReferences()?.Contains(analyzer.ProjectFilePath) ?? false))
				{
					// Add the reference to the existing project
					var projectReference = new ProjectReference(projectId);
					solution = solution.AddProjectReference(existingProject.Id, projectReference);
				}
			}

			// Apply solution changes
			if (!workspace.TryApplyChanges(solution))
			{
				throw new InvalidOperationException("Could not apply workspace solution changes");
			}

			// Add any project references not already added
			if(addProjectReferences)
			{
				foreach(var referencedAnalyzer in GetReferencedAnalyzerProjects(analyzer))
				{
					// Check if the workspace contains the project inside the loop since adding one might also add this one due to transitive references
					if(!workspace.CurrentSolution.Projects.Any(x => x.FilePath == referencedAnalyzer.ProjectFilePath))
					{
						AddToWorkspace(referencedAnalyzer, workspace, addProjectReferences);
					}
				}
			}

			// Find and return this project
			return workspace.CurrentSolution.GetProject(projectId);
		}

		private static ProjectInfo GetProjectInfo(ProjectAnalyzer analyzer, AdhocWorkspace workspace, ProjectId projectId)
		{
			var projectName = Path.GetFileNameWithoutExtension(analyzer.ProjectFilePath);
			var languageName = GetLanguageName(analyzer.ProjectFilePath);
			var projectInfo = ProjectInfo.Create(
				projectId,
				VersionStamp.Create(),
				projectName,
				projectName,
				languageName,
				filePath: analyzer.ProjectFilePath,
				outputFilePath: analyzer.CompiledProject?.GetPropertyValue("TargetPath"),
				documents: GetDocuments(analyzer, projectId),
				projectReferences: GetExistingProjectReferences(analyzer, workspace),
				metadataReferences: GetMetadataReferences(analyzer),
				compilationOptions: CreateCompilationOptions(analyzer.Project, languageName));
			return projectInfo;
		}

		private static CompilationOptions CreateCompilationOptions(Microsoft.Build.Evaluation.Project project, string languageName)
		{
			var outputType = project.GetPropertyValue("OutputType");
			OutputKind? kind = null;
			switch (outputType)
			{
				case "Library":
					kind = OutputKind.DynamicallyLinkedLibrary;
					break;
				case "Exe":
					kind = OutputKind.ConsoleApplication;
					break;
				case "Module":
					kind = OutputKind.NetModule;
					break;
				case "Winexe":
					kind = OutputKind.WindowsApplication;
					break;
			}

			if (kind.HasValue)
			{
				if (languageName == LanguageNames.CSharp)
				{
					return new CSharpCompilationOptions(kind.Value);
				}
				if (languageName == LanguageNames.VisualBasic)
				{
					return new VisualBasicCompilationOptions(kind.Value);
				}
			}

			return null;
		}

		private static IEnumerable<ProjectReference> GetExistingProjectReferences(ProjectAnalyzer analyzer, AdhocWorkspace workspace) =>
			analyzer.GetProjectReferences()
				?.Select(x => workspace.CurrentSolution.Projects.FirstOrDefault(y => y.FilePath == x))
				.Where(x => x != null)
				.Select(x => new ProjectReference(x.Id))
			?? Array.Empty<ProjectReference>();

		private static IEnumerable<ProjectAnalyzer> GetReferencedAnalyzerProjects(ProjectAnalyzer analyzer) =>
			analyzer.GetProjectReferences()
				?.Select(x => analyzer.Manager.Projects.TryGetValue(x, out var a) ? a : null)
				.Where(x => x != null)
			?? Array.Empty<ProjectAnalyzer>();

		private static IEnumerable<DocumentInfo> GetDocuments(ProjectAnalyzer analyzer, ProjectId projectId) =>
			analyzer
				.GetSourceFiles()
				?.Where(File.Exists)
				.Select(x => DocumentInfo.Create(
					DocumentId.CreateNewId(projectId),
					Path.GetFileName(x),
					loader: TextLoader.From(
						TextAndVersion.Create(
							SourceText.From(File.ReadAllText(x)), VersionStamp.Create())),
					filePath: x))
			?? Array.Empty<DocumentInfo>();

		private static IEnumerable<MetadataReference> GetMetadataReferences(ProjectAnalyzer analyzer) =>
			analyzer
				.GetReferences()
				?.Where(File.Exists)
				.Select(x => MetadataReference.CreateFromFile(x))
			?? (IEnumerable<MetadataReference>)Array.Empty<MetadataReference>();

		private static string GetLanguageName(string projectPath)
		{
			switch (Path.GetExtension(projectPath))
			{
				case ".csproj":
					return LanguageNames.CSharp;
				case ".vbproj":
					return LanguageNames.VisualBasic;
				default:
					throw new InvalidOperationException("Could not determine supported language from project path");
			}
		}
	}
}
