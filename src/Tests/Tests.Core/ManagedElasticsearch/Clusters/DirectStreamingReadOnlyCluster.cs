using Elastic.Managed.Ephemeral.Plugins;
using Nest;
using Tests.Core.ManagedElasticsearch.NodeSeeders;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class DirectStreamingReadOnlyCluster : ClientTestClusterBase
	{
		public DirectStreamingReadOnlyCluster() : base(ElasticsearchPlugin.MapperMurmur3) { }

		protected override ConnectionSettings ConnectionSettings(ConnectionSettings s) => s.DisableDirectStreaming(false);

		protected override void SeedCluster() => new DefaultSeeder(Client).SeedNode();
	}
}
