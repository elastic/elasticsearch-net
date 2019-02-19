using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace ApiGenerator
{
	public static class CodeConfiguration
	{
		private static string _root = null;

		// @formatter:off — disable formatter after this line
		public static string EsNetFolder { get; } = $@"{Root}..\..\..\src\Elasticsearch.Net\";
		public static string LastDownloadedVersionFile { get; } = Path.Combine(Root, "last_downloaded_version.txt");

		public static string NestFolder { get; } = $@"{Root}..\..\..\src\Nest\";
		public static string RestSpecificationFolder { get; } = $@"{Root}RestSpecification\";
		public static string ViewFolder { get; } = $@"{Root}Views\";
		// @formatter:on — enable formatter after this line

		public static readonly Assembly Assembly = typeof(ApiGenerator).Assembly;

		public static string[] IgnoredApis { get; } =
		{
			// these API's are not ready for primetime yet
			"ml.delete_filter.json",
			"ml.get_filters.json",
			"ml.put_filter.json",
			"rank_eval.json",

			// these API's are new and need to be mapped
			"ml.delete_calendar.json",
			"ml.delete_calendar_event.json",
			"ml.delete_calendar_job.json",
			"ml.get_calendar_events.json",
			"ml.get_calendars.json",
			"ml.info.json",
			"ml.post_calendar_events.json",
			"ml.put_calendar.json",
			"ml.put_calendar_job.json",
			"ml.get_calendar_job.json",
			"ml.delete_forecast.json",
			"ml.delete_filter.json",
			"ml.set_upgrade_mode.json",
			"ml.put_filter.json",
			"ml.find_file_structure.json",
			"monitoring.bulk.json",
			"delete_by_query_rethrottle.json",
			"update_by_query_rethrottle.json",

			"ml.update_filter.json",
			"security.delete_privileges.json",
			"security.get_privileges.json",
			"security.put_privileges.json",
			"security.get_user_privileges.json",
			"security.get_index_privileges.json",
			"security.has_privileges.json",
			"security.put_privilege.json",
			"security.create_api_key.json",
			"security.get_api_key.json",
			"security.invalidate_api_key.json",

			"ilm.delete_lifecycle.json",
			"ilm.explain_lifecycle.json",
			"ilm.get_lifecycle.json",
			"ilm.get_status.json",
			"ilm.move_to_step.json",
			"ilm.put_lifecycle.json",
			"ilm.remove_policy.json",
			"ilm.retry.json",
			"ilm.start.json",
			"ilm.stop.json",

			"indices.freeze.json",
			"indices.unfreeze.json",

			"ccr.follow_info.json",



		};


		public static readonly Dictionary<string, string> ApiNameMapping =
			(from f in new DirectoryInfo(NestFolder).GetFiles("*.cs", SearchOption.AllDirectories)
				let contents = File.ReadAllText(f.FullName)
				let c = Regex.Replace(contents, @"^.+\[MapsApi\(""([^ \r\n]+)""\)\].*$", "$1", RegexOptions.Singleline)
				where !c.Contains(" ") //filter results that did not match
				select new { Value = f.Name.Replace("Request", ""), Key = c.Replace(".json", "") })
			.DistinctBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value.Replace(".cs", ""));

		public static readonly Dictionary<string, string> MethodNameOverrides = new Dictionary<string, string>();

		public static readonly Dictionary<string, string> KnownDescriptors =
			(from f in new DirectoryInfo(NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
				let contents = File.ReadAllText(f.FullName)
				let c = Regex.Replace(contents, @"^.+class ([^ \r\n]+Descriptor(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
				select new { Key = Regex.Replace(c, "<.*$", ""), Value = Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1") })
			.DistinctBy(v => v.Key)
			.OrderBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value);

		private static readonly List<Tuple<string, string>> AllKnownRequests = (
			from f in new DirectoryInfo(NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
			from l in File.ReadLines(f.FullName)
			where Regex.IsMatch(l, @"^.+interface [^ \r\n]+Request")
			let c = Regex.Replace(l, @"^.+interface ([^ \r\n]+Request(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
			where c.StartsWith("I") && c.Contains("Request")
			let key = Regex.Replace(c, "<.*$", "")
			let value = Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1")
			select Tuple.Create(key,  value)
			).OrderBy(v=>v.Item1).ToList();

		public static readonly Dictionary<string, string> KnownRequests =
			AllKnownRequests
			.GroupBy(v=>v.Item1)
			.Select(g=>g.Last())
			.ToDictionary(k => k.Item1, v => v.Item2);

		public static readonly Dictionary<string, int> NumberOfDeclaredRequests =
			AllKnownRequests
			.GroupBy(v=>v.Item1)
			.Where(v => v.Count() > 1)
			.ToDictionary(k => k.Key, v => v.Count());

		private static string Root
		{
			get
			{
				if (_root != null) return _root;

				var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

				var runningAsDnx =
					directoryInfo.Name == "ApiGenerator" &&
					directoryInfo.Parent != null &&
					directoryInfo.Parent.Name == "CodeGeneration";

				_root = runningAsDnx ? "" : @"..\..\..\";
				return _root;
			}
		}
	}
}
