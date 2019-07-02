using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApiGenerator.Configuration;
using ApiGenerator.Configuration.Overrides;
using ApiGenerator.Domain;
using ApiGenerator.Domain.Code;
using ApiGenerator.Domain.Specification;
using Newtonsoft.Json.Linq;

namespace ApiGenerator.Generator 
{
	public static class ApiEndpointFactory
	{
		public static ApiEndpoint FromFile(string jsonFile)
		{
			var officialJsonSpec = JObject.Parse(File.ReadAllText(jsonFile));
			PatchOfficialSpec(officialJsonSpec, jsonFile);
			var (name, endpoint) = officialJsonSpec.ToObject<Dictionary<string, ApiEndpoint>>().First();
			
			endpoint.FileName = Path.GetFileName(jsonFile);
			endpoint.Name = name;
			var tokens = name.Split(".");
			
			endpoint.MethodName = tokens.Last();
			if (tokens.Length > 1)
				endpoint.Namespace = tokens[0];
			//todo side effect
			endpoint.CsharpNames = new CsharpNames(name, endpoint.MethodName, endpoint.Namespace);
			
			LoadOverridesOnEndpoint(endpoint);
			PatchRequestParameters(endpoint);

			EnforceRequiredOnParts(jsonFile, endpoint.Url);
			return endpoint;
		}
		
		/// <summary>
		/// This makes sure required is configured correctly by inspecting the paths.
		/// Will emit a warning if the spec file got this wrong
		/// </summary>
		private static void EnforceRequiredOnParts(string jsonFile, UrlInformation url)
		{
			if (url.IsPartless) return;
			foreach (var part in url.Parts)
			{
				var required = url.Paths.All(p => p.Path.Contains($"{{{part.Name}}}"));
				if (part.Required != required)
					ApiGenerator.Warnings.Add($"{jsonFile} has part: {part.Name} listed as {part.Required} but should be {required}");
				part.Required = required;
			}
		}

		private static void LoadOverridesOnEndpoint(ApiEndpoint endpoint)
		{
			var method = endpoint.CsharpNames.MethodName;
			if (CodeConfiguration.ApiNameMapping.TryGetValue(endpoint.Name, out var mapsApiMethodName))
				method = mapsApiMethodName;

			var namespacePrefix = typeof(GlobalOverrides).Namespace + ".Endpoints.";
			var typeName = namespacePrefix + method + "Overrides";
			var type = GeneratorLocations.Assembly.GetType(typeName);
			if (type != null && Activator.CreateInstance(type) is IEndpointOverrides overrides)
				endpoint.Overrides = overrides;
		}

		private static void PatchRequestParameters(ApiEndpoint endpoint)
		{
			var newParams = ApiQueryParametersPatcher.Patch(endpoint.Name, endpoint.Url.Params, endpoint.Overrides);
			endpoint.Url.Params = newParams;
		}
		
		/// <summary>
		/// Finds a patch file in patches and union merges this with the official spec.
		/// This allows us to check in tweaks should breaking changes occur in the spec before we catch them
		/// </summary>
		private static void PatchOfficialSpec(JObject original, string jsonFile)
		{
			var directory = Path.GetDirectoryName(jsonFile);
			var patchFile = Path.Combine(directory,"..", "_Patches", Path.GetFileNameWithoutExtension(jsonFile)) + ".patch.json";
			if (!File.Exists(patchFile)) return;

			var patchedJson = JObject.Parse(File.ReadAllText(patchFile));

			var pathsOverride = patchedJson.SelectToken("*.url.paths");

			original.Merge(patchedJson, new JsonMergeSettings
			{
				MergeArrayHandling = MergeArrayHandling.Union
			});

			if (pathsOverride != null) original.SelectToken("*.url.paths").Replace(pathsOverride);

			void ReplaceOptions(string path)
			{
				var optionsOverrides = patchedJson.SelectToken(path);
				if (optionsOverrides != null)
					original.SelectToken(path).Replace(optionsOverrides);
			}

			ReplaceOptions("*.url.parts.metric.options");
			ReplaceOptions("*.url.parts.index_metric.options");
		}
	}
}
