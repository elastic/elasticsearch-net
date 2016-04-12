using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CodeGeneration.LowLevelClient.Domain;
using CodeGeneration.LowLevelClient.Overrides.Allow404;
using CodeGeneration.LowLevelClient.Overrides.Descriptors;
using CsQuery;
using CsQuery.ExtensionMethods.Internal;
using Newtonsoft.Json;
using Xipton.Razor;
using CodeGeneration.LowLevelClient.Overrides.Global;
using ShellProgressBar;

namespace CodeGeneration.LowLevelClient
{
	public class ApiGenerator
	{
		static readonly RazorMachine RazorHelper = new RazorMachine();

		public static void Generate(params string[] folders)
		{
			var spec = CreateRestApiSpecModel(folders);
			var actions = new Dictionary<Action<RestApiSpec>, string>
			{
				{  GenerateClientInterface, "Client interface" },
				{  GenerateRequestParameters, "Request parameters" },
				{  GenerateRequestParametersExtensions, "Request parameters override" },
				{  GenerateDescriptors, "Descriptors" },
				{  GenerateRequests, "Requests" },
				{  GenerateEnums, "Enums" },
				{  GenerateRawClient, "Lowlevel client" },
				{  GenerateRawDispatch, "Dispatch" },
			};

			using (var pbar = new ProgressBar(actions.Count, "Generating code", new ProgressBarOptions { BackgroundColor = ConsoleColor.DarkGray }))
			{
				foreach(var kv in actions)
				{
					pbar.UpdateMessage("Generating " + kv.Value);
					kv.Key(spec);
					pbar.Tick("Generated " + kv.Value);
				}
			}
		}

		private static RestApiSpec CreateRestApiSpecModel(string[] folders)
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
						foreach (var endpoint in jsonFiles.Select(CreateApiEndpoint))
						{
							endpoints.Add(endpoint.Key, endpoint.Value);
							fileProgress.Tick();
						}
					}
					pbar.Tick();
				}
			}

			return new RestApiSpec { Endpoints = endpoints };
		}



		public static string PascalCase(string s)
		{
			var textInfo = new CultureInfo("en-US").TextInfo;
			return textInfo.ToTitleCase(s.ToLowerInvariant()).Replace("_", string.Empty).Replace(".", string.Empty);
		}

		private static KeyValuePair<string, ApiEndpoint> CreateApiEndpoint(string jsonFile)
		{
			var json = File.ReadAllText(jsonFile);
			var endpoint = JsonConvert.DeserializeObject<Dictionary<string, ApiEndpoint>>(json).First();
			endpoint.Value.CsharpMethodName = CreateMethodName(endpoint.Key);
			return endpoint;
		}

		private static string CreateMethodName(string apiEnpointKey)
		{
			return PascalCase(apiEnpointKey);
		}

		private static void GenerateClientInterface(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.EsNetFolder + @"IElasticLowLevelClient.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(CodeConfiguration.ViewFolder + @"IElasticLowLevelClient.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateRawDispatch(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.NestFolder + @"_Generated/_LowLevelDispatch.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(CodeConfiguration.ViewFolder + @"_LowLevelDispatch.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateRawClient(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.EsNetFolder + @"ElasticLowLevelClient.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(CodeConfiguration.ViewFolder + @"ElasticLowLevelClient.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateDescriptors(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.NestFolder + @"_Generated\_Descriptors.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(CodeConfiguration.ViewFolder + @"_Descriptors.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateRequests(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.NestFolder + @"_Generated\_Requests.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(CodeConfiguration.ViewFolder + @"_Requests.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateRequestParameters(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.EsNetFolder + @"Domain\RequestParameters\RequestParameters.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(CodeConfiguration.ViewFolder + @"RequestParameters.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateRequestParametersExtensions(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.NestFolder + @"_Generated\_RequestParametersExtensions.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(CodeConfiguration.ViewFolder + @"_RequestParametersExtensions.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		private static void GenerateEnums(RestApiSpec model)
		{
			var targetFile = CodeConfiguration.EsNetFolder + @"Domain\Enums.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(CodeConfiguration.ViewFolder + @"Enums.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}
	}
}
