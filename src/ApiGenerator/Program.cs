using System;
using System.IO;
using System.Threading.Tasks;
using ApiGenerator.Configuration;

namespace ApiGenerator
{
	public static class Program
	{
		private static readonly string DownloadBranch = "master";

		// ReSharper disable once UnusedParameter.Local
		private static async Task Main(string[] args)
		{

			var redownloadCoreSpecification = Ask("Download online rest specifications?", false);

			var downloadBranch = DownloadBranch;
			if (redownloadCoreSpecification)
			{
				Console.Write($"Branch to download specification from (default {downloadBranch}): ");
				var readBranch = Console.ReadLine()?.Trim();
				if (!string.IsNullOrEmpty(readBranch)) downloadBranch = readBranch;
			}
			else
			{
				// read last downloaded branch from file.
				if (File.Exists(GeneratorLocations.LastDownloadedVersionFile))
					downloadBranch = File.ReadAllText(GeneratorLocations.LastDownloadedVersionFile);
			}

			if (string.IsNullOrEmpty(downloadBranch))
				downloadBranch = DownloadBranch;

			var generateCode = Ask("Generate code from the specification files on disk?", true);
			var lowLevelOnly = generateCode && Ask("Generate low level client only?", false);

			if (redownloadCoreSpecification)
				RestSpecDownloader.Download(downloadBranch);

			if (generateCode)
				await Generator.ApiGenerator.Generate(downloadBranch, lowLevelOnly, "Core", "XPack");
		}

		private static bool Ask(string question, bool defaultAnswer = true)
		{
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
