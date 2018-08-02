using System.Diagnostics;
using System.Linq;
using Elastic.Managed.Configuration;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.ManagedElasticsearch;

namespace Tests.Framework
{
	public static class TestClient
	{
		public static readonly ConnectionSettings GlobalDefaultSettings = new TestConnectionSettings();
		public static readonly IElasticClient Default = new ElasticClient(new TestConnectionSettings());
		public static readonly IElasticClient DefaultInMemoryClient = new ElasticClient(new AlwaysInMemoryConnectionSettings());
		public static readonly IElasticClient DisabledStreaming = new ElasticClient(new TestConnectionSettings().DisableDirectStreaming());

		public static readonly bool RunningFiddler = Process.GetProcessesByName("fiddler").Any();
		public static readonly ITestConfiguration Configuration = TestConfiguration.Instance;
	}
}
