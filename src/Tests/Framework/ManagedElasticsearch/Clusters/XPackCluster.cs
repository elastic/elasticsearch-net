using System;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.ClientConcepts.Certificates;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.ManagedElasticsearch.Plugins;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class XPackCluster : ClusterBase
	{
		private string[] XPackSettings => TestClient.VersionUnderTestSatisfiedBy(">=5.5.0")
			? new[] { "xpack.security.authc.token.enabled=true" }
			: new string[] {} ;

		public override ConnectionSettings ClusterConnectionSettings(ConnectionSettings s) =>
			this.ConnectionSettings(Authenticate(s));

		protected virtual ConnectionSettings Authenticate(ConnectionSettings s) =>
			s.BasicAuthentication("es_admin", "es_admin");

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
			.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

		protected override InstallationTaskBase[] AdditionalInstallationTasks => new [] { new EnableSslAndKpiOnCluster() };

		public override bool EnableSsl { get; } = true;

		protected override string[] AdditionalServerSettings => new []
		{
			$"xpack.ssl.key={this.Node.FileSystem.NodePrivateKey}",
			$"xpack.ssl.certificate={this.Node.FileSystem.NodeCertificate}",
			$"xpack.ssl.certificate_authorities={this.Node.FileSystem.CaCertificate}",
			"xpack.security.transport.ssl.enabled=true",
			"xpack.security.http.ssl.enabled=true",
		}.Concat(this.XPackSettings).ToArray();

	}
}
