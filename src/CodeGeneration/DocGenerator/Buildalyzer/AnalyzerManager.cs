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
using System.Xml.Linq;
using DocGenerator.Buildalyzer.Logging;
using Microsoft.Build.Construction;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;

namespace DocGenerator.Buildalyzer
{
 public class AnalyzerManager
    {
        private readonly Dictionary<string, ProjectAnalyzer> _projects = new Dictionary<string, ProjectAnalyzer>();

        public IReadOnlyDictionary<string, ProjectAnalyzer> Projects => _projects;

        internal ILogger<ProjectAnalyzer> ProjectLogger { get; }

        internal LoggerVerbosity LoggerVerbosity { get; }

        public string SolutionDirectory { get; }

        public AnalyzerManager(ILoggerFactory loggerFactory = null, LoggerVerbosity loggerVerbosity = LoggerVerbosity.Normal)
            : this(null, null, loggerFactory, loggerVerbosity)
        {
        }

        public AnalyzerManager(TextWriter logWriter, LoggerVerbosity loggerVerbosity = LoggerVerbosity.Normal)
            : this(null, logWriter, loggerVerbosity)
        {
        }

        public AnalyzerManager(string solutionFilePath, string[] projects, ILoggerFactory loggerFactory = null, LoggerVerbosity loggerVerbosity = LoggerVerbosity.Normal)
        {
            LoggerVerbosity = loggerVerbosity;
            ProjectLogger = loggerFactory?.CreateLogger<ProjectAnalyzer>();

            if (solutionFilePath != null)
            {
                solutionFilePath = ValidatePath(solutionFilePath, true);
                SolutionDirectory = Path.GetDirectoryName(solutionFilePath);
                GetProjectsInSolution(solutionFilePath, projects);
            }
        }

        public AnalyzerManager(string solutionFilePath, TextWriter logWriter, LoggerVerbosity loggerVerbosity = LoggerVerbosity.Normal)
        {
            LoggerVerbosity = loggerVerbosity;
            if (logWriter != null)
            {
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddProvider(new TextWriterLoggerProvider(logWriter));
                ProjectLogger = loggerFactory.CreateLogger<ProjectAnalyzer>();
            }

            if (solutionFilePath != null)
            {
                solutionFilePath = ValidatePath(solutionFilePath, true);
                SolutionDirectory = Path.GetDirectoryName(solutionFilePath);
                GetProjectsInSolution(solutionFilePath);
            }
        }

        private void GetProjectsInSolution(string solutionFilePath, string[] projects = null)
        {
	        projects = projects ?? new string[] { };
            var supportedType = new[]
            {
                SolutionProjectType.KnownToBeMSBuildFormat,
                SolutionProjectType.WebProject
            };

            var solution = SolutionFile.Parse(solutionFilePath);
            foreach(var project in solution.ProjectsInOrder)
            {
                if (!supportedType.Contains(project.ProjectType)) continue;
	            if (projects.Length > 0 && !projects.Contains(project.ProjectName)) continue;
                GetProject(project.AbsolutePath);
            }
        }

        public ProjectAnalyzer GetProject(string projectFilePath)
        {
            if (projectFilePath == null)
            {
                throw new ArgumentNullException(nameof(projectFilePath));
            }

            // Normalize as .sln uses backslash regardless of OS the sln is created on
            projectFilePath = projectFilePath.Replace('\\', Path.DirectorySeparatorChar);
            projectFilePath = ValidatePath(projectFilePath, true);
            if (_projects.TryGetValue(projectFilePath, out var project))
            {
                return project;
            }
            project = new ProjectAnalyzer(this, projectFilePath);
            _projects.Add(projectFilePath, project);
            return project;
        }

        public ProjectAnalyzer GetProject(string projectFilePath, XDocument projectDocument)
        {
            if (projectFilePath == null)
            {
                throw new ArgumentNullException(nameof(projectFilePath));
            }
            if (projectDocument == null)
            {
                throw new ArgumentNullException(nameof(projectDocument));
            }

            // Normalize as .sln uses backslash regardless of OS the sln is created on
            projectFilePath = projectFilePath.Replace('\\', Path.DirectorySeparatorChar);
            projectFilePath = ValidatePath(projectFilePath, false);
            if (_projects.TryGetValue(projectFilePath, out var project))
            {
                return project;
            }
            project = new ProjectAnalyzer(this, projectFilePath, projectDocument);
            _projects.Add(projectFilePath, project);
            return project;
        }

        private static string ValidatePath(string path, bool checkExists)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            path = Path.GetFullPath(path); // Normalize the path
            if (checkExists && !File.Exists(path))
            {
                throw new ArgumentException($"The path {path} could not be found.");
            }
            return path;
        }
    }
}
