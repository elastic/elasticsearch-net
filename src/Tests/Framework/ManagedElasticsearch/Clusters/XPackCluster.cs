using System;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Plugins;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class XPackCluster : ClusterBase
	{
		protected string[] XPackSettings => TestClient.VersionUnderTestSatisfiedBy(">=5.5.0")
			? new[] {"xpack.security.authc.token.enabled=true"}
			: Array.Empty<string>();

        protected override string[] AdditionalServerSettings => base.AdditionalServerSettings.Concat(this.XPackSettings).ToArray();

		public override ConnectionSettings ClusterConnectionSettings(ConnectionSettings s) =>
			s.BasicAuthentication("es_admin", "es_admin");
	}
}
