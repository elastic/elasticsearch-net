using System;
using System.Linq;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit;
using Elasticsearch.Net;
using Nest;
using Tests.ClientConcepts.Certificates;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.ManagedElasticsearch.Plugins;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class XPackClusterConfiguration : ClientTestClusterConfiguration
	{
		public XPackClusterConfiguration() : this(ClusterFeatures.SSL | ClusterFeatures.Security) { }

		public XPackClusterConfiguration(ClusterFeatures features) : base(ClusterFeatures.XPack | features, 1)
		{
			this.Add("xpack.ssl.key", this.FileSystem.NodePrivateKey);
			this.Add("xpack.ssl.certificate", this.FileSystem.NodeCertificate);
			this.Add("xpack.ssl.certificate_authorities", this.FileSystem.CaCertificate);
			this.Add("xpack.security.transport.ssl.enabled", "true");
			if (TestClient.VersionUnderTestSatisfiedBy(">=5.5.0"))
				this.Add("xpack.security.authc.token.enabled", "true");

		}

		//TODO ugly
		public int MaxConcurrencySetter { get; set; }
		public override int MaxConcurrency { get => MaxConcurrencySetter; }
	}
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class XPackCluster : XunitClusterBase<XPackClusterConfiguration>
	{
		public XPackCluster(XPackClusterConfiguration configuration) : base(configuration) { }

		public XPackCluster() : base(new XPackClusterConfiguration()) { }

		protected override ConnectionSettings CreateConnectionSettings(ConnectionSettings s) => this.ConnectionSettings(Authenticate(s));

		protected virtual ConnectionSettings Authenticate(ConnectionSettings s) => s
			.BasicAuthentication("es_admin", "es_admin");

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
			.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

		protected override InstallationTaskBase[] AdditionalInstallationTasks => new [] { new EnableSslAndKpiOnCluster() };

	}
}
