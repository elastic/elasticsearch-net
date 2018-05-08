using Elastic.Managed.Ephemeral.Plugins;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.ClientConcepts.Certificates
{
	public class SslAndKpiClusterConfiguration : XPackClusterConfiguration
	{
		public SslAndKpiClusterConfiguration()
		{
			// Skipping bootstrap validation because they call out to elasticsearch and would force
			// The ServerCertificateValidationCallback to return true. Since its cached this would mess with later assertations.
			this.SkipBuiltInAfterStartTasks = true;
		}
	}

	[IntegrationOnly]
	public abstract class SslAndKpiXPackCluster : XPackCluster
	{
		public SslAndKpiXPackCluster() : this(new SslAndKpiClusterConfiguration()) { }

		public SslAndKpiXPackCluster(SslAndKpiClusterConfiguration configuration) : base(configuration) { }
	}
}
