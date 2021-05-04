// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
