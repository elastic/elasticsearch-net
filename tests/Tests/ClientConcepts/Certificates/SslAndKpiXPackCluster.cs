using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;

namespace Tests.ClientConcepts.Certificates
{
	public class SslAndKpiClusterConfiguration : XPackClusterConfiguration
	{
		public SslAndKpiClusterConfiguration() => SkipBuiltInAfterStartTasks = true;
	}

	[IntegrationOnly]
	public abstract class SslAndKpiXPackCluster : XPackCluster
	{
		public SslAndKpiXPackCluster() : this(new SslAndKpiClusterConfiguration()) { }

		public SslAndKpiXPackCluster(SslAndKpiClusterConfiguration configuration) : base(configuration) { }

		protected override void SeedCluster() { }
	}
}
