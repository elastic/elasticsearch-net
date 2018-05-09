using Elastic.Managed.Ephemeral;
using Elastic.Xunit;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.ManagedElasticsearch.Tasks;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class XPackClusterConfiguration : ClientTestClusterConfiguration
	{
		public XPackClusterConfiguration() : this(ClusterFeatures.SSL | ClusterFeatures.Security) { }

		public XPackClusterConfiguration(ClusterFeatures features) : base(ClusterFeatures.XPack | features, 1)
		{
			this.ShowElasticsearchOutputAfterStarted = false;

			this.AdditionalBeforeNodeStartedTasks.Add(new EnsureWatcherActionConfigurationInElasticsearchYaml());
		}
	}

	public class XPackCluster : XunitClusterBase<XPackClusterConfiguration>, INestTestCluster
	{
		public XPackCluster() : this(new XPackClusterConfiguration()) { }
		public XPackCluster(XPackClusterConfiguration configuration) : base(configuration) { }

		protected virtual ConnectionSettings Authenticate(ConnectionSettings s) => s
			.BasicAuthentication(ClusterAuthentication.Admin.Username, ClusterAuthentication.Admin.Password);

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
			.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

		public virtual IElasticClient Client => this.GetOrAddClient(ConnectionSettings);

		protected override void SeedCluster() => new DefaultSeeder(this.Client).SeedNode();
	}
}
