using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiGenerator
{
	public static class Program
	{
		static void Main(string[] args)
		{
			bool redownloadCoreSpecification = false;
			string downloadBranch = "5.x";

			var answer = "invalid";
			while (answer != "y" && answer != "n" && answer != "")
			{
				Console.Write("Download online rest specifications? [Y/N] (default N): ");
				answer = Console.ReadLine()?.Trim().ToLowerInvariant();
				redownloadCoreSpecification = answer == "y";
			}

			if (redownloadCoreSpecification)
			{
				Console.Write("Branch to download specification from (default master): ");
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

			if (redownloadCoreSpecification)
				RestSpecDownloader.Download(downloadBranch);

			ApiGenerator.Generate(downloadBranch, "Core", "Graph", "License", "Security", "Watcher");

			//ApiGenerator.Generate(); //generates everything under ApiSpecification
		}

	}
}
