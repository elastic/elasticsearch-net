using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using CsQuery;
using Newtonsoft.Json;
using RawClientGenerator.Overrides.Descriptors;
using Xipton.Razor;

namespace RawClientGenerator
{
	public static class ApiGenerator
	{
		private readonly static string _listingUrl = "https://github.com/elasticsearch/elasticsearch/tree/0.90/rest-api-spec/api";
		private readonly static string _rawUrlPrefix = "https://raw.github.com/elasticsearch/elasticsearch/0.90/rest-api-spec/api/";
		private readonly static string _nestFolder = @"..\..\..\..\src\Nest\";
		private readonly static string _viewFolder = @"..\..\Views\";
		private readonly static string _cacheFolder = @"..\..\Cache\";
		private static readonly RazorMachine _razorMachine;

		private static readonly Assembly _assembly;

		static ApiGenerator()
		{
			_razorMachine = new RazorMachine();
			_assembly = typeof (ApiGenerator).Assembly;
		}
		public static string PascalCase(string s)
		{
			var textInfo = new CultureInfo("en-US").TextInfo;
			return textInfo.ToTitleCase(s.ToLowerInvariant()).Replace("_", string.Empty).Replace(".", string.Empty);
		}
		public static RestApiSpec GetRestSpec(bool useCache)
		{
			Console.WriteLine("Getting a listing of all the api endpoints from the elasticsearch-rest-api-spec repos");

			string html = string.Empty;
			using (var client = new WebClient())
				html = client.DownloadString(useCache ? LocalUri("root.html") : _listingUrl);
			
			var dom = CQ.Create(html);
			if (!useCache)
				File.WriteAllText(_cacheFolder + "root.html", html);
			
			var endpoints = dom[".js-directory-link"]
				.Select(s => s.InnerText)
				.Where(s => !string.IsNullOrEmpty(s) && s.EndsWith(".json"))
				.Select(s => CreateApiDocumentation(useCache, s))
				.ToDictionary(d => d.Key, d => d.Value);

			var restSpec = new RestApiSpec
			{
				Endpoints = endpoints,
				Commit = dom[".sha:first"].Text()
			};


			return restSpec;
		}

		private static string LocalUri(string file)
		{
			var basePath = Path.Combine(Assembly.GetEntryAssembly().Location, @"..\" + _cacheFolder + file);
			var assemblyPath = Path.GetFullPath((new Uri(basePath)).LocalPath);
			var fileUri = new Uri(assemblyPath).AbsoluteUri;
			return fileUri;
		}

		private static KeyValuePair<string, ApiEndpoint> CreateApiDocumentation(bool useCache, string s)
		{
			using (var client = new WebClient())
			{
				var rawFile = _rawUrlPrefix + s;
				var fileName = rawFile.Split(new[] {'/'}).Last();
				Console.WriteLine("Downloading {0}", rawFile);
				var json = client.DownloadString(useCache ? LocalUri(fileName) : rawFile);
				if (!useCache)
					File.WriteAllText(_cacheFolder + fileName, json);

				var apiDocumentation = JsonConvert.DeserializeObject<Dictionary<string, ApiEndpoint>>(json).First();
				apiDocumentation.Value.CsharpMethodName = CreateMethodName(
					apiDocumentation.Key,
					apiDocumentation.Value
					);
				return apiDocumentation;
			}
		}
		private static readonly Dictionary<string, string> MethodNameOverrides =
			(from f in new DirectoryInfo(_nestFolder + "/DSL").GetFiles("*.cs", SearchOption.TopDirectoryOnly)
			 where f.FullName.EndsWith("Descriptor.cs")
			let contents = File.ReadAllText(f.FullName)
			let c = Regex.Replace(contents, @"^.+\[DescriptorFor\(""([^ \r\n]+)""\)\].*$", "$1", RegexOptions.Singleline)
			where !c.Contains(" ") //filter results that did not match
			select new { Value = f.Name.Replace("Descriptor.cs",""), Key = c })
			.DistinctBy(v=>v.Key)
			.ToDictionary(k => k.Key, v => v.Value);

		private static readonly Dictionary<string, string> KnownDescriptors =
			(from f in new DirectoryInfo(_nestFolder + "/DSL").GetFiles("*.cs", SearchOption.TopDirectoryOnly)
			 where f.FullName.EndsWith("Descriptor.cs")
			let contents = File.ReadAllText(f.FullName)
			let c = Regex.Replace(contents, @"^.+class ([^ \r\n]+).*$", "$1", RegexOptions.Singleline)
			select new { Key =  Regex.Replace(c, "<.*$", ""), Value =  Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1")})
			.DistinctBy(v=>v.Key)
			.ToDictionary(k => k.Key, v => v.Value);



		//Patches a method name for the exceptions (IndicesStats needs better unique names for all the url endpoints)
		//or to get rid of double verbs in an method name i,e ClusterGetSettingsGet > ClusterGetSettings
		public static void PatchMethod(CsharpMethod method)
		{
			if (method.FullName.StartsWith("IndicesStats") && method.Path.Contains("{index}"))
				method.FullName = method.FullName.Replace("IndicesStats", "IndexStats");
			//IndicesPutAliasPutAsync
			if (method.FullName.StartsWith("IndicesPutAlias") && method.Path.Contains("{index}"))
				method.FullName = method.FullName.Replace("IndicesPutAlias", "IndexPutAlias");

			if (method.FullName.StartsWith("IndicesStats") || method.FullName.StartsWith("IndexStats"))
			{
				if (method.Path.Contains("/indexing/"))
					method.FullName = method.FullName.Replace("Stats", "IndexingStats");
				if (method.Path.Contains("/search/"))
					method.FullName = method.FullName.Replace("Stats", "SearchStats");
				if (method.Path.Contains("/fielddata/"))
					method.FullName = method.FullName.Replace("Stats", "FieldDataStats");
			}

			//remove duplicate occurance of the HTTP method name
			var m = method.HttpMethod.ToPascalCase();
			method.FullName =
				Regex.Replace(method.FullName, m, (a) => a.Index != method.FullName.IndexOf(m) ? "" : m);

			string manualOverride;
			var key = method.QueryStringParamName.Replace("QueryString", "");
			if (MethodNameOverrides.TryGetValue(key, out manualOverride))
				method.QueryStringParamName = manualOverride + "QueryString";

			method.DescriptorType = method.QueryStringParamName.Replace("QueryString","Descriptor");

			string generic;
			if (KnownDescriptors.TryGetValue(method.DescriptorType, out generic))
				method.DescriptorTypeGeneric = generic;

			try
			{
				var typeName = "RawClientGenerator.Overrides.Descriptors." + method.DescriptorType + "Overrides";
				var type = _assembly.GetType(typeName);
				if (type == null)
					return;
				var overrides = Activator.CreateInstance(type) as IDescriptorOverrides;
				if (overrides == null)
					return;
				method.Url.Params = method.Url.Params.Where(p => !overrides.SkipQueryStringParams.Contains(p.Key))
					.ToDictionary(k => k.Key, v => v.Value);
			}
// ReSharper disable once EmptyGeneralCatchClause
			catch (Exception e)
			{
			}

		}

		public static string CreateMethodName(string apiEnpointKey, ApiEndpoint endpoint)
		{
			return PascalCase(apiEnpointKey);
		}

		public static void GenerateClientInterface(RestApiSpec model)
		{
			var targetFile = _nestFolder + @"IRawElasticClient.Generated.cs";
			var source = _razorMachine.Execute(File.ReadAllText(_viewFolder + @"IRawElasticClient.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}


		public static void GenerateRawDispatch(RestApiSpec model)
		{
			var targetFile = _nestFolder + @"RawDispatch.Generated.cs";
			var source = _razorMachine.Execute(File.ReadAllText(_viewFolder + @"RawDispatch.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}
		public static void GenerateRawClient(RestApiSpec model)
		{
			var targetFile = _nestFolder + @"RawElasticClient.Generated.cs";
			var source = _razorMachine.Execute(File.ReadAllText(_viewFolder + @"RawElasticClient.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		public static void GenerateDescriptors(RestApiSpec model)
		{
			var targetFile = _nestFolder + @"DSL\_Descriptors.Generated.cs";
			var source = _razorMachine.Execute(File.ReadAllText(_viewFolder + @"_Descriptors.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		public static void GenerateQueryStringParameters(RestApiSpec model)
		{
			var targetFile = _nestFolder + @"QueryStringParameters\QueryStringParameters.Generated.cs";
			var source = _razorMachine.Execute(File.ReadAllText(_viewFolder + @"QueryStringParameters.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}

		public static void GenerateEnums(RestApiSpec model)
		{
			var targetFile = _nestFolder + @"Enums\Enums.Generated.cs";
			var source = _razorMachine.Execute(File.ReadAllText(_viewFolder + @"Enums.Generated.cshtml"), model).ToString();
			File.WriteAllText(targetFile, source);
		}
	}
}