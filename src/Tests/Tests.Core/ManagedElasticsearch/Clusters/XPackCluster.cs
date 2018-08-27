using System;
using System.IO;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Core.ManagedElasticsearch.Tasks;
using Tests.Domain.Extensions;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class XPackClusterConfiguration : ClientTestClusterConfiguration
	{
		public XPackClusterConfiguration() : this(ClusterFeatures.SSL | ClusterFeatures.Security) { }

		public XPackClusterConfiguration(ClusterFeatures features) : base(ClusterFeatures.XPack | features, 1)
		{
			// Get license file path from environment variable
			var licenseFilePath = Environment.GetEnvironmentVariable("ES_LICENSE_FILE");
			if (!string.IsNullOrEmpty(licenseFilePath) && File.Exists(licenseFilePath))
			{
				var licenseContents = File.ReadAllText(licenseFilePath);
				this.XPackLicenseJson = licenseContents;
			}
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

		public virtual IElasticClient Client => this.GetOrAddClient(s=> this.Authenticate(this.ConnectionSettings(s.ApplyDomainSettings())));

		protected override void SeedCluster() => new DefaultSeeder(this.Client).SeedNode();
	}
}
