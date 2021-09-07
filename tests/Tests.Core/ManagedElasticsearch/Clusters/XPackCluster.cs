using System;
using System.IO;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Cluster;
using Elastic.Clients.Elasticsearch.Cluster.Health;
using Elastic.Clients.Elasticsearch.Core;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Core.ManagedElasticsearch.Tasks;
using Tests.Domain.Extensions;
using static Elastic.Stack.ArtifactsApi.Products.ElasticsearchPlugin;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class XPackCluster : XunitClusterBase<XPackClusterConfiguration>, ITestCluster
	{
		public XPackCluster() : this(new XPackClusterConfiguration()) { }

		public XPackCluster(XPackClusterConfiguration configuration) : base(configuration) { }

		public virtual IElasticClient Client =>
			this.GetOrAddClient(s => Authenticate(ConnectionSettings(s.ApplyDomainSettings())));

		protected virtual ElasticsearchClientSettings Authenticate(ElasticsearchClientSettings s) => s
			.Authentication(new BasicAuthentication(ClusterAuthentication.Admin.Username,
				ClusterAuthentication.Admin.Password));

		protected virtual ElasticsearchClientSettings ConnectionSettings(ElasticsearchClientSettings s) => s
			.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

		protected sealed override void SeedCluster()
		{
			Client.Cluster.Health(new HealthRequest {WaitForStatus = WaitForStatus.Green});
			//Client.WaitForSecurityIndices();
			SeedNode();
			Client.Cluster.Health(new HealthRequest {WaitForStatus = WaitForStatus.Green});
			//Client.WaitForSecurityIndices();
		}

		protected virtual void SeedNode() => new DefaultSeeder(Client).SeedNode();
	}

	public class XPackClusterConfiguration : ClientTestClusterConfiguration
	{
		public XPackClusterConfiguration() : this(ClusterFeatures.SSL | ClusterFeatures.Security) { }

		public XPackClusterConfiguration(ClusterFeatures features) : base(ClusterFeatures.XPack | features, 1,
			IngestAttachment)
		{
			var isSnapshot = !string.IsNullOrEmpty(Version.PreRelease) &&
			                 Version.PreRelease.ToLower().Contains("snapshot");
			// Get license file path from environment variable
			var licenseFilePath = Environment.GetEnvironmentVariable("ES_LICENSE_FILE");
			if (!isSnapshot && !string.IsNullOrEmpty(licenseFilePath) && File.Exists(licenseFilePath))
			{
				var licenseContents = File.ReadAllText(licenseFilePath);
				XPackLicenseJson = licenseContents;
			}

			TrialMode = XPackTrialMode.Trial;
			AdditionalBeforeNodeStartedTasks.Add(new EnsureWatcherActionConfigurationInElasticsearchYaml());
			AdditionalBeforeNodeStartedTasks.Add(new EnsureWatcherActionConfigurationSecretsInKeystore());
			AdditionalBeforeNodeStartedTasks.Add(new EnsureNativeSecurityRealmEnabledInElasticsearchYaml());
			ShowElasticsearchOutputAfterStarted = true; //this.TestConfiguration.ShowElasticsearchOutputAfterStarted;
		}
	}
}
