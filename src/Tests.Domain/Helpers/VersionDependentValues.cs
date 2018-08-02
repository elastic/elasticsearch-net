using Elastic.Managed.Configuration;

namespace Tests.Framework.ManagedElasticsearch
{
	public static class VersionDependentValues
	{
		private static bool InRange(string range) =>
			ElasticsearchVersion.From(TestConfiguration.Instance.ElasticsearchVersion).InRange(range);

		public static string PercolatorType => InRange("<5.0.0-alpha1") ? ".percolator" : "query";

		public const string ProjectsIndex = "project";
	}
}
