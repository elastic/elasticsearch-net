using Elastic.Managed.Configuration;
using Elasticsearch.Net;
using Tests.Configuration;

namespace Tests.Core.Extensions
{
	public static class TestConfigurationExtensions
	{
		public static IConnection CreateConnection(this TestConfigurationBase configuration, bool forceInMemory = false) =>
			configuration.RunIntegrationTests && !forceInMemory ? (IConnection)new HttpConnection() : new InMemoryConnection();

		public static bool InRange(this TestConfigurationBase configuration, string range) =>
			ElasticsearchVersion.From(configuration.ElasticsearchVersion).InRange(range);
	}
}
