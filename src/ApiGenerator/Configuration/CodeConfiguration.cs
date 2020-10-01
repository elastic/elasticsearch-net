// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApiGenerator.Configuration
{
	public static class CodeConfiguration
	{
		/// <summary> These APIs are not implemented yet in the low and high level client</summary>
		public static string[] IgnoredApis { get; } =
		{
			// Internal only,
			"monitoring.bulk.json",

			// Never exposed and now deprecated
			"data_frame_transform_deprecated.delete_transform.json",
			"data_frame_transform_deprecated.get_transform.json",
			"data_frame_transform_deprecated.get_transform_stats.json",
			"data_frame_transform_deprecated.preview_transform.json",
			"data_frame_transform_deprecated.put_transform.json",
			"data_frame_transform_deprecated.start_transform.json",
			"data_frame_transform_deprecated.stop_transform.json",
			"data_frame_transform_deprecated.update_transform.json",

			// To be removed
			"indices.upgrade.json",
			"indices.get_upgrade.json",

			// already removed in the client.
			"indices.exists_type.json"

		};

		private static string[] IgnoredApisHighLevel { get; } =
		{
			"autoscaling.get_autoscaling_decision.json", // 7.7 experimental
			"autoscaling.delete_autoscaling_decision.json", // experimental
			"autoscaling.get_autoscaling_policy.json", // experimental
			"autoscaling.put_autoscaling_policy.json", // experimental
			"autoscaling.delete_autoscaling_policy.json", // experimental

			"indices.delete_index_template.json",
			"indices.exists_index_template.json",
			"indices.get_index_template.json",
			"indices.put_index_template.json",
			"indices.simulate_index_template.json",
			"indices.simulate_template.json",

			"searchable_snapshots.stats.json",
			"searchable_snapshots.clear_cache.json",
			"searchable_snapshots.mount.json",
			"searchable_snapshots.repository_stats.json",

			"get_script_context.json", // 7.7 experimental
			"get_script_languages.json", // 7.7 experimental

			"indices.exist_type.json", // already removed on client

			"ml.delete_trained_model.json", // 7.7 experimental
			"ml.evaluate_data_frame.json", // 7.7 experimental
			"ml.explain_data_frame_analytics.json", // 7.7 experimental
			"ml.find_file_structure.json", // 7.7 experimental
			"ml.get_data_frame_analytics.json", // 7.7 experimental
			"ml.get_data_frame_analytics_stats.json", // 7.7 experimental
			"ml.delete_data_frame_analytics.json", // 7.7 experimental
			"ml.get_trained_models.json", // 7.7 experimental
			"ml.get_trained_models_stats.json", // 7.7 experimental
			"ml.put_data_frame_analytics.json", // 7.7 experimental
			"ml.put_trained_model.json", // 7.7 experimental
			"ml.start_data_frame_analytics.json", // 7.7 experimental
			"ml.stop_data_frame_analytics.json", // 7.7 experimental
			"ml.update_data_frame_analytics.json", // 7.7 experimental

			"rank_eval.json", // 7.7 experimental
			"scripts_painless_context.json", // 7.7 experimental
			"cluster.delete_component_template.json", // 7.8 experimental
			"cluster.get_component_template.json", // 7.8 experimental
			"cluster.put_component_template.json", // 7.8 experimental
			"cluster.exists_component_template.json", // 7.8 experimental

			"eql.search.json", // 7.9 beta
			"eql.get.json", // 7.9 beta
			"eql.delete.json", // 7.9 beta
		};

		/// <summary>
		/// Map API default names for API's we are only supporting on the low level client first
		/// </summary>
		private static readonly Dictionary<string, string> LowLevelApiNameMapping = new Dictionary<string, string>
		{
			{ "indices.delete_index_template", "DeleteIndexTemplateV2" },
			{ "indices.get_index_template", "GetIndexTemplateV2" },
			{ "indices.put_index_template", "PutIndexTemplateV2" }
		};

		/// <summary>
		/// Scan all nest source code files for Requests and look for the [MapsApi(filename)] attribute.
		/// The class name minus Request is used as the canonical .NET name for the API.
		/// </summary>
		public static readonly Dictionary<string, string> HighLevelApiNameMapping =
			(from f in new DirectoryInfo(GeneratorLocations.NestFolder).GetFiles("*.cs", SearchOption.AllDirectories)
				let contents = File.ReadAllText(f.FullName)
				let c = Regex.Replace(contents, @"^.+\[MapsApi\(""([^ \r\n]+)""\)\].*$", "$1", RegexOptions.Singleline)
				where !c.Contains(" ") //filter results that did not match
				select new { Value = f.Name.Replace("Request", ""), Key = c.Replace(".json", "") })
			.DistinctBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value.Replace(".cs", ""));

		public static readonly HashSet<string> EnableHighLevelCodeGen = new HashSet<string>();

		public static bool IsNewHighLevelApi(string apiFileName) =>
			// if its explicitly ignored we know about it.
			!IgnoredApis.Contains(apiFileName)
			&& !IgnoredApisHighLevel.Contains(apiFileName)
			// no requests with [MapsApi("filename.json")] found
			&& !HighLevelApiNameMapping.ContainsKey(apiFileName.Replace(".json", ""));

		public static bool IgnoreHighLevelApi(string apiFileName)
		{
			//explicitly ignored
			if (IgnoredApis.Contains(apiFileName) || IgnoredApisHighLevel.Contains(apiFileName)) return true;

			//always generate already mapped requests

			if (HighLevelApiNameMapping.ContainsKey(apiFileName.Replace(".json", ""))) return false;

			return !EnableHighLevelCodeGen.Contains(apiFileName);
		}

		private static Dictionary<string, string> _apiNameMapping;

		public static Dictionary<string, string> ApiNameMapping
		{
			get
			{
				if (_apiNameMapping != null) return _apiNameMapping;
				lock (LowLevelApiNameMapping)
				{
					if (_apiNameMapping == null)
					{
						var mapping = new Dictionary<string, string>(HighLevelApiNameMapping);
						foreach (var (k, v) in LowLevelApiNameMapping)
							mapping[k] = v;
						_apiNameMapping = mapping;
					}
					return _apiNameMapping;
				}
			}
		}

		private static readonly string ResponseBuilderAttributeRegex = @"^.+\[ResponseBuilderWithGeneric\(""([^ \r\n]+)""\)\].*$";
		/// <summary>
		/// Scan all nest source code files for Requests and look for the [MapsApi(filename)] attribute.
		/// The class name minus Request is used as the canonical .NET name for the API.
		/// </summary>
		public static readonly Dictionary<string, string> ResponseBuilderInClientCalls =
			(from f in new DirectoryInfo(GeneratorLocations.NestFolder).GetFiles("*.cs", SearchOption.AllDirectories)
				from l in File.ReadLines(f.FullName)
				where Regex.IsMatch(l, ResponseBuilderAttributeRegex)
				let c = Regex.Replace(l, @"^.+\[ResponseBuilderWithGeneric\(""([^ \r\n]+)""\)\].*$", "$1", RegexOptions.Singleline)
				select new { Key = f.Name.Replace(".cs", ""), Value = c })
			.DistinctBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value);

		public static readonly Dictionary<string, string> DescriptorGenericsLookup =
			(from f in new DirectoryInfo(GeneratorLocations.NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
				let name = Path.GetFileNameWithoutExtension(f.Name).Replace("Request", "")
				let contents = File.ReadAllText(f.FullName)
				let c = Regex.Replace(contents, $@"^.+class ({name}Descriptor(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
				let key = $"{name}Descriptor"
				select new { Key = key, Value = Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1") })
			.DistinctBy(v => v.Key)
			.OrderBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value);

		/// <summary> Scan all NEST files for request interfaces and note any generics declared on them </summary>
		private static readonly List<Tuple<string, string>> AllKnownRequestInterfaces = (
			// find all files in NEST ending with Request.cs
			from f in new DirectoryInfo(GeneratorLocations.NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
			from l in File.ReadLines(f.FullName)
			// attempt to locate all Request interfaces lines
			where Regex.IsMatch(l, @"^.+interface [^ \r\n]+Request")
			//grab the interface name including any generics declared on it
			let c = Regex.Replace(l, @"^.+interface ([^ \r\n]+Request(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
			where c.StartsWith("I") && c.Contains("Request")
			let request = Regex.Replace(c, "<.*$", "")
			let generics = Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1")
			select Tuple.Create(request,  generics)
			)
			.OrderBy(v=>v.Item1)
			.ToList();

		public static readonly HashSet<string> GenericOnlyInterfaces = new HashSet<string>(AllKnownRequestInterfaces
			.GroupBy(v => v.Item1)
			.Where(g => g.All(v => !string.IsNullOrEmpty(v.Item2)))
			.Select(g => g.Key)
			.ToList());

		public static readonly HashSet<string> DocumentRequests = new HashSet<string>((
			// find all files in NEST ending with Request.cs
			from f in new DirectoryInfo(GeneratorLocations.NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
			from l in File.ReadLines(f.FullName)
			// attempt to locate all Request interfaces lines
			where Regex.IsMatch(l, @"^.+interface [^ \r\n]+Request")
			where l.Contains("IDocumentRequest")
			let c = Regex.Replace(l, @"^.+interface ([^ \r\n]+Request(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
			//grab the interface name including any generics declared on it
			let request = Regex.Replace(c, "<.*$", "")
			select request
			)
			.ToList());

		public static readonly Dictionary<string, string> DescriptorConstructors = (
			// find all files in NEST ending with Request.cs
			from f in new DirectoryInfo(GeneratorLocations.NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
			let descriptor = Path.GetFileNameWithoutExtension(f.Name).Replace("Request", "Descriptor")
			let re = $@"^.+public {descriptor}\(([^\r\n\)]+?)\).*$"
			from l in File.ReadLines(f.FullName)
			where Regex.IsMatch(l, re)
			let args = Regex.Replace(l, re, "$1", RegexOptions.Singleline)
			where !string.IsNullOrWhiteSpace(args) && !args.Contains(": base")
			select (Descriptor: descriptor, Args: args)
			)
			.ToDictionary(r => r.Descriptor, r => r.Args);

		public static readonly Dictionary<string, string> RequestInterfaceGenericsLookup =
			AllKnownRequestInterfaces
			.GroupBy(v=>v.Item1)
			.Select(g=>g.Last())
			.ToDictionary(k => k.Item1, v => v.Item2);

		/// <summary>
		/// Some API's reuse response this is a hardcoded map of these cases
		/// </summary>
		private static Dictionary<string, (string, string)> ResponseReroute = new Dictionary<string, (string, string)>
		{
			{"UpdateByQueryRethrottleResponse", ("ListTasksResponse", "")},
			{"DeleteByQueryRethrottleResponse", ("ListTasksResponse", "")},
			{"MultiSearchTemplateResponse", ("MultiSearchResponse", "")},
			{"ScrollResponse", ("SearchResponse", "<TDocument>")},
			{"SearchTemplateResponse", ("SearchResponse", "<TDocument>")},

		};


		/// <summary> Create a dictionary lookup of all responses and their generics </summary>
		public static readonly SortedDictionary<string, (string, string)> ResponseLookup = new SortedDictionary<string, (string, string)>(
		(
			// find all files in NEST ending with Request.cs
			from f in new DirectoryInfo(GeneratorLocations.NestFolder).GetFiles("*Response.cs", SearchOption.AllDirectories)
			from l in File.ReadLines(f.FullName)
			// attempt to locate all Response class lines
			where Regex.IsMatch(l, @"^.+public class [^ \r\n]+Response")
			//grab the response name including any generics declared on it
			let c = Regex.Replace(l, @"^.+public class ([^ \r\n]+Response(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
			where c.Contains("Response")
			let response = Regex.Replace(c, "<.*$", "")
			let generics = Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1")
			select (response,  (response, generics))
		)
			.Concat(ResponseReroute.Select(kv=>(kv.Key, (kv.Value.Item1, kv.Value.Item2))))
			.ToDictionary(t=>t.Item1, t=>t.Item2));

	}
}
