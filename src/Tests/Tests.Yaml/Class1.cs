using System;
using System.IO;
using System.Threading.Tasks;

namespace Tests.Yaml
{

	public static class Program
	{
		private static readonly string DownloadBranch = "master";

		public static async Task Main()
		{
			var redownloadCoreSpecification = false;
			var generateCode = false;
			var downloadBranch = DownloadBranch;

			Console.WriteLine(GeneratorLocations.Root);
			return;

			var answer = "invalid";
			while (answer != "y" && answer != "n" && answer != "")
			{
				Console.Write("Download online yaml test specifications? [y/N] (default N): ");
				answer = Console.ReadLine()?.Trim().ToLowerInvariant();
				redownloadCoreSpecification = answer == "y";
			}

			if (redownloadCoreSpecification)
			{
				Console.Write($"Branch to download yaml test specifications from (default {downloadBranch}): ");
				var readBranch = Console.ReadLine()?.Trim();
				if (!string.IsNullOrEmpty(readBranch)) downloadBranch = readBranch;
			}
			else
			{
				// read last downloaded branch from file.
				//if (File.Exists(GeneratorLocations.LastDownloadedVersionFile))
					//downloadBranch = File.ReadAllText(GeneratorLocations.LastDownloadedVersionFile);
			}

			if (string.IsNullOrEmpty(downloadBranch))
				downloadBranch = DownloadBranch;

			if (redownloadCoreSpecification)
				YamlTestDownloader.Download(downloadBranch);

			answer = "invalid";
			while (answer != "y" && answer != "n" && answer != "")
			{
				Console.Write("Generate code from the specification files on disk? [Y/n] (default Y): ");
				answer = Console.ReadLine()?.Trim().ToLowerInvariant();
				generateCode = answer == "y" || answer == "";
			}
			//if (generateCode)
				//await Generator.ApiGenerator.Generate(downloadBranch, "Core", "XPack");
		}
	}

}
