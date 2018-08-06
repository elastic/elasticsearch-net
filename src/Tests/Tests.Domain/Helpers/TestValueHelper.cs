using System;
using Elastic.Managed.Configuration;
using Tests.Configuration;

namespace Tests.Domain.Helpers
{
	public static class TestValueHelper
	{
		private static bool InRange(string range) => ElasticsearchVersion.From(TestConfiguration.Instance.ElasticsearchVersion).InRange(range);

		public const string ProjectsIndex = "project";

		public static DateTime FixedDate { get; } = new DateTime(2015, 06, 06, 12, 01, 02, 123);

		public static string PercolatorType => InRange("<5.0.0-alpha1") ? ".percolator" : "query";

		public static object Dependant(object builtin, object source) => TestConfiguration.Instance.Random.SourceSerializer ? source : builtin;

	}
}
