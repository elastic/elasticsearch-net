using System;
using System.IO;

namespace ApiGenerator
{
	public static class Program
	{
		private static readonly string DownloadBranch = "master";

		private static void Main(string[] args)
		{
			var redownloadCoreSpecification = false;
			var generateCode = false;
			var downloadBranch = DownloadBranch;

			var answer = "invalid";
			while (answer != "y" && answer != "n" && answer != "")
			{
				Console.Write("Download online rest specifications? [Y/N] (default N): ");
				answer = Console.ReadLine()?.Trim().ToLowerInvariant();
				redownloadCoreSpecification = answer == "y" || answer == "";
			}

			if (redownloadCoreSpecification)
			{
				Console.Write($"Branch to download specification from (default {downloadBranch}): ");
				var readBranch = Console.ReadLine()?.Trim();
				if (!string.IsNullOrEmpty(readBranch)) downloadBranch = readBranch;
			}
			else
			{
				// read last downloaded branch from file.
				if (File.Exists(CodeConfiguration.LastDownloadedVersionFile))
					downloadBranch = File.ReadAllText(CodeConfiguration.LastDownloadedVersionFile);
			}

			if (string.IsNullOrEmpty(downloadBranch))
				downloadBranch = DownloadBranch;

			if (redownloadCoreSpecification)
				RestSpecDownloader.Download(downloadBranch);

			answer = "invalid";
			while (answer != "y" && answer != "n" && answer != "")
			{
				Console.Write("Generate code from the specification files on disk? [Y/N] (default Y): ");
				answer = Console.ReadLine()?.Trim().ToLowerInvariant();
				generateCode = answer == "y" || answer == "";
			}
			if (generateCode)
				ApiGenerator.Generate(downloadBranch, "Core", "XPack");
		}
	}
}
