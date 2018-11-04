using Nest;
using Tests.Configuration;
using Tests.Core.Client.Settings;
using Tests.Domain.Extensions;

namespace Tests.Core.Client
{
	public static class TestClient
	{
		public static readonly ITestConfiguration Configuration = TestConfiguration.Instance;
		public static readonly IElasticClient Default = new ElasticClient(new TestConnectionSettings().ApplyDomainSettings());
		public static readonly IElasticClient DefaultInMemoryClient = new ElasticClient(new AlwaysInMemoryConnectionSettings().ApplyDomainSettings());

		public static readonly IElasticClient DisabledStreaming =
			new ElasticClient(new TestConnectionSettings().ApplyDomainSettings().DisableDirectStreaming());
	}
}
