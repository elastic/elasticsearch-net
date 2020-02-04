using System.IO;
using System.Reflection;

namespace ApiGenerator.Configuration
{
	public static class GeneratorLocations
	{
		// @formatter:off — disable formatter after this line
		public static string EsNetFolder { get; } = $@"{Root}../../src/Elasticsearch.Net/";
		public static string LastDownloadedVersionFile { get; } = Path.Combine(Root, "last_downloaded_version.txt");

		public static string NestFolder { get; } = $@"{Root}../../src/Nest/";
		public static string RestSpecificationFolder { get; } = $@"{Root}RestSpecification/";
		// @formatter:on — enable formatter after this line

		public static string HighLevel(params string[] paths) => NestFolder + string.Join("/", paths);
		public static string LowLevel(params string[] paths) => EsNetFolder + string.Join("/", paths);

		public static readonly Assembly Assembly = typeof(Generator.ApiGenerator).Assembly;

		private static string _root;
		public static string Root
		{
			get
			{
				if (_root != null) return _root;

				var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

				var dotnetRun =
					directoryInfo.Name == "ApiGenerator" &&
					directoryInfo.Parent != null &&
					directoryInfo.Parent.Name == "src";

				_root = dotnetRun ? "" : @"../../../";
				return _root;
			}
		}

	}
}
