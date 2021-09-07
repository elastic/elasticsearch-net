using Elastic.Clients.Elasticsearch;
using Tests.Configuration;
using Tests.Core.Client.Settings;
using Tests.Domain.Extensions;

namespace Tests.Core.Client
{
	public static class TestClient
	{
		public static readonly TestConfigurationBase Configuration = TestConfiguration.Instance;

		public static readonly IElasticClient Default =
			new ElasticClient(new TestElasticsearchClientSettings().ApplyDomainSettings());

		public static readonly IElasticClient DefaultInMemoryClient =
			new ElasticClient(new AlwaysInMemoryElasticsearchClientSettings().ApplyDomainSettings());

		public static readonly IElasticClient DisabledStreaming =
			new ElasticClient(new TestElasticsearchClientSettings().ApplyDomainSettings().DisableDirectStreaming());

		public static IElasticClient FixedInMemoryClient(byte[] response) => new ElasticClient(
			new AlwaysInMemoryElasticsearchClientSettings(response)
				.ApplyDomainSettings()
				.DisableDirectStreaming()
				.EnableHttpCompression(false)
		);
	}
}
