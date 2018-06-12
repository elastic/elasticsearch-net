using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ApiGenerator.Domain;
using Newtonsoft.Json;
using ShellProgressBar;
using Newtonsoft.Json.Linq;
using RazorLight;

namespace ApiGenerator
{
	public class ApiGenerator
	{
		private static readonly RazorLightEngine Razor = new RazorLightEngineBuilder()
			.UseMemoryCachingProvider()
			.Build();

		public static void Generate(string downloadBranch, params string[] folders)
		{
			var spec = CreateRestApiSpecModel(downloadBranch, folders);
			var actions = new Dictionary<Action<RestApiSpec>, string>
			{
				{  GenerateClientInterface, "Client interface" },
				{  GenerateRequestParameters, "Request parameters" },
				{  GenerateDescriptors, "Descriptors" },
				{  GenerateRequests, "Requests" },
				{  GenerateEnums, "Enums" },
				{  GenerateRawClient, "Lowlevel client" },
				{  GenerateRawDispatch, "Dispatch" },
				{  GenerateRequestParametersExtensions, "Request parameters override" },

			};

			using (var pbar = new ProgressBar(actions.Count, "Generating code", new ProgressBarOptions { BackgroundColor = ConsoleColor.DarkGray }))
			{
				foreach(var kv in actions)
				{
					pbar.Message = "Generating " + kv.Value;
					kv.Key(spec);
					pbar.Tick("Generated " + kv.Value);
				}
			}
		}

		private static RestApiSpec CreateRestApiSpecModel(string downloadBranch, string[] folders)
		{
			var directories = Directory.GetDirectories(CodeConfiguration.RestSpecificationFolder, "*", SearchOption.AllDirectories)
				.Where(f=>folders == null || folders.Length == 0 || folders.Contains(new DirectoryInfo(f).Name))
				.ToList();

			var endpoints = new Dictionary<string, ApiEndpoint>();
			using (var pbar = new ProgressBar(directories.Count, $"Listing {directories.Count} directories", new ProgressBarOptions { BackgroundColor = ConsoleColor.DarkGray }))
			{
				foreach (var jsonFiles in directories.Select(dir => Directory.GetFiles(dir).Where(f => f.EndsWith(".json")).ToList()))
				{
					using (var fileProgress = pbar.Spawn(jsonFiles.Count, $"Listing {jsonFiles.Count} files", new ProgressBarOptions { ProgressCharacter = '─', BackgroundColor = ConsoleColor.DarkGray }))
					{
						foreach (var file in jsonFiles)
						{
							if (file.EndsWith("_common.json")) RestApiSpec.CommonApiQueryParameters = CreateCommonApiQueryParameters(file);
							else if (file.EndsWith(".obsolete.json")) continue;
							else if (file.EndsWith(".patch.json")) continue;
							else if (file.EndsWith(".replace.json")) continue;
							else
							{
								var endpoint = CreateApiEndpoint(file);
								endpoints.Add(endpoint.Key, endpoint.Value);
							}
							fileProgress.Tick();
						}
					}
					pbar.Tick();
				}
			}

			return new RestApiSpec { Endpoints = endpoints, Commit = downloadBranch };
		}

		public static string PascalCase(string s)
		{
			var textInfo = new CultureInfo("en-US").TextInfo;
			return textInfo.ToTitleCase(s.ToLowerInvariant()).Replace("_", string.Empty).Replace(".", string.Empty);
		}

		private static KeyValuePair<string, ApiEndpoint> CreateApiEndpoint(string jsonFile)
		{
			var replaceFile = Path.Combine(Path.GetDirectoryName(jsonFile), Path.GetFileNameWithoutExtension(jsonFile)) + ".replace.json";
			if (File.Exists(replaceFile))
			{
				var replaceSpec = JObject.Parse(File.ReadAllText(replaceFile));
				var endpointReplaced = replaceSpec.ToObject<Dictionary<string, ApiEndpoint>>().First();
				endpointReplaced.Value.CsharpMethodName = CreateMethodName(endpointReplaced.Key);
				return endpointReplaced;
			}

			var officialJsonSpec = JObject.Parse(File.ReadAllText(jsonFile));
			PatchOfficialSpec(officialJsonSpec, jsonFile);
			var endpoint = officialJsonSpec.ToObject<Dictionary<string, ApiEndpoint>>().First();
			endpoint.Value.CsharpMethodName = CreateMethodName(endpoint.Key);
			AddObsoletes(jsonFile, endpoint.Value);
			return endpoint;
		}

		private static void PatchOfficialSpec(JObject original, string jsonFile)
		{
			var directory = Path.GetDirectoryName(jsonFile);
			var patchFile = Path.Combine(directory, Path.GetFileNameWithoutExtension(jsonFile)) + ".patch.json";
			if (!File.Exists(patchFile)) return;

			var patchedJson = JObject.Parse(File.ReadAllText(patchFile));

			original.Merge(patchedJson, new JsonMergeSettings
			{
				MergeArrayHandling = MergeArrayHandling.Union
			});
		}

		private static void AddObsoletes(string jsonFile, ApiEndpoint endpoint)
		{
			var directory = Path.GetDirectoryName(jsonFile);
			var obsoleteFile = Path.Combine(directory, Path.GetFileNameWithoutExtension(jsonFile)) + ".obsolete.json";
			if (!File.Exists(obsoleteFile)) return;

			var json = File.ReadAllText(obsoleteFile);
			var endpointOverride = JsonConvert.DeserializeObject<Dictionary<string, ApiEndpoint>>(json).First();
			endpoint.ObsoleteQueryParameters = endpointOverride.Value?.Url?.Params ?? new Dictionary<string, ApiQueryParameters>();
			endpoint.RemovedMethods = endpointOverride.Value?.RemovedMethods ?? new Dictionary<string, string>();
		}

		private static Dictionary<string, ApiQueryParameters> CreateCommonApiQueryParameters(string jsonFile)
		{
			var json = File.ReadAllText(jsonFile);
			var jobject = JObject.Parse(json);
			var commonParameters = jobject.Property("params").Value.ToObject<Dictionary<string, ApiQueryParameters>>();
			return commonParameters;
		}

		private static string CreateMethodName(string apiEndpointKey)
		{
			return PascalCase(apiEndpointKey);
		}

		private static string DoRazor(string name, string template, RestApiSpec model)
		{
			return Razor.CompileRenderAsync(name, template, model).GetAwaiter().GetResult();
		}

		private static void GenerateClientInterface(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.EsNetFolder + @"IElasticLowLevelClient.Generated.cs";
			var source = DoRazor(nameof(GenerateClientInterface), File.ReadAllText(CodeConfiguration.ViewFolder + @"IElasticLowLevelClient.Generated.cshtml"), model);
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateRawDispatch(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.NestFolder + @"_Generated/_LowLevelDispatch.Generated.cs";
			var source = DoRazor(nameof(GenerateRawDispatch), File.ReadAllText(CodeConfiguration.ViewFolder + @"_LowLevelDispatch.Generated.cshtml"), model);
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateRawClient(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.EsNetFolder + @"ElasticLowLevelClient.Generated.cs";
			var source = DoRazor(nameof(GenerateRawClient), File.ReadAllText(CodeConfiguration.ViewFolder + @"ElasticLowLevelClient.Generated.cshtml"), model);
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateDescriptors(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.NestFolder + @"_Generated\_Descriptors.Generated.cs";
			var source = DoRazor(nameof(GenerateDescriptors), File.ReadAllText(CodeConfiguration.ViewFolder + @"_Descriptors.Generated.cshtml"), model);
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateRequests(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.NestFolder + @"_Generated\_Requests.Generated.cs";
			var source = DoRazor(nameof(GenerateRequests), File.ReadAllText(CodeConfiguration.ViewFolder + @"_Requests.Generated.cshtml"), model);
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateRequestParameters(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.EsNetFolder + @"Domain\RequestParameters\RequestParameters.Generated.cs";
			var source = DoRazor(nameof(GenerateRequestParameters), File.ReadAllText(CodeConfiguration.ViewFolder + @"RequestParameters.Generated.cshtml"), model);
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateRequestParametersExtensions(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.NestFolder + @"_Generated\_RequestParametersExtensions.Generated.cs";
			var source = DoRazor(nameof(GenerateRequestParametersExtensions), File.ReadAllText(CodeConfiguration.ViewFolder + @"_RequestParametersExtensions.Generated.cshtml"), model);
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateEnums(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.EsNetFolder + @"Domain\Enums.Generated.cs";
			var source = DoRazor(nameof(GenerateEnums), File.ReadAllText(CodeConfiguration.ViewFolder + @"Enums.Generated.cshtml"), model);
			File.WriteAllText(targetFile, source);
		}
	}
}
