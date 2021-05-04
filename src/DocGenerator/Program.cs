// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
 using System.Globalization;
 using System.IO;
using System.Linq;
using CommandLine;
using Newtonsoft.Json.Linq;

namespace DocGenerator
{
	public static class Program
	{
		static Program()
		{
			CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-us");
			var root = new DirectoryInfo(Directory.GetCurrentDirectory());

			do
			{
				if (File.Exists(Path.Combine(root.FullName, "global.json")))
					break;
				root = root.Parent;
			} while (root != null && root.Parent != root.Root);

			if (root == null || root.Parent == root.Root)
				throw new Exception("Expected to find a global.json in a parent folder");


			var r = root.FullName;
			var globalJson = Path.Combine(r, "global.json");
			InputDirPath = Path.Combine(r, "src");
			OutputDirPath = Path.Combine(r, "docs");
			TmpOutputDirPath = Path.Combine(r, "docs-temp");

			var jObject = JObject.Parse(File.ReadAllText(globalJson));

			DocVersion = string.Join(".", jObject["doc_current"]
				.Value<string>()
				.Split(".")
				.Take(2));

			BranchName =
				jObject.ContainsKey("doc_branch")
					? jObject["doc_branch"].Value<string>()
					: "master";
		}

		/// <summary>
		/// The branch name to include in generated docs to link back to the original source file
		/// </summary>
		public static string BranchName { get; private set; }

		/// <summary>
		/// The Elasticsearch documentation version to link to
		/// </summary>
		public static string DocVersion { get; private set; }

		public static string InputDirPath { get; }

		public static string OutputDirPath { get; }

		public static string TmpOutputDirPath { get; }

		private static int Main(string[] args) =>
			Parser.Default.ParseArguments<DocGeneratorOptions>(args)
				.MapResult(
					opts =>
					{
						try
						{
							if (!string.IsNullOrEmpty(opts.BranchName))
								BranchName = opts.BranchName;

							if (!string.IsNullOrEmpty(opts.DocVersion))
								DocVersion = opts.DocVersion;

							Console.WriteLine($"Using branch name {BranchName} in documentation");
							Console.WriteLine($"Using doc reference version {DocVersion} in documentation");

							return LitUp.GoAsync(args).GetAwaiter().GetResult();
						}
						catch (AggregateException ae)
						{
							var ex = ae.InnerException ?? ae;
							Console.WriteLine(ex.Message);
							return 1;
						}
					},
					errs => 1);
	}

	public class DocGeneratorOptions
	{
		[Option('b', "branch", Required = false, HelpText = "The version that appears in generated from source link")]
		public string BranchName { get; set; }

		[Option('d', "docs", Required = false, HelpText = "The version that links to the Elasticsearch reference documentation")]
		public string DocVersion { get; set; }
	}
}
