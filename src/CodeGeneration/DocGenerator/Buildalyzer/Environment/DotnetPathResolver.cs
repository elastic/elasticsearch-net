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
using System.Diagnostics;
using System.IO;

namespace DocGenerator.Buildalyzer.Environment
{
	internal static class DotnetPathResolver
	{
		const string DOTNET_CLI_UI_LANGUAGE = nameof(DOTNET_CLI_UI_LANGUAGE);

		private static readonly object BasePathLock = new object();
		private static string BasePath = null;

		public static string ResolvePath(string projectPath)
		{
			lock(BasePathLock)
			{
				if(BasePath != null)
				{
					return BasePath;
				}

				// Need to rety calling "dotnet --info" and do a hacky timeout for the process otherwise it occasionally locks up during testing (and possibly in the field)
				var lines = GetInfo(projectPath);
				var retry = 0;
				do
				{
					lines = GetInfo(projectPath);
					retry++;
				} while ((lines == null || lines.Count == 0) && retry < 5);
				BasePath = ParseBasePath(lines);

				return BasePath;
			}
		}

		private static List<string> GetInfo(string projectPath)
		{
			// Ensure that we set the DOTNET_CLI_UI_LANGUAGE environment variable to "en-US" before
			// running 'dotnet --info'. Otherwise, we may get localized results.
			var originalCliLanguage = System.Environment.GetEnvironmentVariable(DOTNET_CLI_UI_LANGUAGE);
			System.Environment.SetEnvironmentVariable(DOTNET_CLI_UI_LANGUAGE, "en-US");

			try
			{
				// Create the process info
				var process = new Process();
				process.StartInfo.FileName = "dotnet";
				process.StartInfo.Arguments = "--info";
				process.StartInfo.WorkingDirectory = Path.GetDirectoryName(projectPath); // global.json may change the version, so need to set working directory
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.UseShellExecute = false;

				// Capture output
				var lines = new List<string>();
				process.StartInfo.RedirectStandardOutput = true;
				process.OutputDataReceived += (s, e) => lines.Add(e.Data);

				// Execute the process
				process.Start();
				process.BeginOutputReadLine();
				var sw = new Stopwatch();
				sw.Start();
				while (!process.HasExited)
				{
					if (sw.ElapsedMilliseconds > 4000)
					{
						break;
					}
				}
				sw.Stop();
				process.Close();
				return lines;
			}
			finally
			{
				System.Environment.SetEnvironmentVariable(DOTNET_CLI_UI_LANGUAGE, originalCliLanguage);
			}
		}

		private static string ParseBasePath(List<string> lines)
		{
			if (lines == null || lines.Count == 0)
			{
				throw new InvalidOperationException("Could not get results from `dotnet --info` call");
			}

			foreach (var line in lines)
			{
				var colonIndex = line.IndexOf(':');
				if (colonIndex >= 0
				    && line.Substring(0, colonIndex).Trim().Equals("Base Path", StringComparison.OrdinalIgnoreCase))
				{
					var basePath = line.Substring(colonIndex + 1).Trim();

					// Make sure the base path matches the runtime architecture if on Windows
					// Note that this only works for the default installation locations under "Program Files"
					if (basePath.Contains(@"\Program Files\") && !System.Environment.Is64BitProcess)
					{
						var newBasePath = basePath.Replace(@"\Program Files\", @"\Program Files (x86)\");
						if (Directory.Exists(newBasePath))
						{
							basePath = newBasePath;
						}
					}
					else if (basePath.Contains(@"\Program Files (x86)\") && System.Environment.Is64BitProcess)
					{
						var newBasePath = basePath.Replace(@"\Program Files (x86)\", @"\Program Files\");
						if (Directory.Exists(newBasePath))
						{
							basePath = newBasePath;
						}
					}

					return basePath;
				}
			}

			throw new InvalidOperationException("Could not locate base path in `dotnet --info` results");
		}
	}
}
