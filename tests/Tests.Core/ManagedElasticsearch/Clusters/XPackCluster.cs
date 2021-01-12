// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit;
using Elastic.Stack.ArtifactsApi.Products;
using Elastic.Transport;
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

		public XPackClusterConfiguration(ClusterFeatures features) : base(ClusterFeatures.XPack | features, 1, ElasticsearchPlugin.IngestAttachment)
		{
			var isSnapshot = !string.IsNullOrEmpty(Version.PreRelease) && Version.PreRelease.ToLower().Contains("snapshot");
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

	public class XPackCluster : XunitClusterBase<XPackClusterConfiguration>, INestTestCluster
	{
		public XPackCluster() : this(new XPackClusterConfiguration()) { }

		public XPackCluster(XPackClusterConfiguration configuration) : base(configuration) { }

		public virtual IElasticClient Client => this.GetOrAddClient(s => Authenticate(ConnectionSettings(s.ApplyDomainSettings())));

		protected virtual ConnectionSettings Authenticate(ConnectionSettings s) => s
			.Authentication(new BasicAuthentication(ClusterAuthentication.Admin.Username, ClusterAuthentication.Admin.Password));

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
			.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

		protected sealed override void SeedCluster()
		{
			Client.Cluster.Health(new ClusterHealthRequest { WaitForStatus = WaitForStatus.Green });
			Client.WaitForSecurityIndices();
			SeedNode();
			Client.Cluster.Health(new ClusterHealthRequest { WaitForStatus = WaitForStatus.Green });
			Client.WaitForSecurityIndices();
		}

		protected virtual void SeedNode() => new DefaultSeeder(Client).SeedNode();
	}
}
