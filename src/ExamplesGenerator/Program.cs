/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Diagnostics;
using CommandLine;

namespace ExamplesGenerator
{
	internal class Program
	{
		private static int Main(string[] args) =>
			Parser.Default.ParseArguments<AsciiDocOptions, CSharpOptions>(args)
				.MapResult(
					(AsciiDocOptions opts) => RunAsciiDocAndReturnExitCode(opts),
					(CSharpOptions opts) => RunCSharpAndReturnExitCode(opts),
					errs => 1);

		private static int RunAsciiDocAndReturnExitCode(AsciiDocOptions opts)
		{
			var branchName = !string.IsNullOrEmpty(opts.BranchName)
				? opts.BranchName
				: GetBranchNameFromGit();

			var examples = CSharpParser.ParseImplementedExamples();
			AsciiDocGenerator.GenerateExampleAsciiDoc(examples, branchName);
			return 0;
		}

		private static string GetBranchNameFromGit()
		{
			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					UseShellExecute = false,
					RedirectStandardOutput = true,
					FileName = "git.exe",
					CreateNoWindow = true,
					WorkingDirectory = Environment.CurrentDirectory,
					Arguments = "rev-parse --abbrev-ref HEAD"
				}
			};

			try
			{
				process.Start();
				var branchName = process.StandardOutput.ReadToEnd().Trim();
				process.WaitForExit();
				return branchName;
			}
			catch (Exception)
			{
				return "master";
			}
			finally
			{
				process.Dispose();
			}
		}

		private static int RunCSharpAndReturnExitCode(CSharpOptions opts)
		{
			var pages = AsciiDocParser.ParsePages(opts.Path);
			CSharpGenerator.GenerateExampleClasses(pages);
			return 0;
		}
	}

	[Verb("asciidoc", HelpText = "Generate asciidoc client example files from the C# Examples classes")]
	public class AsciiDocOptions
	{
		[Option('b', "branch", Required = false, HelpText = "The version that appears in generated from source link")]
		public string BranchName { get; set; }
	}

	[Verb("csharp", HelpText = "Generate C# Examples classes from the asciidoc master reference")]
	public class CSharpOptions
	{
		[Option('p', "path", Required = true, HelpText = "Path to the master reference file, for example: https://raw.githubusercontent.com/elastic/built-docs/master/raw/en/elasticsearch/reference/master/alternatives_report.json")]
		public string Path { get; set; }
	}
}
