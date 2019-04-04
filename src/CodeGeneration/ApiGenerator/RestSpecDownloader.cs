using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using CsQuery;
using ShellProgressBar;

namespace ApiGenerator
{
	public class RestSpecDownloader
	{
		private const string Core = "Core";
		private const string XpackTemp = "_Xpack";

		private static readonly ProgressBarOptions MainProgressBarOptions = new ProgressBarOptions { BackgroundColor = ConsoleColor.DarkGray };

		private static readonly Dictionary<string, string> OnlineSpecifications = new Dictionary<string, string>
		{
			{ Core, "https://github.com/elastic/elasticsearch/tree/{version}/rest-api-spec/src/main/resources/rest-api-spec/api" },
			{ XpackTemp, "https://github.com/elastic/elasticsearch/tree/{version}/x-pack/plugin/src/test/resources/rest-api-spec/api"}
		};

		private static readonly Dictionary<string, string> XpackFolderMapping = new Dictionary<string, string>
		{
			{ "ccr.", "Ccr" },
			{ "ilm.", "Ilm" },
			{ "indices.", "Indices" },
			{ "security.", "Security" },
			{ "xpack.graph.", "Graph" },
			{ "xpack.info", "Info" },
			{ "xpack.usage", "Info" },
			{ "xpack.license.", "License" },
			{ "xpack.migration.", "Migration" },
			{ "xpack.ml.", "MachineLearning" },
			{ "xpack.monitoring.", "Monitoring" },
			{ "xpack.rollup.", "Rollup" },
			{ "xpack.security.", "Security" },
			{ "xpack.sql.", "Sql" },
			{ "xpack.ssl.", "Ssl" },
			{ "xpack.watcher.", "Watcher" }
		};

		private static readonly ProgressBarOptions SubProgressBarOptions = new ProgressBarOptions
		{
			ForegroundColor = ConsoleColor.Cyan,
			ForegroundColorDone = ConsoleColor.DarkGreen,
			ProgressCharacter = '─',
			BackgroundColor = ConsoleColor.DarkGray,
		};

		private RestSpecDownloader(string branch)
		{
			var specifications =
				(from kv in OnlineSpecifications
					let url = kv.Value.Replace("{version}", branch)
					select new Specification { FolderOnDisk = kv.Key, Branch = branch, GithubListingUrl = url }).ToList();

			using (var pbar = new ProgressBar(specifications.Count, "Downloading specifications", MainProgressBarOptions))
			{
				foreach (var spec in specifications)
				{
					pbar.Message = $"Downloading rest-api-spec to {spec.FolderOnDisk} for branch {branch}";
					DownloadJsonDefinitions(spec, pbar);
					pbar.Tick($"Downloaded rest-api-spec to {spec.FolderOnDisk} for branch {branch}");
				}
			}

			// Move Xpack endpoints into their own folders
			var xpackTempPath = Path.Combine(CodeConfiguration.RestSpecificationFolder, XpackTemp);
			var xpackFiles = Directory.GetFiles(xpackTempPath, "*.json").ToList();
			using (var pbar = new ProgressBar(xpackFiles.Count, "Copying x-pack specifications", MainProgressBarOptions))
			{
				foreach (var file in xpackFiles)
				{
					var found = false;
					var info = new FileInfo(file);
					foreach (var mapping in XpackFolderMapping)
					{
						if (info.Name.StartsWith(mapping.Key))
						{
							var target = Path.Combine(CodeConfiguration.RestSpecificationFolder,
													  "XPack",
													  mapping.Value,
													  Path.GetFileName(info.FullName));
							if (File.Exists(target))
							{
								File.Delete(target);
							}
							File.Move(info.FullName, target);
							found = true;
						}
					}
					if (!found)
					{
						throw new Exception($"XPack file unmapped: {info.Name}");
					}
					pbar.Tick($"Moved {info.Name}");
				}
			}
			Directory.Delete(xpackTempPath, true);

			File.WriteAllText(CodeConfiguration.LastDownloadedVersionFile, branch);
		}

		public static RestSpecDownloader Download(string branch) => new RestSpecDownloader(branch);

		private void DownloadJsonDefinitions(Specification spec, IProgressBar pbar)
		{
			using (var client = new WebClient())
			{
				var html = client.DownloadString(spec.GithubListingUrl);
				FindJsonFilesOnListing(spec, html, pbar);
			}
		}

		private void FindJsonFilesOnListing(Specification spec, string html, IProgressBar pbar)
		{
			if (!Directory.Exists(CodeConfiguration.RestSpecificationFolder)) Directory.CreateDirectory(CodeConfiguration.RestSpecificationFolder);

			var dom = CQ.Create(html);

			WriteToEndpointsFolder(spec.FolderOnDisk, "root.html", html);

			var endpoints = dom[".js-navigation-open"]
				.Select(s => s.InnerText)
				.Where(s => !string.IsNullOrEmpty(s) && s.EndsWith(".json"))
				.ToList();

			using (var subBar = pbar.Spawn(endpoints.Count, "fetching individual json files", SubProgressBarOptions))
				endpoints.ForEach(s => WriteEndpointFile(spec, s, subBar));
		}

		private void WriteEndpointFile(Specification spec, string s, IProgressBar pbar)
		{
			var rawFile = spec.GithubDownloadUrl(s);
			using (var client = new WebClient())
			{
				var fileName = rawFile.Split('/').Last();
				var json = client.DownloadString(rawFile);
				WriteToEndpointsFolder(spec.FolderOnDisk, fileName, json);
				pbar.Tick($"Downloading {fileName}");
			}
		}

		private void WriteToEndpointsFolder(string folder, string filename, string contents)
		{
			var f = Path.Combine(CodeConfiguration.RestSpecificationFolder, folder);
			if (!Directory.Exists(f)) Directory.CreateDirectory(f);
			File.WriteAllText(f + "\\" + filename, contents);
		}

		private class Specification
		{
			public string Branch { get; set; }
			public string FolderOnDisk { get; set; }
			public string GithubListingUrl { get; set; }

			public string GithubDownloadUrl(string file) =>
				GithubListingUrl.Replace("github.com", "raw.githubusercontent.com").Replace("tree/", "") + "/" + file;
		}
	}
}
