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

using System.Collections.Generic;
using System.IO;

namespace DocGenerator.Buildalyzer.Environment
{
	// Based on code from OmniSharp
	// https://github.com/OmniSharp/omnisharp-roslyn/blob/78ccc8b4376c73da282a600ac6fb10fce8620b52/src/OmniSharp.Abstractions/Services/DotNetCliService.cs
	internal class CoreEnvironment : BuildEnvironment
	{
		public string ToolsPath { get; }
		public string ExtensionsPath { get; }
		public string SDKsPath { get; }
		public string RoslynTargetsPath { get; }

		public CoreEnvironment(string projectPath)
		{
			var dotnetPath = DotnetPathResolver.ResolvePath(projectPath);
			ToolsPath = dotnetPath;
			ExtensionsPath = dotnetPath;
			SDKsPath = Path.Combine(dotnetPath, "Sdks");
			RoslynTargetsPath = Path.Combine(dotnetPath, "Roslyn");
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
	}
}
