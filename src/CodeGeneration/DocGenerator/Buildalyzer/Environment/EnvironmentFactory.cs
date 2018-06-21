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
using System.Linq;
using System.Xml.Linq;

namespace DocGenerator.Buildalyzer.Environment
{
	internal abstract class EnvironmentFactory
	{
		public static BuildEnvironment GetBuildEnvironment(string projectPath, XDocument projectDocument)
		{
			// If we're running on .NET Core, use the .NET Core SDK regardless of the project file
			if (System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription
				.Replace(" ", "").StartsWith(".NETCore", StringComparison.OrdinalIgnoreCase))
			{
				return new CoreEnvironment(projectPath);
			}

			// Look at the project file to determine
			var projectElement = projectDocument.GetDescendants("Project").FirstOrDefault();
			if (projectElement != null)
			{
				// Does this project use the SDK?
				// Check for an SDK attribute on the project element
				// If no <Project> attribute, check for a SDK import (see https://github.com/Microsoft/msbuild/issues/1493)
				if (projectElement.GetAttributeValue("Sdk") != null
				    || projectElement.GetDescendants("Import").Any(x => x.GetAttributeValue("Sdk") != null))
				{
					// Use the Framework tools if this project targets .NET Framework ("net" followed by a digit)
					// https://docs.microsoft.com/en-us/dotnet/standard/frameworks
					var targetFramework = projectElement.GetDescendants("TargetFramework").FirstOrDefault()?.Value;
					if(targetFramework != null
					   && targetFramework.StartsWith("net", StringComparison.OrdinalIgnoreCase)
					   && targetFramework.Length > 3
					   && char.IsDigit(targetFramework[4]))
					{
						return new FrameworkEnvironment(projectPath, true);
					}

					// Otherwise use the .NET Core SDK
					return new CoreEnvironment(projectPath);
				}

				// Use Framework tools if a ToolsVersion attribute
				if (projectElement.GetAttributeValue("ToolsVersion") != null)
				{
					return new FrameworkEnvironment(projectPath, false);
				}
			}

			throw new InvalidOperationException("Unrecognized project file format");
		}
	}
}
