using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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

		public static readonly Dictionary<string, string> ApiNameMapping =
			(from f in new DirectoryInfo(NestFolder).GetFiles("*.cs", SearchOption.AllDirectories)
				let contents = File.ReadAllText(f.FullName)
				let c = Regex.Replace(contents, @"^.+\[MapsApi\(""([^ \r\n]+)""\)\].*$", "$1", RegexOptions.Singleline)
				where !c.Contains(" ") //filter results that did not match
				select new { Value = f.Name.Replace("Request", ""), Key = c.Replace(".json", "") })
			.DistinctBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value.Replace(".cs", ""));


		public static readonly Dictionary<string, string> MethodNameOverrides =
			(from f in new DirectoryInfo(NestFolder).GetFiles("*.cs", SearchOption.AllDirectories)
				let contents = File.ReadAllText(f.FullName)
				let c = Regex.Replace(contents, @"^.+\[DescriptorFor\(""([^ \r\n]+)""\)\].*$", "$1", RegexOptions.Singleline)
				where !c.Contains(" ") //filter results that did not match
				select new { Value = f.Name.Replace("Request", ""), Key = c })
			.DistinctBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value.Replace(".cs", ""));

		public static readonly Dictionary<string, string> KnownDescriptors =
			(from f in new DirectoryInfo(NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
				let contents = File.ReadAllText(f.FullName)
				let c = Regex.Replace(contents, @"^.+class ([^ \r\n]+Descriptor(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
				select new { Key = Regex.Replace(c, "<.*$", ""), Value = Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1") })
			.DistinctBy(v => v.Key)
			.OrderBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value);

		public static readonly Dictionary<string, string> KnownRequests =
			(from f in new DirectoryInfo(NestFolder).GetFiles("*Request.cs", SearchOption.AllDirectories)
				let contents = File.ReadAllText(f.FullName)
				let c = Regex.Replace(contents, @"^.+interface ([^ \r\n]+Request(?:<[^>\r\n]+>)?[^ \r\n]*).*$", "$1", RegexOptions.Singleline)
				where c.StartsWith("I") && c.Contains("Request")
				select new { Key = Regex.Replace(c, "<.*$", ""), Value = Regex.Replace(c, @"^.*?(?:(\<.+>).*?)?$", "$1") })
			.DistinctBy(v => v.Key)
			.ToDictionary(k => k.Key, v => v.Value);


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
