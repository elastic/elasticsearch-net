// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using ApiGenerator.Configuration;
using CsQuery;
using ShellProgressBar;

namespace ApiGenerator
{
	public class RestSpecDownloader
	{
		private static readonly ProgressBarOptions MainProgressBarOptions = new ProgressBarOptions { BackgroundColor = ConsoleColor.DarkGray };

		private static readonly Dictionary<string, string> OnlineSpecifications = new Dictionary<string, string>
		{
			{ "Core", "https://github.com/elastic/elasticsearch/tree/{version}/rest-api-spec/src/main/resources/rest-api-spec/api" },
			{ "XPack", "https://github.com/elastic/elasticsearch/tree/{version}/x-pack/plugin/src/test/resources/rest-api-spec/api"}
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
					var specFolderOnDisk = Path.Combine(GeneratorLocations.RestSpecificationFolder, spec.FolderOnDisk);
					if (Directory.Exists(specFolderOnDisk))
					{
						Directory.Delete(specFolderOnDisk, true);
						pbar.WriteLine($"Deleted target spec folder, before downloading new copy: {specFolderOnDisk}");
					}
					pbar.Message = $"Downloading rest-api-spec to {spec.FolderOnDisk} for branch {branch}";
					DownloadJsonDefinitions(spec, pbar);
					pbar.Tick($"Downloaded rest-api-spec to {spec.FolderOnDisk} for branch {branch}");
				}
			}

			File.WriteAllText(GeneratorLocations.LastDownloadedVersionFile, branch);
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
			if (!Directory.Exists(GeneratorLocations.RestSpecificationFolder))
				Directory.CreateDirectory(GeneratorLocations.RestSpecificationFolder);

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
			var f = Path.Combine(GeneratorLocations.RestSpecificationFolder, folder);
			if (!Directory.Exists(f)) Directory.CreateDirectory(f);
			var target = Path.Combine(f, filename);
			File.WriteAllText(target, contents);
		}

		private class Specification
		{
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public string Branch { get; set; }
			public string FolderOnDisk { get; set; }
			public string GithubListingUrl { get; set; }

			public string GithubDownloadUrl(string file) =>
				GithubListingUrl.Replace("github.com", "raw.githubusercontent.com").Replace("tree/", "") + "/" + file;
		}
	}
}
