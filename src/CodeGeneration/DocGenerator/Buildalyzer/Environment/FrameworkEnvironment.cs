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
using Microsoft.Build.Utilities;

namespace DocGenerator.Buildalyzer.Environment
{
	internal class FrameworkEnvironment : BuildEnvironment
	{
		public string ToolsPath { get; }
		public string ExtensionsPath { get; }
		public string SDKsPath { get; }
		public string RoslynTargetsPath { get; }

		public FrameworkEnvironment(string projectPath, bool sdkProject)
		{
			ToolsPath = LocateToolsPath();
			ExtensionsPath = Path.GetFullPath(Path.Combine(ToolsPath, @"..\..\"));
			SDKsPath = Path.Combine(sdkProject ? DotnetPathResolver.ResolvePath(projectPath) : ExtensionsPath, "Sdks");
			RoslynTargetsPath = Path.Combine(ToolsPath, "Roslyn");
		}

		public override string GetToolsPath() => ToolsPath;

		public override Dictionary<string, string> GetGlobalProperties(string solutionDir)
		{
			var globalProperties = base.GetGlobalProperties(solutionDir);
			globalProperties.Add(MsBuildProperties.MSBuildExtensionsPath, ExtensionsPath);
			globalProperties.Add(MsBuildProperties.MSBuildSDKsPath, SDKsPath);
			globalProperties.Add(MsBuildProperties.RoslynTargetsPath, RoslynTargetsPath);
			return globalProperties;
		}

		private static string LocateToolsPath()
		{
			var toolsPath = ToolLocationHelper.GetPathToBuildToolsFile("msbuild.exe", ToolLocationHelper.CurrentToolsVersion);
			if (string.IsNullOrEmpty(toolsPath))
			{
				// Could not find the tools path, possibly due to https://github.com/Microsoft/msbuild/issues/2369
				// Try to poll for it
				toolsPath = PollForToolsPath();
			}
			if (string.IsNullOrEmpty(toolsPath))
			{
				throw new InvalidOperationException("Could not locate the tools (msbuild.exe) path");
			}
			return Path.GetDirectoryName(toolsPath);
		}

		// From https://github.com/KirillOsenkov/MSBuildStructuredLog/blob/4649f55f900a324421bad5a714a2584926a02138/src/StructuredLogViewer/MSBuildLocator.cs
		private static string PollForToolsPath()
		{
			var programFilesX86 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86);
			return new[]
				{
					Path.Combine(programFilesX86, @"Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe"),
					Path.Combine(programFilesX86, @"Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"),
					Path.Combine(programFilesX86, @"Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe")
				}
				.Where(File.Exists)
				.FirstOrDefault();
		}
	}
}
