using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using CodeGeneration.LowLevelClient.Domain;
using CodeGeneration.LowLevelClient.Overrides.Allow404;
using CodeGeneration.LowLevelClient.Overrides.Descriptors;
using CsQuery;
using CsQuery.ExtensionMethods.Internal;
using Newtonsoft.Json;
using Xipton.Razor;
using CodeGeneration.LowLevelClient.Overrides.Global;

namespace CodeGeneration.LowLevelClient
{
	public class ApiGenerator
	{
		private static string NestFolder;
		private static string EsNetFolder;
		private static string ViewFolder;
		private static string ApiEndpointsFolder;
		private static readonly RazorMachine RazorHelper;
		private static readonly string Version = "master";
		private static readonly List<string> ApiListings = new List<string>
		{
			"https://github.com/elastic/elasticsearch/tree/{version}/rest-api-spec/src/main/resources/rest-api-spec/api"
		};

		private static readonly Assembly Assembly;

		static ApiGenerator()
		{
			RazorHelper = new RazorMachine();
			Assembly = typeof(ApiGenerator).Assembly;

			var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

			if (directoryInfo.Name == "CodeGeneration.LowLevelClient" &&
				directoryInfo.Parent != null &&
				directoryInfo.Parent.Name == "CodeGeneration")
			{
				// running as a dnx project
				NestFolder = @"..\..\..\src\Nest\";
				EsNetFolder = @"..\..\..\src\Elasticsearch.Net\";
				ViewFolder = @"Views\";
				ApiEndpointsFolder = @"ApiEndpoints\";
			}
			else
			{
				NestFolder = @"..\..\..\..\..\..\src\Nest\";
				EsNetFolder = @"..\..\..\..\..\..\src\Elasticsearch.Net\";
				ViewFolder = @"..\..\..\Views\";
				ApiEndpointsFolder = @"..\..\..\ApiEndpoints\";
			}
		}

		public static string PascalCase(string s)
		{
			var textInfo = new CultureInfo("en-US").TextInfo;
			return textInfo.ToTitleCase(s.ToLowerInvariant()).Replace("_", string.Empty).Replace(".", string.Empty).Replace("-", string.Empty);
		}

		public void GenerateEndpointFiles()
		{
			Console.WriteLine("Getting a listing of all the api endpoints from the elasticsearch-rest-api-spec repos");
			foreach(var listing in ApiListings.Select(l=>l.Replace("{version}", Version)))
				DownloadJsonDefinitions(listing);
		}

		private void DownloadJsonDefinitions(string listingUrl)
		{
			using (var client = new WebClient())
			{
				var html = client.DownloadString(listingUrl);
				FindJsonFilesOnListing(listingUrl, html);
			}
		}

		private void FindJsonFilesOnListing(string listingUrl, string html)
		{
			var dom = CQ.Create(html);

			WriteToEndpointsFolder("root.html", html);

			var endpoints = dom[".js-navigation-open"]
				.Select(s => s.InnerText)
				.Where(s => !string.IsNullOrEmpty(s) && s.EndsWith(".json"))
				.ToList();

			endpoints.ForEach(s => WriteEndpointFile(listingUrl, s));
		}


		private void WriteEndpointFile(string listingUrl, string s)
		{
			using (var client = new WebClient())
			{
				var rawFile = listingUrl.Replace("github.com", "raw.githubusercontent.com").Replace("tree/", "") + "/" + s;
				var fileName = rawFile.Split('/').Last();
				Console.WriteLine("Downloading {0}", rawFile);
				var json = client.DownloadString(rawFile);
				WriteToEndpointsFolder(fileName, json);
			}
		}


		public RestApiSpec GetRestApiSpec()
		{
			var spec = new RestApiSpec
			{
				Commit = CQ.Create(LocalUri("root.html"))[".sha:first"].Text(),
				Endpoints = Directory.GetFiles(ApiEndpointsFolder)
					.Where(f => f.EndsWith(".json"))
					.Select(CreateApiEndpoint)
					.ToDictionary(d => d.Key, d => d.Value)
			};

			return spec;
		}

		private KeyValuePair<string, ApiEndpoint> CreateApiEndpoint(string jsonFile)
		{
			var json = File.ReadAllText(jsonFile);
			var endpoint = JsonConvert.DeserializeObject<Dictionary<string, ApiEndpoint>>(json).First();
			endpoint.Value.Generator = this;
			endpoint.Value.CsharpMethodName = CreateMethodName(endpoint.Key);
			return endpoint;
		}

		private static string LocalUri(string file)
		{
			var basePath = Path.Combine(Assembly.GetEntryAssembly().Location, @"..\" + ApiEndpointsFolder + file);
			var assemblyPath = Path.GetFullPath(new Uri(basePath).LocalPath);
			var fileUri = new Uri(assemblyPath).AbsoluteUri;
			return fileUri;
		}

		private readonly Dictionary<string, string> MethodNameOverrides =
			(from f in new DirectoryInfo(NestFolder).GetFiles("*.cs", SearchOption.AllDirectories)
			 let contents = File.ReadAllText(f.FullName)
			 let c = Regex.Replace(contents, @"^.+\[DescriptorFor\(""([^ \r\n]+)""\)\].*$", "$1", RegexOptions.Singleline)
			 where !c.Contains(" ") //filter results that did not match
			 select new { Value = f.Name.Replace("Request", ""), Key = c })
			.DistinctBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value.Replace(".cs", ""));

		private readonly Dictionary<string, string> KnownDescriptors =
			(from f in new DirectoryInfo(NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
			 let contents = File.ReadAllText(f.FullName)
			 let c = Regex.Replace(contents, @"^.+class ([^ \r\n]+Descriptor(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
			 select new { Key = Regex.Replace(c, "<.*$", ""), Value = Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1") })
			.DistinctBy(v => v.Key)
			.OrderBy(v=>v.Key)
			.ToDictionary(k => k.Key, v => v.Value);

		private readonly Dictionary<string, string> KnownRequests =
			(from f in new DirectoryInfo(NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
			 let contents = File.ReadAllText(f.FullName)
			 let c = Regex.Replace(contents, @"^.+interface ([^ \r\n]+Request(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
			 where c.StartsWith("I") && c.Contains("Request")
			 select new { Key = Regex.Replace(c, "<.*$", ""), Value = Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1") })
			.DistinctBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value);


		//Patches a method name for the exceptions (IndicesStats needs better unique names for all the url endpoints)
		//or to get rid of double verbs in an method name i,e ClusterGetSettingsGet > ClusterGetSettings
		public void PatchMethod(CsharpMethod method)
		{
			Func<string, bool> ms = s => method.FullName.StartsWith(s);
			Func<string, bool> pc = s => method.Path.Contains(s);

			if (ms("Indices") && !pc("{index}"))
				method.FullName = (method.FullName + "ForAll").Replace("AsyncForAll", "ForAllAsync");

			if (ms("Nodes") && !pc("{node_id}"))
				method.FullName = (method.FullName + "ForAll").Replace("AsyncForAll", "ForAllAsync");

			//remove duplicate occurance of the HTTP method name
			var m = method.HttpMethod.ToPascalCase();
			method.FullName =
				Regex.Replace(method.FullName, m, a => a.Index != method.FullName.IndexOf(m, StringComparison.Ordinal) ? "" : m);

			foreach (var param in GlobalQueryParameters.Parameters)
			{
				if (!method.Url.Params.ContainsKey(param.Key))
					method.Url.Params.Add(param.Key, param.Value);
			}

			string manualOverride;
			var key = method.QueryStringParamName.Replace("RequestParameters", "");
			if (MethodNameOverrides.TryGetValue(key, out manualOverride))
				method.QueryStringParamName = manualOverride + "RequestParameters";

			method.DescriptorType = method.QueryStringParamName.Replace("RequestParameters", "Descriptor");
			method.RequestType = method.QueryStringParamName.Replace("RequestParameters", "Request");
			string requestGeneric;
			if (KnownRequests.TryGetValue("I" + method.RequestType, out requestGeneric))
				method.RequestTypeGeneric = requestGeneric;
			else method.RequestTypeUnmapped = true;

			method.Allow404 = ApiEndpointsThatAllow404.Endpoints.Contains(method.DescriptorType.Replace("Descriptor", ""));

			string generic;
			if (KnownDescriptors.TryGetValue(method.DescriptorType, out generic))
				method.DescriptorTypeGeneric = generic;
			else method.Unmapped = true;

			try
			{
				IEnumerable<string> skipList = new List<string>();
				IDictionary<string, string> renameList = new Dictionary<string, string>();

				var typeName = "CodeGeneration.LowLevelClient.Overrides.Descriptors." + method.DescriptorType + "Overrides";
				var type = Assembly.GetType(typeName);
				if (type != null)
				{
					var overrides = Activator.CreateInstance(type) as IDescriptorOverrides;
					if (overrides != null)
					{
						skipList = overrides.SkipQueryStringParams ?? skipList;
						renameList = overrides.RenameQueryStringParams ?? renameList;
					}
				}

				var globalQueryStringRenames = new Dictionary<string, string>
				{
					{"_source", "source_enabled"},
					{"_source_include", "source_include"},
					{"_source_exclude", "source_exclude"},
					{"q", "query_on_query_string"},
				};

				foreach (var kv in globalQueryStringRenames)
					if (!renameList.ContainsKey(kv.Key))
						renameList[kv.Key] = kv.Value;

				var patchedParams = new Dictionary<string, ApiQueryParameters>();
				foreach (var kv in method.Url.Params)
				{
					if (kv.Value.OriginalQueryStringParamName.IsNullOrEmpty())
						kv.Value.OriginalQueryStringParamName = kv.Key;
					if (skipList.Contains(kv.Key))
						continue;

					string newName;
					if (!renameList.TryGetValue(kv.Key, out newName))
					{
						patchedParams.Add(kv.Key, kv.Value);
						continue;
					}

					patchedParams.Add(newName, kv.Value);

					//if (newName == "source_enabled")
					//{
					//	kv.Value.DeprecatedInFavorOf = "EnableSource";
					//	patchedParams.Add("enable_source", new ApiQueryParameters
					//	{
					//		Description = kv.Value.Description,
					//		Options = kv.Value.Options,
					//		Type = "boolean",
					//		OriginalQueryStringParamName = "_source"
					//	});
					//}
				}

				method.Url.Params = patchedParams;
			}
			// ReSharper disable once EmptyGeneralCatchClause
			catch
			{
			}

		}

		public static string CreateMethodName(string apiEnpointKey)
		{
			return PascalCase(apiEnpointKey);
		}

		public void GenerateClientInterface(RestApiSpec model)
		{
			var targetFile = EsNetFolder + @"IElasticLowLevelClient.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(ViewFolder + @"IElasticLowLevelClient.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}


		public void GenerateRawDispatch(RestApiSpec model)
		{
			var targetFile = NestFolder + @"_Generated/_LowLevelDispatch.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(ViewFolder + @"_LowLevelDispatch.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}
		public void GenerateRawClient(RestApiSpec model)
		{
			var targetFile = EsNetFolder + @"ElasticLowLevelClient.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(ViewFolder + @"ElasticLowLevelClient.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		public void GenerateDescriptors(RestApiSpec model)
		{
			var targetFile = NestFolder + @"_Generated\_Descriptors.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(ViewFolder + @"_Descriptors.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		public void GenerateRequests(RestApiSpec model)
		{
			var targetFile = NestFolder + @"_Generated\_Requests.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(ViewFolder + @"_Requests.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		public void GenerateRequestParameters(RestApiSpec model)
		{
			var targetFile = EsNetFolder + @"Domain\RequestParameters\RequestParameters.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(ViewFolder + @"RequestParameters.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		public void GenerateRequestParametersExtensions(RestApiSpec model)
		{
			var targetFile = NestFolder + @"_Generated\_RequestParametersExtensions.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(ViewFolder + @"_RequestParametersExtensions.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}
		public void GenerateEnums(RestApiSpec model)
		{
			var targetFile = EsNetFolder + @"Domain\Enums.Generated.cs";
			var source = RazorHelper.Execute(File.ReadAllText(ViewFolder + @"Enums.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		private void WriteToEndpointsFolder(string filename, string contents)
		{
			if (!Directory.Exists(ApiEndpointsFolder))
				Directory.CreateDirectory(ApiEndpointsFolder);

			File.WriteAllText(ApiEndpointsFolder + filename, contents);
		}
	}
}
