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
using System.Xml;
using System.Xml.Linq;
using DocGenerator.Buildalyzer.Environment;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;
using Microsoft.Build.Tasks;
using Microsoft.Build.Utilities;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Build.Framework.ILogger;

namespace DocGenerator.Buildalyzer
{
	public class ProjectAnalyzer
	{
		private readonly XDocument _projectDocument;
		private readonly Dictionary<string, string> _globalProperties;
		private readonly BuildEnvironment _buildEnvironment;
		private readonly ConsoleLogger _logger;

		private Project _project = null;
		private ProjectInstance _compiledProject = null;

		public AnalyzerManager Manager { get; }

		public string ProjectFilePath { get; }

		/// <summary>
		/// The global properties for MSBuild. By default, each project
		/// is configured with properties that use a design-time build without calling the compiler.
		/// </summary>
		public IReadOnlyDictionary<string, string> GlobalProperties => _globalProperties;

		public Project Project => Load();

		public ProjectInstance CompiledProject => Compile();

		internal ProjectAnalyzer(AnalyzerManager manager, string projectFilePath)
			: this(manager, projectFilePath, XDocument.Load(projectFilePath))
		{
		}

		internal ProjectAnalyzer(AnalyzerManager manager, string projectFilePath, XDocument projectDocument)
		{
			Manager = manager;
			ProjectFilePath = projectFilePath;
			var projectFolder = Path.GetDirectoryName(projectFilePath);
			_projectDocument = TweakProjectDocument(projectDocument, projectFolder);

			// Get the paths
			_buildEnvironment = EnvironmentFactory.GetBuildEnvironment(projectFilePath, _projectDocument);

			// Preload/enforce referencing some required asemblies
			var copy = new Copy();


			var solutionDir = manager.SolutionDirectory ?? projectFolder;
			_globalProperties = _buildEnvironment.GetGlobalProperties(solutionDir);

			// Create the logger
			if (manager.ProjectLogger != null)
			{
				_logger = new ConsoleLogger(manager.LoggerVerbosity, x => manager.ProjectLogger.LogInformation(x), null, null);
			}
		}

		public Project Load()
		{
			if (_project != null)
			{
				return _project;
			}

			// Create a project collection for each project since the toolset might change depending on the type of project
			var projectCollection = CreateProjectCollection();

			// Load the project
			_buildEnvironment.SetEnvironmentVars(GlobalProperties);
			try
			{
				using (var projectReader = _projectDocument.CreateReader())
				{
					_project = projectCollection.LoadProject(projectReader);
					_project.FullPath = ProjectFilePath;
				}
				return _project;
			}
			finally
			{
				_buildEnvironment.UnsetEnvironmentVars();
			}
		}



		// Tweaks the project file a bit to ensure a succesfull build
		private static XDocument TweakProjectDocument(XDocument projectDocument, string projectFolder)
		{
			foreach (var import in projectDocument.GetDescendants("Import").ToArray())
			{
				var att = import.Attribute("Project");
				if (att == null) continue;

				var project = att.Value;

				if (!ResolveKnownPropsPath(projectFolder, project, att, "PublishArtifacts.build.props"))
				{
					ResolveKnownPropsPath(projectFolder, project, att, "Artifacts.build.props");
				}
				ResolveKnownPropsPath(projectFolder, project, att, "Library.build.props");
			}
			// Add SkipGetTargetFrameworkProperties to every ProjectReference
			foreach (var projectReference in projectDocument.GetDescendants("ProjectReference").ToArray())
			{
				projectReference.AddChildElement("SkipGetTargetFrameworkProperties", "true");
			}

			// Removes all EnsureNuGetPackageBuildImports
			foreach (var ensureNuGetPackageBuildImports in
				projectDocument.GetDescendants("Target").Where(x => x.GetAttributeValue("Name") == "EnsureNuGetPackageBuildImports").ToArray())
			{
				ensureNuGetPackageBuildImports.Remove();
			}

			return projectDocument;
		}

		private static bool ResolveKnownPropsPath(string projectFolder, string project, XAttribute att, string buildPropFile)
		{
			if (!project.Contains(buildPropFile)) return false;
			var dir = new DirectoryInfo(projectFolder).Parent;
			while (dir != null && dir.Name != "src")
				dir = dir.Parent;
			if (dir == null) return true;
			att.Value = Path.GetFullPath(Path.Combine(dir.FullName, buildPropFile));
			return false;
		}

		private ProjectCollection CreateProjectCollection()
		{
			var projectCollection = new ProjectCollection(_globalProperties);
			projectCollection.RemoveAllToolsets(); // Make sure we're only using the latest tools
			projectCollection.AddToolset(
				new Toolset(ToolLocationHelper.CurrentToolsVersion, _buildEnvironment.GetToolsPath(), projectCollection, string.Empty));
			projectCollection.DefaultToolsVersion = ToolLocationHelper.CurrentToolsVersion;
			if (_logger != null)
			{
				projectCollection.RegisterLogger(_logger);
			}
			return projectCollection;
		}

		public ProjectInstance Compile()
		{
			if (_compiledProject != null)
			{
				return _compiledProject;
			}
			var project = Load();
			if (project == null)
			{
				return null;
			}

			// Compile the project
			_buildEnvironment.SetEnvironmentVars(GlobalProperties);
			try
			{
				var projectInstance = project.CreateProjectInstance();
				if (!projectInstance.Build("Clean", _logger == null ? null : new ILogger[] { _logger }))
				{
					return null;
				}
				if (!projectInstance.Build("Compile", _logger == null ? null : new ILogger[] { _logger }))
				{
					return null;
				}
				_compiledProject = projectInstance;
				return _compiledProject;
			}
			finally
			{
				_buildEnvironment.UnsetEnvironmentVars();
			}
		}

		public IReadOnlyList<string> GetSourceFiles() =>
			Compile()?.Items
				.Where(x => x.ItemType == "CscCommandLineArgs" && !x.EvaluatedInclude.StartsWith("/"))
				.Select(x => Path.GetFullPath(Path.Combine(Path.GetDirectoryName(ProjectFilePath), x.EvaluatedInclude)))
				.ToList();

		public IReadOnlyList<string> GetReferences() =>
			Compile()?.Items
				.Where(x => x.ItemType == "CscCommandLineArgs" && x.EvaluatedInclude.StartsWith("/reference:"))
				.Select(x => x.EvaluatedInclude.Substring(11).Trim('"'))
				.ToList();

		public IReadOnlyList<string> GetProjectReferences() =>
			Compile()?.Items
				.Where(x => x.ItemType == "ProjectReference")
				.Select(x => Path.GetFullPath(Path.Combine(Path.GetDirectoryName(ProjectFilePath), x.EvaluatedInclude)))
				.ToList();

		public void SetGlobalProperty(string key, string value)
		{
			if (_project != null)
			{
				throw new InvalidOperationException("Can not change global properties once project has been loaded");
			}
			_globalProperties[key] = value;
		}

		public bool RemoveGlobalProperty(string key)
		{
			if (_project != null)
			{
				throw new InvalidOperationException("Can not change global properties once project has been loaded");
			}
			return _globalProperties.Remove(key);
		}
	}
}
