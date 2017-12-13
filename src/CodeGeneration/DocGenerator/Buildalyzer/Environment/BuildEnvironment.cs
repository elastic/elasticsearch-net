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

namespace DocGenerator.Buildalyzer.Environment
{
	internal abstract class BuildEnvironment
	{
		private string _oldMsBuildExtensionsPath = null;
		private string _oldMsBuildSdksPath = null;

		public abstract string GetToolsPath();

		public virtual Dictionary<string, string> GetGlobalProperties(string solutionDir) =>
			new Dictionary<string, string>
			{
				{ MsBuildProperties.SolutionDir, solutionDir },
				{ MsBuildProperties.DesignTimeBuild, "true" },
				{ MsBuildProperties.BuildProjectReferences, "false" },
				{ MsBuildProperties.SkipCompilerExecution, "true" },
				{ MsBuildProperties.ProvideCommandLineArgs, "true" },
				// Workaround for a problem with resource files, see https://github.com/dotnet/sdk/issues/346#issuecomment-257654120
				{ MsBuildProperties.GenerateResourceMSBuildArchitecture, "CurrentArchitecture" }
			};

		public virtual void SetEnvironmentVars(IReadOnlyDictionary<string, string> globalProperties)
		{
			if (globalProperties.TryGetValue(MsBuildProperties.MSBuildExtensionsPath, out var msBuildExtensionsPath))
			{
				_oldMsBuildExtensionsPath = System.Environment.GetEnvironmentVariable(MsBuildProperties.MSBuildExtensionsPath);
				System.Environment.SetEnvironmentVariable(MsBuildProperties.MSBuildExtensionsPath, msBuildExtensionsPath);
			}
			if (globalProperties.TryGetValue(MsBuildProperties.MSBuildSDKsPath, out var msBuildSDKsPath))
			{
				_oldMsBuildSdksPath = System.Environment.GetEnvironmentVariable(MsBuildProperties.MSBuildSDKsPath);
				System.Environment.SetEnvironmentVariable(MsBuildProperties.MSBuildSDKsPath, msBuildSDKsPath);
			}
		}

		public virtual void UnsetEnvironmentVars()
		{
			if (_oldMsBuildExtensionsPath != null)
			{
				System.Environment.SetEnvironmentVariable(MsBuildProperties.MSBuildExtensionsPath, _oldMsBuildExtensionsPath);
			}
			if (_oldMsBuildSdksPath != null)
			{
				System.Environment.SetEnvironmentVariable(MsBuildProperties.MSBuildSDKsPath, _oldMsBuildSdksPath);
			}
		}
	}
}
