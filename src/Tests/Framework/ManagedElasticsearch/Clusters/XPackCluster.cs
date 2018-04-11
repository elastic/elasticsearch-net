using System.IO;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Xunit;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class XPackClusterConfiguration : ClientTestClusterConfiguration
	{
		public XPackClusterConfiguration() : this(ClusterFeatures.SSL | ClusterFeatures.Security) { }

		public XPackClusterConfiguration(ClusterFeatures features) : base(ClusterFeatures.XPack | features, 1)
		{
			this.ShowElasticsearchOutputAfterStarted = false;

			this.Add("xpack.ssl.key", this.NodePrivateKey);
			this.Add("xpack.ssl.certificate", this.NodeCertificate);
			this.Add("xpack.ssl.certificate_authorities", this.CaCertificate);
			this.Add("xpack.security.transport.ssl.enabled", "true");
			this.Add("xpack.security.authc.token.enabled", "true", ">=5.5.0");

			this.AdditionalInstallationTasks.Add(new EnsureSecurityRealms());
			this.AdditionalInstallationTasks.Add(new EnsureSecurityRolesFileExists());
			this.AdditionalInstallationTasks.Add(new EnsureSecurityUsersInDefaultRealmAreAdded());
			this.AdditionalInstallationTasks.Add(new EnsureWatcherActionConfigurationInElasticsearchYaml());
		}

		//certificates
		public string CertGenBinary => Path.Combine(this.FileSystem.ElasticsearchHome, "bin", "x-pack", "certgen") + BinarySuffix;
		public string XPackEnvBinary => Path.Combine(this.FileSystem.ElasticsearchHome, "bin", "x-pack", "x-pack-env") + BinarySuffix;

		public string CertificateFolderName => "node-certificates";
		public string CertificateNodeName => "node01";
		public string ClientCertificateName => "cn=John Doe,ou=example,o=com";
		public string ClientCertificateFilename => "john_doe";
		public string CertificatesPath => Path.Combine(this.FileSystem.ConfigPath, this.CertificateFolderName);
		public string CaCertificate => Path.Combine(this.CertificatesPath, "ca", "ca") + ".crt";
		public string NodePrivateKey => Path.Combine(this.CertificatesPath, this.CertificateNodeName, this.CertificateNodeName) + ".key";
		public string NodeCertificate => Path.Combine(this.CertificatesPath, this.CertificateNodeName, this.CertificateNodeName) + ".crt";
		public string ClientCertificate => Path.Combine(this.CertificatesPath, this.ClientCertificateFilename, this.ClientCertificateFilename) + ".crt";
		public string ClientPrivateKey => Path.Combine(this.CertificatesPath, this.ClientCertificateFilename, this.ClientCertificateFilename) + ".key";

		public string UnusedCertificateFolderName => $"unused-{CertificateFolderName}";
		public string UnusedCertificatesPath => Path.Combine(this.FileSystem.ConfigPath, this.UnusedCertificateFolderName);
		public string UnusedCaCertificate => Path.Combine(this.UnusedCertificatesPath, "ca", "ca") + ".crt";
		public string UnusedClientCertificate => Path.Combine(this.UnusedCertificatesPath, this.ClientCertificateFilename, this.ClientCertificateFilename) + ".crt";
	}


	public class XPackCluster : XunitClusterBase<XPackClusterConfiguration>, INestTestCluster
	{
		public XPackCluster() : this(new XPackClusterConfiguration()) { }

		public XPackCluster(XPackClusterConfiguration configuration) : base(configuration)
		{
			this.Plugins.Add(ElasticsearchPlugin.XPack);
		}

		protected virtual ConnectionSettings Authenticate(ConnectionSettings s) => s
			.BasicAuthentication("es_admin", "es_admin");

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
			.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

		public IElasticClient Client => this.GetOrAddClient(ConnectionSettings);
	}
}
