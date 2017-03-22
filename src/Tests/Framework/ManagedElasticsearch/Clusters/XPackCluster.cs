using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Plugins;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class XPackCluster : ClusterBase
	{
		public override ConnectionSettings ClusterConnectionSettings(ConnectionSettings s) =>
			s.BasicAuthentication("es_admin", "es_admin");
	}
}
