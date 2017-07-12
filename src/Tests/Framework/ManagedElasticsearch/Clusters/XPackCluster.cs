using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Plugins;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class XPackCluster : ClusterBase
	{
        protected override string[] AdditionalServerSettings => base.AdditionalServerSettings.Concat(new[]
        {
	        "xpack.security.authc.token.enabled=true"
        }).ToArray();

		public override ConnectionSettings ClusterConnectionSettings(ConnectionSettings s) =>
			s.BasicAuthentication("es_admin", "es_admin");
	}
}
