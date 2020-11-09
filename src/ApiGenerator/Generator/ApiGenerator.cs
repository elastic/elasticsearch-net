// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
 using System.Threading;
 using System.Threading.Tasks;
using ApiGenerator.Configuration;
using ApiGenerator.Domain;
using ApiGenerator.Domain.Specification;
using ApiGenerator.Generator.Razor;
using Newtonsoft.Json.Linq;
using ShellProgressBar;

namespace ApiGenerator.Generator
{
	public class ApiGenerator
	{
		public static List<string> Warnings { get; private set; } = new List<string>();

		public static async Task Generate(string downloadBranch, bool lowLevelOnly, RestApiSpec spec, CancellationToken token)
		{
			static async Task DoGenerate(ICollection<RazorGeneratorBase> generators, RestApiSpec restApiSpec, bool highLevel, CancellationToken token)
			{
				var pbarOpts = new ProgressBarOptions { ProgressCharacter = '─', BackgroundColor = ConsoleColor.Yellow };
				var message = $"Generating {(highLevel ? "high" : "low")} level code";
				using var pbar = new ProgressBar(generators.Count, message, pbarOpts);
				foreach (var generator in generators)
				{
					pbar.Message = "Generating " + generator.Title;
					await generator.Generate(restApiSpec, pbar, token);
					pbar.Tick("Generated " + generator.Title);
				}
			}


			var lowLevelGenerators = new List<RazorGeneratorBase>
			{
				//low level client
				new LowLevelClientInterfaceGenerator(),
				new LowLevelClientImplementationGenerator(),
				new RequestParametersGenerator(),
				new EnumsGenerator(),
				new ApiUrlsLookupsGenerator(),
			};

			var highLevelGenerators = new List<RazorGeneratorBase>
			{
				//high level client
				new HighLevelClientInterfaceGenerator(),
				new HighLevelClientImplementationGenerator(),
				new DescriptorsGenerator(),
				new RequestsGenerator(),
			};

			await DoGenerate(lowLevelGenerators, spec, highLevel: false, token);
			if (!lowLevelOnly)
				await DoGenerate(highLevelGenerators, spec, highLevel: true, token);

		}

		public static RestApiSpec CreateRestApiSpecModel(string downloadBranch, params string[] folders)
		{
			var directories = Directory.GetDirectories(GeneratorLocations.RestSpecificationFolder, "*", SearchOption.AllDirectories)
				.Where(f => folders == null || folders.Length == 0 || folders.Contains(new DirectoryInfo(f).Name))
				.OrderBy(f => new FileInfo(f).Name)
				.ToList();

			var endpoints = new SortedDictionary<string, ApiEndpoint>();
			var seenFiles = new HashSet<string>();
			using (var pbar = new ProgressBar(directories.Count, $"Listing {directories.Count} directories",
				new ProgressBarOptions { ProgressCharacter = '─', BackgroundColor = ConsoleColor.DarkGray, CollapseWhenFinished = false }))
			{
				var folderFiles = directories.Select(dir =>
					Directory.GetFiles(dir)
						.Where(f => f.EndsWith(".json") && !CodeConfiguration.IgnoredApis.Contains(new FileInfo(f).Name))
						.ToList()
				);
				var commonFile = Path.Combine(GeneratorLocations.RestSpecificationFolder, "Core", "_common.json");
				if (!File.Exists(commonFile)) throw new Exception($"Expected to find {commonFile}");

				RestApiSpec.CommonApiQueryParameters = CreateCommonApiQueryParameters(commonFile);

				foreach (var jsonFiles in folderFiles)
				{
					using (var fileProgress = pbar.Spawn(jsonFiles.Count, $"Listing {jsonFiles.Count} files",
						new ProgressBarOptions { ProgressCharacter = '─', BackgroundColor = ConsoleColor.DarkGray }))
					{
						foreach (var file in jsonFiles)
						{
							if (file.EndsWith("_common.json")) continue;
							else if (file.EndsWith(".patch.json")) continue;
							else
							{
								var endpoint = ApiEndpointFactory.FromFile(file);
								seenFiles.Add(Path.GetFileNameWithoutExtension(file));
								endpoints.Add(endpoint.Name, endpoint);
							}

							fileProgress.Tick();
						}
					}
					pbar.Tick();
				}
			}
			var wrongMapsApi = CodeConfiguration.ApiNameMapping.Where(k => !string.IsNullOrWhiteSpace(k.Key) && !seenFiles.Contains(k.Key));
			foreach (var (key, value) in wrongMapsApi)
			{
				var isIgnored = CodeConfiguration.IgnoredApis.Contains($"{value}.json");
				if (isIgnored)
					Warnings.Add($"{value} uses MapsApi: {key} ignored in ${nameof(CodeConfiguration)}.{nameof(CodeConfiguration.IgnoredApis)}");
				else Warnings.Add($"{value} uses MapsApi: {key} which does not exist");
			}

			return new RestApiSpec { Endpoints = endpoints, Commit = downloadBranch };
		}

		private static SortedDictionary<string, QueryParameters> CreateCommonApiQueryParameters(string jsonFile)
		{
			var json = File.ReadAllText(jsonFile);
			var jobject = JObject.Parse(json);
			var commonParameters = jobject.Property("params").Value.ToObject<Dictionary<string, QueryParameters>>();
			return ApiQueryParametersPatcher.Patch(null, commonParameters, null, false);
		}
	}
}
