using Elastic.Managed.Configuration;
using Elasticsearch.Net;
using Tests.Configuration;

namespace Tests.Core.Extensions
{
	public static class TestConfigurationExtensions
	{
		private static IConnection CreateLiveConnection(this ITestConfiguration configuration) =>
			new HttpConnection();

		public static IConnection CreateConnection(this ITestConfiguration configuration, bool forceInMemory = false) =>
			configuration.RunIntegrationTests && !forceInMemory ? configuration.CreateLiveConnection() : new InMemoryConnection();

		public static bool InRange(this ITestConfiguration configuration, string range) =>
			ElasticsearchVersion.From(configuration.ElasticsearchVersion).InRange(range);
	}
}
