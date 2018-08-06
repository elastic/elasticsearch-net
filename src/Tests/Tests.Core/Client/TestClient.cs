using System.Diagnostics;
using System.Linq;
using Elastic.Managed.Configuration;
using Nest;
using Tests.Configuration;
using Tests.Domain.Extensions;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public static class TestClient
	{
		public static readonly IElasticClient Default = new ElasticClient(new TestConnectionSettings().ApplyDomainSettings());
		public static readonly IElasticClient DefaultInMemoryClient = new ElasticClient(new AlwaysInMemoryConnectionSettings().ApplyDomainSettings());
		public static readonly IElasticClient DisabledStreaming = new ElasticClient(new TestConnectionSettings().ApplyDomainSettings().DisableDirectStreaming());

		public static readonly ITestConfiguration Configuration = TestConfiguration.Instance;
	}
}
