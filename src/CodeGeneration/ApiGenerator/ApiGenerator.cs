using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ApiGenerator.Domain;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json.Linq;
using RazorLight;
using RazorLight.Generation;
using RazorLight.Razor;
using ShellProgressBar;

namespace ApiGenerator
{
	public partial class ApiGenerator
	{
		private static readonly RazorLightEngine Razor = new RazorLightEngineBuilder()
			.UseProject(new FileSystemRazorProject(Path.GetFullPath(GeneratorLocations.ViewFolder)))
			.UseMemoryCachingProvider()
			.Build();

		public static List<string> Warnings { get; private set; }

		public static void Generate(string downloadBranch, params string[] folders)
		{
			Warnings = new List<string>();
			var spec = CreateRestApiSpecModel(downloadBranch, folders);
			var actions = new Dictionary<Action<RestApiSpec>, string>
			{
				{ GenerateClientInterface, "Client interface" },
				{ GenerateRequestParameters, "Request parameters" },
				{ GenerateDescriptors, "Descriptors" },
				{ GenerateRequests, "Requests" },
				{ GenerateEnums, "Enums" },
				{ GenerateRawClient, "Lowlevel client" },
			};

			using (var pbar = new ProgressBar(actions.Count, "Generating code", new ProgressBarOptions { BackgroundColor = ConsoleColor.DarkGray }))
			{
				foreach (var kv in actions)
				{
					pbar.Message = "Generating " + kv.Value;
					kv.Key(spec);
					pbar.Tick("Generated " + kv.Value);
				}
			}

			if (Warnings.Count == 0) return;

			Console.ForegroundColor = ConsoleColor.Yellow;
			foreach (var warning in Warnings.Distinct().OrderBy(w => w))
				Console.WriteLine(warning);
			Console.ResetColor();
		}

		private static RestApiSpec CreateRestApiSpecModel(string downloadBranch, string[] folders)
		{
			var directories = Directory.GetDirectories(GeneratorLocations.RestSpecificationFolder, "*", SearchOption.AllDirectories)
				.Where(f => folders == null || folders.Length == 0 || folders.Contains(new DirectoryInfo(f).Name))
				.OrderBy(f=>new FileInfo(f).Name)
				.ToList();

			var endpoints = new SortedDictionary<string, ApiEndpoint>();
			var seenFiles = new HashSet<string>();
			using (var pbar = new ProgressBar(directories.Count, $"Listing {directories.Count} directories",
				new ProgressBarOptions { BackgroundColor = ConsoleColor.DarkGray }))
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
			var wrongMapsApi = CodeConfiguration.ApiNameMapping.Where(k =>!string.IsNullOrWhiteSpace(k.Key) && !seenFiles.Contains(k.Key));
			foreach (var (key, value) in wrongMapsApi)
			{
				var isIgnored = CodeConfiguration.IgnoredApis.Contains($"{value}.json");
				if (isIgnored) Warnings.Add($"{value} uses MapsApi: {key} ignored in ${nameof(CodeConfiguration)}.{nameof(CodeConfiguration.IgnoredApis)}");
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


		private static string DoRazor(string name, string template, RestApiSpec model)
		{
			try
			{
				return Razor.CompileRenderStringAsync<RestApiSpec>(name, template,  model).GetAwaiter().GetResult();
			}
			catch (TemplateGenerationException e)
			{
				foreach (var d in e.Diagnostics)
				{
					Console.WriteLine(d.GetMessage());
				}
				throw e;
			}
		}

		private static void WriteFormattedCsharpFile(string path, string contents)
		{
			var tree = CSharpSyntaxTree.ParseText(contents);
			var root = tree.GetRoot().NormalizeWhitespace(indentation:"\t", "\n", elasticTrivia: false);
			contents = root.ToFullString();
			File.WriteAllText(path, contents);
		}


		private static void GenerateClientInterface(RestApiSpec model)
		{
			var targetFile = GeneratorLocations.EsNetFolder + @"IElasticLowLevelClient.Generated.cs";
			var source = DoRazor(nameof(GenerateClientInterface),
				File.ReadAllText(GeneratorLocations.ViewFolder + @"IElasticLowLevelClient.Generated.cshtml"), model);
			WriteFormattedCsharpFile(targetFile, source);
		}

		private static void GenerateRawClient(RestApiSpec model)
		{
			var targetFile = GeneratorLocations.EsNetFolder + @"ElasticLowLevelClient.Generated.cs";
			var source = DoRazor(nameof(GenerateRawClient),
				File.ReadAllText(GeneratorLocations.ViewFolder + @"ElasticLowLevelClient.Generated.cshtml"), model);
			WriteFormattedCsharpFile(targetFile, source);
		}

		private static void GenerateDescriptors(RestApiSpec model)
		{
			var targetFile = GeneratorLocations.NestFolder + @"_Generated/_Descriptors.generated.cs";
			var source = DoRazor(nameof(GenerateDescriptors), File.ReadAllText(GeneratorLocations.ViewFolder + @"_Descriptors.Generated.cshtml"),
				model);
			WriteFormattedCsharpFile(targetFile, source);
		}

		private static void GenerateRequests(RestApiSpec model)
		{
			var targetFile = GeneratorLocations.NestFolder + @"_Generated/_Requests.generated.cs";
			var source = DoRazor(nameof(GenerateRequests), File.ReadAllText(GeneratorLocations.ViewFolder + @"_Requests.Generated.cshtml"), model);
			WriteFormattedCsharpFile(targetFile, source);
		}

		private static void GenerateRequestParameters(RestApiSpec model)
		{
			var targetFile = GeneratorLocations.EsNetFolder + @"Domain/RequestParameters/RequestParameters.Generated.cs";
			var source = DoRazor(nameof(GenerateRequestParameters),
				File.ReadAllText(GeneratorLocations.ViewFolder + @"RequestParameters.Generated.cshtml"), model);
			WriteFormattedCsharpFile(targetFile, source);
		}

		private static void GenerateEnums(RestApiSpec model)
		{
			var targetFile = GeneratorLocations.EsNetFolder + @"Domain/Enums.Generated.cs";
			var source = DoRazor(nameof(GenerateEnums), File.ReadAllText(GeneratorLocations.ViewFolder + @"Enums.Generated.cshtml"), model);
			WriteFormattedCsharpFile(targetFile, source);
		}
	}
}
