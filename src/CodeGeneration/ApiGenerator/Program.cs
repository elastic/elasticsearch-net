using System;
using System.IO;

namespace ApiGenerator
{
	public static class Program
	{
		private static readonly string DownloadBranch = "v2.4.4";

		static void Main(string[] args)
		{
			bool redownloadCoreSpecification = false;
			string downloadBranch = DownloadBranch;

			var answer = "invalid";
			while (answer != "y" && answer != "n" && answer != "")
			{
				Console.Write("Download online rest specifications? [Y/N] (default N): ");
				answer = Console.ReadLine()?.Trim().ToLowerInvariant();
				redownloadCoreSpecification = answer == "y";
			}
			if (redownloadCoreSpecification)
			{
				Console.Write($"Branch to download specification from (default {DownloadBranch}): ");
				downloadBranch = Console.ReadLine()?.Trim();
			}
			else
			{
				// read last downloaded branch from file.
				if (File.Exists(CodeConfiguration.LastDownloadedVersionFile))
				{
					downloadBranch = File.ReadAllText(CodeConfiguration.LastDownloadedVersionFile);
				}
			}

			if (string.IsNullOrEmpty(downloadBranch))
				downloadBranch = DownloadBranch;

			if (redownloadCoreSpecification)
				RestSpecDownloader.Download(downloadBranch);

			ApiGenerator.Generate(downloadBranch, "Core", "DeleteByQuery", "Graph", "License", "Shield", "Watcher");
			//ApiGenerator.Generate(); //generates everything under ApiSpecification
		}

	}
}
