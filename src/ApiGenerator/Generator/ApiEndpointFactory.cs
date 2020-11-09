// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApiGenerator.Configuration;
using ApiGenerator.Configuration.Overrides;
using ApiGenerator.Domain;
using ApiGenerator.Domain.Code;
using ApiGenerator.Domain.Specification;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiGenerator.Generator
{
	public static class ApiEndpointFactory
	{
		private static readonly JsonSerializer Serializer = JsonSerializer.Create(
			new JsonSerializerSettings { Converters = new List<JsonConverter> { new QueryParameterDeprecationConverter() } });

		public static ApiEndpoint FromFile(string jsonFile)
		{
			var officialJsonSpec = JObject.Parse(File.ReadAllText(jsonFile));
			TransformNewSpecStructureToOld(officialJsonSpec);
			PatchOfficialSpec(officialJsonSpec, jsonFile);
			var (name, endpoint) = officialJsonSpec.ToObject<Dictionary<string, ApiEndpoint>>(Serializer).First();

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
				{
					var message = required
						? "is [b green] required [/] but appears in spec as [b red] optional [/]"
						: "is [b green] optional [/] but marked as [b red] required [/]  ";
					// TODO submit PR to fix these, too noisy for now
					//ApiGenerator.Warnings.Add($"[grey]{jsonFile}[/] part [b white] {part.Name} [/] {message}");
				}
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
			var patchFile = Path.Combine(directory!,"..", "_Patches", Path.GetFileNameWithoutExtension(jsonFile)) + ".patch.json";
			if (!File.Exists(patchFile)) return;

			var patchedJson = JObject.Parse(File.ReadAllText(patchFile));

			var pathsOverride = patchedJson.SelectToken("*.url.paths");

			original.Merge(patchedJson, new JsonMergeSettings
			{
				MergeArrayHandling = MergeArrayHandling.Union
			});

			if (pathsOverride != null) original.SelectToken("*.url.paths").Replace(pathsOverride);

			var methodsOverride = patchedJson.SelectToken("*.methods");
			if (methodsOverride != null)
				original.SelectToken("*.methods").Replace(methodsOverride);

			var paramsOverride = patchedJson.SelectToken("*.params");
			var originalParams = original.SelectToken("*.url.params") as JObject;
			originalParams?.Merge(paramsOverride, new JsonMergeSettings
			{
				MergeArrayHandling = MergeArrayHandling.Union
			});

			if (paramsOverride != null) originalParams?.Replace(originalParams);

			void ReplaceOptions(string path)
			{
				var optionsOverrides = patchedJson.SelectToken(path);
				if (optionsOverrides != null)
					original.SelectToken(path).Replace(optionsOverrides);
			}

			ReplaceOptions("*.url.parts.metric.options");
			ReplaceOptions("*.url.parts.index_metric.options");
		}

		/// <summary>
		/// Changes the structure of new REST API spec in 7.4.0 to one that matches prior spec structure.
		/// </summary>
		private static void TransformNewSpecStructureToOld(JObject original)
		{
			var name = (JProperty)original.First;
			var spec = (JObject)name.Value;

			// old spec structure, nothing to change
			if (spec.ContainsKey("methods"))
				return;

			var methods = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			JObject parts = null;
			var paths = new List<string>();
			var deprecatedPaths = new List<JObject>();

			foreach (var path in spec["url"]["paths"].Cast<JObject>())
			{
				if (path.ContainsKey("deprecated"))
				{
					var deprecated = new JObject
					{
						["version"] = path["deprecated"]["version"].Value<string>(),
						["path"] = path["path"].Value<string>(),
						["description"] = path["deprecated"]["description"].Value<string>()
					};

					deprecatedPaths.Add(deprecated);
				}
				else
					paths.Add(path["path"].Value<string>());

				if (path.ContainsKey("parts"))
				{
					if (parts == null)
						parts = path["parts"].Value<JObject>();
					else
						parts.Merge(path["parts"].Value<JObject>(), new JsonMergeSettings
						{
							MergeArrayHandling = MergeArrayHandling.Union
						});
				}

				foreach (var method in path["methods"].Cast<JValue>())
					methods.Add(method.Value<string>());
			}



			var newUrl = new JObject
			{
				["paths"] = new JArray(paths.Cast<object>().ToArray()),
			};

			if (spec.ContainsKey("params"))
			{
				newUrl["params"] = spec["params"];
				spec.Remove("params");
			}

			if (parts != null)
				newUrl["parts"] = parts;

			if (deprecatedPaths.Any())
				newUrl["deprecated_paths"] = new JArray(deprecatedPaths.Cast<object>().ToArray());

			spec["url"] = newUrl;
			spec["methods"] = new JArray(methods.Cast<object>().ToArray());
		}
	}
}
