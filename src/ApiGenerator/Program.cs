// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiGenerator.Configuration;
using Spectre.Console;

#pragma warning disable 162

namespace ApiGenerator
{
	public static class Program
	{
		private static bool Interactive { get; set; } = false;

		/// <summary>
		/// A main function can also take <see cref="CancellationToken"/> which is hooked up to support termination (e.g CTRL+C)
		/// </summary>
		/// <param name="branch">The stack's branch we are targeting the generation for</param>
		/// <param name="interactive">Run the generation interactively, this will ignore all flags</param>
		/// <param name="download">Whether to download the specs or use an already downloaded copy</param>
		/// <param name="includeHighLevel">Also generate the high level client (NEST)</param>
		/// <param name="skipGenerate">Only download the specs, skip all code generation</param>
		/// <param name="token"></param>
		/// <returns></returns>
		private static async Task<int> Main(
			string branch, bool interactive = false, bool download = false, bool includeHighLevel = false, bool skipGenerate = false
			, CancellationToken token = default)
		{
			Interactive = interactive;
			try
			{
				if (string.IsNullOrEmpty(branch))
				{

					throw new ArgumentException("--branch can not be null");
				}
				await Generate(download, branch, includeHighLevel, skipGenerate, token);
			}
			catch (OperationCanceledException)
			{
				AnsiConsole.WriteLine();
				AnsiConsole.Render(new Rule("[b white on orange4_1] Cancelled [/]").LeftAligned());
				AnsiConsole.WriteLine();
				return 1;
			}
			catch (Exception ex)
			{
				AnsiConsole.WriteLine();
				AnsiConsole.Render(new Rule("[b white on darkred] Exception [/]")
				{
					Alignment = Justify.Left,
				});
				AnsiConsole.WriteLine();
				AnsiConsole.WriteException(ex);
				return 1;
			}
			return 0;
		}

		private static async Task<int> Generate(bool download, string branch, bool includeHighLevel, bool skipGenerate, CancellationToken token = default)
		{
			var redownloadCoreSpecification = Ask("Download online rest specifications?", download);

			var downloadBranch = branch;
			if (Interactive && redownloadCoreSpecification)
			{
				Console.Write($"Branch to download specification from (default {downloadBranch}): ");
				var readBranch = Console.ReadLine()?.Trim();
				if (!string.IsNullOrEmpty(readBranch))
					downloadBranch = readBranch;
			}

			if (string.IsNullOrEmpty(downloadBranch))
				throw new Exception($"Branch to download from is null or empty");

			var generateCode = Ask("Generate code from the specification files on disk?", !skipGenerate);
			var lowLevelOnly = generateCode && Ask("Generate low level client only?", !includeHighLevel);

			static string YesNo(bool value) => value ? "[bold green]Yes[/]" : "[grey]No[/]";
			var grid = new Grid()
				.AddColumn(new GridColumn().PadRight(4))
				.AddColumn()
				.AddRow("[b]Download specification[/]", $"{YesNo(download)}")
				.AddRow("[b]Download branch[/]", $"{downloadBranch}")
				.AddRow("[b]Generate code from specification[/]", $"{YesNo(generateCode)}")
				.AddRow("[b]Include high level client[/]", $"{YesNo(!lowLevelOnly)}");

			Console.WriteLine();
			AnsiConsole.Render(
				new Panel(grid)
					.Header(new PanelHeader("[b white on chartreuse4] Elasticsearch .NET client API generator [/]", Justify.Left))
			);
			Console.WriteLine();

			if (redownloadCoreSpecification)
			{
				Console.WriteLine();
				AnsiConsole.Render(new Rule("[b white on chartreuse4] Downloading specification [/]").LeftAligned());
				Console.WriteLine();
				await RestSpecDownloader.DownloadAsync(downloadBranch, token);
			}

			if (!generateCode) return 0;

			Console.WriteLine();
			AnsiConsole.Render(new Rule("[b white on chartreuse4] Loading specification [/]").LeftAligned());
			Console.WriteLine();

			var spec = Generator.ApiGenerator.CreateRestApiSpecModel(downloadBranch, "Core", "XPack");
			if (!lowLevelOnly)
			{
				foreach (var endpoint in spec.Endpoints.Select(e => e.Value.FileName))
				{
					if (CodeConfiguration.IsNewHighLevelApi(endpoint)
						&& Ask($"Generate highlevel code for new api {endpoint}", false))
						CodeConfiguration.EnableHighLevelCodeGen.Add(endpoint);

				}
			}

			Console.WriteLine();
			AnsiConsole.Render(new Rule("[b white on chartreuse4] Generating code [/]").LeftAligned());
			Console.WriteLine();

			await Generator.ApiGenerator.Generate(downloadBranch, lowLevelOnly, spec, token);

			var warnings = Generator.ApiGenerator.Warnings;
			if (warnings.Count > 0)
			{
				Console.WriteLine();
				AnsiConsole.Render(new Rule("[b black on yellow] Specification warnings [/]").LeftAligned());
				Console.WriteLine();

				foreach (var warning in warnings.Distinct().OrderBy(w => w))
					AnsiConsole.MarkupLine(" {0} [yellow] {1} [/] ", Emoji.Known.Warning, warning);
			}

			return 0;
		}

		private static bool Ask(string question, bool defaultAnswer = true)
		{
			if (!Interactive) return defaultAnswer;

			var answer = "invalid";
			var defaultResponse = defaultAnswer ? "y" : "n";

			while (answer != "y" && answer != "n" && answer != "")
			{
				Console.Write($"{question}[y/N] (default {defaultResponse}): ");
				answer = Console.ReadLine()?.Trim().ToLowerInvariant();
				if (string.IsNullOrWhiteSpace(answer)) answer = defaultResponse;
				defaultAnswer = answer == "y";
			}
			return defaultAnswer;
		}
	}
}
