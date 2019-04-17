using Elastic.Managed.Configuration;
using Elasticsearch.Net;
using Tests.Configuration;

namespace Tests.Core.Extensions
{
	public static class TestConfigurationExtensions
	{
		private static IConnection CreateLiveConnection(this TestConfigurationBase configuration) =>
			new HttpConnection();

		public static IConnection CreateConnection(this TestConfigurationBase configuration, bool forceInMemory = false) =>
			configuration.RunIntegrationTests && !forceInMemory ? configuration.CreateLiveConnection() : new InMemoryConnection();

		public static bool InRange(this TestConfigurationBase configuration, string range) =>
			ElasticsearchVersion.From(configuration.ElasticsearchVersion).InRange(range);
	}
}
