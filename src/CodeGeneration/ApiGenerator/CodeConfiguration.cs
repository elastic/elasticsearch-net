using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApiGenerator
{
	public static class CodeConfiguration
	{
		/// <summary> These API"s are not implemented yet in the low and high level client</summary>
		public static string[] IgnoredApis { get; } =
		{
			// these API's are not ready for primetime yet
			"rank_eval.json",

			// these API's are new and need to be mapped
			"ml.set_upgrade_mode.json",
			"ml.find_file_structure.json",
			"monitoring.bulk.json",
			"indices.freeze.json",
			"indices.unfreeze.json",

			"ccr.follow_info.json",
			"ccr.forget_follower.json"
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

		public static readonly Dictionary<string, string> DescriptorGenericsLookup =
			(from f in new DirectoryInfo(GeneratorLocations.NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
				let contents = File.ReadAllText(f.FullName)
				let c = Regex.Replace(contents, @"^.+class ([^ \r\n]+Descriptor(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
				select new { Key = Regex.Replace(c, "<.*$", ""), Value = Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1") })
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

		public static readonly Dictionary<string, string> RequestInterfaceGenericsLookup =
			AllKnownRequestInterfaces
			.GroupBy(v=>v.Item1)
			.Select(g=>g.Last())
			.ToDictionary(k => k.Item1, v => v.Item2);

	}
}
