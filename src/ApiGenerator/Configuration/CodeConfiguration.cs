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
			// Upgrade API no longer relevant, might make a reapearance
			"indices.upgrade.json",
			"indices.get_upgrade.json",

			// these APIs are not ready for primetime yet
			"indices.reload_search_analyzers.json",
			"rank_eval.json",

			// Internal API,
			"monitoring.bulk.json",

			// Already gone in our client
			"indices.exists_type.json",

			// Never exposed and now deprecated
			"data_frame_transform_deprecated.delete_transform.json",
			"data_frame_transform_deprecated.get_transform.json",
			"data_frame_transform_deprecated.get_transform_stats.json",
			"data_frame_transform_deprecated.preview_transform.json",
			"data_frame_transform_deprecated.put_transform.json",
			"data_frame_transform_deprecated.start_transform.json",
			"data_frame_transform_deprecated.stop_transform.json",
			"data_frame_transform_deprecated.update_transform.json",
		};

		public static string[] IgnoredApisHighLevel { get; } = new []
		{
			"scripts_painless_context.json",
			"security.get_builtin_privileges.json",

			// these APIs are new and need to be mapped
			"transform.delete_transform.json",
			"transform.get_transform.json",
			"transform.get_transform_stats.json",
			"transform.preview_transform.json",
			"transform.put_transform.json",
			"transform.start_transform.json",
			"transform.stop_transform.json",
			"transform.update_transform.json",

			"data_frame.delete_data_frame_transform.json",
			"data_frame.get_data_frame_transform.json",
			"data_frame.get_data_frame_transform_stats.json",
			"data_frame.preview_data_frame_transform.json",
			"data_frame.put_data_frame_transform.json",
			"data_frame.start_data_frame_transform.json",
			"data_frame.stop_data_frame_transform.json",
			"data_frame.update_data_frame_transform.json",

			"ml.estimate_memory_usage.json",
			"ml.set_upgrade_mode.json",
			"ml.find_file_structure.json",
			"ml.evaluate_data_frame.json",
			"ml.delete_data_frame_analytics.json",
			"ml.get_data_frame_analytics.json",
			"ml.get_data_frame_analytics_stats.json",
			"ml.put_data_frame_analytics.json",
			"ml.start_data_frame_analytics.json",
			"ml.stop_data_frame_analytics.json",
		};


		/// <summary>
		/// Scan all nest source code files for Requests and look for the [MapsApi(filename)] attribute.
		/// The class name minus Request is used as the canonical .NET name for the API.
		/// </summary>
		public static readonly Dictionary<string, string> ApiNameMapping =
			(from f in new DirectoryInfo(GeneratorLocations.NestFolder).GetFiles("*.cs", SearchOption.AllDirectories)
				let contents = File.ReadAllText(f.FullName)
				let c = Regex.Replace(contents, @"^.+\[MapsApi\(""([^ \r\n]+)""\)\].*$", "$1", RegexOptions.Singleline)
				where !c.Contains(" ") //filter results that did not match
				select new { Value = f.Name.Replace("Request", ""), Key = c.Replace(".json", "") })
			.DistinctBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value.Replace(".cs", ""));

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
