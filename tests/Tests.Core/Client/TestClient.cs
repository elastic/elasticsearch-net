using Elastic.Clients.Elasticsearch;
using Tests.Configuration;
using Tests.Core.Client.Settings;
using Tests.Domain.Extensions;

namespace Tests.Core.Client
{
	public static class TestClient
	{
		public static readonly TestConfigurationBase Configuration = TestConfiguration.Instance;

		public static readonly IElasticsearchClient Default =
			new ElasticsearchClient(new TestElasticsearchClientSettings().ApplyDomainSettings());

		public static readonly IElasticsearchClient DefaultInMemoryClient =
			new ElasticsearchClient(new AlwaysInMemoryElasticsearchClientSettings().ApplyDomainSettings());

		public static readonly IElasticsearchClient DisabledStreaming =
			new ElasticsearchClient(new TestElasticsearchClientSettings().ApplyDomainSettings().DisableDirectStreaming());

		public static IElasticsearchClient FixedInMemoryClient(byte[] response) => new ElasticsearchClient(
			new AlwaysInMemoryElasticsearchClientSettings(response)
				.ApplyDomainSettings()
				.DisableDirectStreaming()
				.EnableHttpCompression(false)
		);
	}
}
