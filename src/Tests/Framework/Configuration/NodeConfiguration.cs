using System.Linq;
using Tests.Framework.Configuration;
using Tests.Framework.Versions;

namespace Tests.Framework.Integration
{
	public class NodeConfiguration : ITestConfiguration
	{
		public TestMode Mode { get; }
		public ElasticsearchVersion ElasticsearchVersion { get; }
		public bool ForceReseed { get; }
		public bool TestAgainstAlreadyRunningElasticsearch { get; }
		public bool RunIntegrationTests { get; }
		public bool RunUnitTests { get; }

		public string TypeOfCluster { get; set; }
		public ElasticsearchPlugin[] RequiredPlugins { get; set; } = { };

		public bool ShieldEnabled => this.RequiredPlugins.Contains(ElasticsearchPlugin.XPack);

		public NodeConfiguration(ITestConfiguration configuration)
		{
			this.Mode = configuration.Mode;
			this.ElasticsearchVersion = configuration.ElasticsearchVersion;
			this.ForceReseed = configuration.ForceReseed;
			this.TestAgainstAlreadyRunningElasticsearch = configuration.TestAgainstAlreadyRunningElasticsearch;
			this.RunIntegrationTests = configuration.RunIntegrationTests;
			this.RunUnitTests = configuration.RunUnitTests;
		}
	}
}
