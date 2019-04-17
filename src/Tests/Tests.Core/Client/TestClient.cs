using Nest;
using Nest.JsonNetSerializer;
using Tests.Configuration;
using Tests.Core.Client.Settings;
using Tests.Domain.Extensions;

namespace Tests.Core.Client
{
	public static class TestClient
	{
		public static readonly TestConfigurationBase Configuration = TestConfiguration.Instance;
		public static readonly IElasticClient Default = new ElasticClient(new TestConnectionSettings().ApplyDomainSettings());
		public static readonly IElasticClient DefaultInMemoryClient = new ElasticClient(new AlwaysInMemoryConnectionSettings().ApplyDomainSettings());

		public static readonly IElasticClient DisabledStreaming =
			new ElasticClient(new TestConnectionSettings().ApplyDomainSettings().DisableDirectStreaming());

		public static readonly IElasticClient InMemoryWithJsonNetSerializer = new ElasticClient(
			new AlwaysInMemoryConnectionSettings(sourceSerializerFactory: JsonNetSerializer.Default).ApplyDomainSettings());
	}
}
