// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ApiGenerator.Configuration;
using CsQuery;
using ShellProgressBar;

namespace ApiGenerator
{
	public class RestSpecDownloader
	{
		private readonly string _branch;
		private static readonly ProgressBarOptions MainProgressBarOptions = new ProgressBarOptions
		{
			BackgroundColor = ConsoleColor.DarkGray,
			ProgressCharacter = '─',
		};

		private static string CommitsUrl = "https://github.com/elastic/elasticsearch/commits/{branch}";
		private static readonly Dictionary<string, string> OnlineSpecifications = new Dictionary<string, string>
		{
			{ "Core", "https://github.com/elastic/elasticsearch/tree/{ref}/rest-api-spec/src/main/resources/rest-api-spec/api" },
			{ "XPack", "https://github.com/elastic/elasticsearch/tree/{ref}/x-pack/plugin/src/test/resources/rest-api-spec/api"}
		};

		private static readonly ProgressBarOptions SubProgressBarOptions = new ProgressBarOptions
		{
			ForegroundColor = ConsoleColor.Cyan,
			ForegroundColorDone = ConsoleColor.DarkGreen,
			ProgressCharacter = '─',
			BackgroundColor = ConsoleColor.DarkGray,
		};

		private RestSpecDownloader(string branch) => _branch = branch;

		private async Task DownloadAsync(CancellationToken token)
		{
			var @ref = await ResolveLastRef(_branch, token);

			var specifications =
				(from kv in OnlineSpecifications
					let url = kv.Value.Replace("{ref}", @ref)
					select new Specification { FolderOnDisk = kv.Key, Branch = _branch, Ref = @ref, GithubListingUrl = url }).ToList();

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
					pbar.Message = $"Downloading rest-api-spec to {spec.FolderOnDisk} for branch {_branch}";
					await DownloadJsonDefinitions(spec, pbar, token);
					pbar.Tick($"Downloaded rest-api-spec to {spec.FolderOnDisk} for branch {_branch}");
				}
			}

			await File.WriteAllTextAsync(GeneratorLocations.LastDownloadedRef, @ref, token);

		}

		public static Task DownloadAsync(string branch, CancellationToken token = default) => new RestSpecDownloader(branch).DownloadAsync(token);

		private static async Task<string> ResolveLastRef(string branch, CancellationToken token)
		{
			var response = await Http.GetAsync(CommitsUrl.Replace("{branch}", branch), token);
			var html = await response.Content.ReadAsStringAsync();
			var dom = CQ.Create(html);

			var prefix = "/elastic/elasticsearch/commit/";
			var commit = dom["a.text-mono"]
				.Select(s => s.GetAttribute("href"))
				.Where(a => a.StartsWith(prefix))
				.Select(a => a.Replace(prefix, ""))
				.FirstOrDefault()
				?? throw new Exception($"Can not locate the latest commit on branch: {branch}");
			return commit;
		}

		private static readonly HttpClient Http = new HttpClient();
		private static async Task DownloadJsonDefinitions(Specification spec, IProgressBar pbar, CancellationToken token)
		{
			var response = await Http.GetAsync(spec.GithubListingUrl, token);
			var html = await response.Content.ReadAsStringAsync();

			await FindJsonFilesOnListing(spec, html, pbar, token);
		}

		private static async Task FindJsonFilesOnListing(Specification spec, string html, IProgressBar pbar, CancellationToken token)
		{
			if (!Directory.Exists(GeneratorLocations.RestSpecificationFolder))
				Directory.CreateDirectory(GeneratorLocations.RestSpecificationFolder);

			var dom = CQ.Create(html);

			var endpoints = dom[".js-navigation-open"]
				.Select(s => s.InnerText)
				.Where(s => !string.IsNullOrEmpty(s) && s.EndsWith(".json"))
				.ToList();

			using var subBar = pbar.Spawn(endpoints.Count, "fetching individual json files", SubProgressBarOptions);
			foreach (var e in endpoints)
				await WriteEndpointFile(spec, e, subBar, token);
		}

		private static async Task WriteEndpointFile(Specification spec, string s, IProgressBar pbar, CancellationToken token)
		{
			var rawFile = spec.GithubDownloadUrl(s);
			var fileName = rawFile.Split('/').Last();

			var response = await Http.GetAsync(rawFile, token);
			var json = await response.Content.ReadAsStringAsync();
			await WriteToEndpointsFolder(spec.FolderOnDisk, fileName, json, token);
			pbar.Tick($"Downloading {fileName}");
		}

		private static async Task WriteToEndpointsFolder(string folder, string filename, string contents, CancellationToken token)
		{
			var f = Path.Combine(GeneratorLocations.RestSpecificationFolder, folder);
			if (!Directory.Exists(f)) Directory.CreateDirectory(f);
			var target = Path.Combine(f, filename);
			await File.WriteAllTextAsync(target, contents, token);
		}

		private class Specification
		{
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public string Branch { get; set; }
			public string Ref { get; set; }
			public string FolderOnDisk { get; set; }
			public string GithubListingUrl { get; set; }

			public string GithubDownloadUrl(string file) =>
				GithubListingUrl.Replace("github.com", "raw.githubusercontent.com").Replace("tree/", "") + "/" + file;
		}
	}
}
