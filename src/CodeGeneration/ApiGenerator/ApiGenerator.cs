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
			var actions = new Dictionary<Action<RestApiSpec, ProgressBar>, string>
			{
				{ GenerateEnums, "Enums" },
				{ GenerateLowLevelClientInterface, "Client interface" },
				{ GenerateLowLevelClient, "Lowlevel client" },
				{ GenerateRequestParameters, "Request parameters" },
				{ GenerateDescriptors, "Descriptors" },
				{ GenerateRequests, "Requests" },
			};

			using (var pbar = new ProgressBar(actions.Count, "Generating code", new ProgressBarOptions { BackgroundColor = ConsoleColor.DarkGray }))
			{
				foreach (var kv in actions)
				{
					pbar.Message = "Generating " + kv.Value;
					kv.Key(spec, pbar);
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


		private static string DoRazor<TModel>(string name, string template, TModel model)
		{
			try
			{
				return Razor.CompileRenderStringAsync(name, template,  model).GetAwaiter().GetResult();
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


		private static void GenerateLowLevelClientInterface(RestApiSpec model, ProgressBar pbar)
		{
			var viewRoot = Path.Combine(GeneratorLocations.ViewFolder, "LowLevelClient") + "/";
			var targetFile = GeneratorLocations.EsNetFolder + @"IElasticLowLevelClient.Generated.cs";
			var source = DoRazor(nameof(GenerateLowLevelClientInterface),
				File.ReadAllText( viewRoot+ @"IElasticLowLevelClient.cshtml"), model);
			WriteFormattedCsharpFile(targetFile, source);
		}

		private static void GenerateLowLevelClient(RestApiSpec model, ProgressBar pbar)
		{
			var viewRoot = Path.Combine(GeneratorLocations.ViewFolder, "LowLevelClient") + "/";
			
			var targetFile = GeneratorLocations.EsNetFolder + @"ElasticLowLevelClient.Root.cs";
			var sourceFileContents = File.ReadAllText(viewRoot + @"ElasticLowLevelClient.cshtml");
			var source = DoRazor(nameof(GenerateLowLevelClient), sourceFileContents, model);
			WriteFormattedCsharpFile(targetFile, source);

			var namespaced = model.EndpointsPerNamespace.Where(kv => kv.Key != CsharpNames.RootNamespace).ToList();
			using (var c = pbar.Spawn(namespaced.Count, "Generating namespaces", new ProgressBarOptions { ForegroundColor = ConsoleColor.Yellow }))
			{
				foreach (var ns in namespaced)
				{
					targetFile = GeneratorLocations.EsNetFolder + $"ElasticLowLevelClient.{ns.Key}.cs";
					sourceFileContents = File.ReadAllText(viewRoot + @"ElasticLowLevelClient.Namespace.cshtml");
					source = DoRazor(nameof(GenerateLowLevelClient) + ns.Key, sourceFileContents, ns);
					WriteFormattedCsharpFile(targetFile, source);
					c.Tick($"Written namespace client for {ns.Key}");
					
				}
			}
		}

		private static void GenerateDescriptors(RestApiSpec model, ProgressBar pbar)
		{
			var targetFile = GeneratorLocations.NestFolder + @"_Generated/_Descriptors.generated.cs";
			var source = DoRazor(nameof(GenerateDescriptors), File.ReadAllText(GeneratorLocations.ViewFolder + @"_Descriptors.Generated.cshtml"),
				model);
			WriteFormattedCsharpFile(targetFile, source);
		}

		private static void GenerateRequests(RestApiSpec model, ProgressBar pbar)
		{
			var targetFile = GeneratorLocations.NestFolder + @"_Generated/_Requests.generated.cs";
			var source = DoRazor(nameof(GenerateRequests), File.ReadAllText(GeneratorLocations.ViewFolder + @"_Requests.Generated.cshtml"), model);
			WriteFormattedCsharpFile(targetFile, source);
		}

		private static void GenerateRequestParameters(RestApiSpec model, ProgressBar pbar)
		{
			var targetFile = GeneratorLocations.EsNetFolder + @"Domain/RequestParameters/RequestParameters.Generated.cs";
			var source = DoRazor(nameof(GenerateRequestParameters),
				File.ReadAllText(GeneratorLocations.ViewFolder + @"RequestParameters.Generated.cshtml"), model);
			WriteFormattedCsharpFile(targetFile, source);
		}

		private static void GenerateEnums(RestApiSpec model, ProgressBar pbar)
		{
			var targetFile = GeneratorLocations.EsNetFolder + @"Domain/Enums.Generated.cs";
			var source = DoRazor(nameof(GenerateEnums), File.ReadAllText(GeneratorLocations.ViewFolder + @"Enums.Generated.cshtml"), model);
			WriteFormattedCsharpFile(targetFile, source);
		}
	}
}
