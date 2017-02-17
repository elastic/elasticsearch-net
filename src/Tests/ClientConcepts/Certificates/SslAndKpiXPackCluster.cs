using System.IO;
using System.IO.Compression;
using System.Linq;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.ManagedElasticsearch.Plugins;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;

namespace Tests.ClientConcepts.Certificates
{
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public abstract class SslAndKpiXPackCluster : XPackCluster
	{
		public override bool EnableSsl { get; } = true;
		/// <summary>
		/// Skipping bootstrap validation because they call out to elasticsearch and would force
		/// The ServerCertificateValidationCallback to return true. Since i
		/// </summary>
		public override bool SkipValidation { get; } = true;

		protected override InstallationTaskBase[] AdditionalInstallationTasks => new [] { new EnableSslAndKpiOnCluster() };

		protected override string[] AdditionalServerSettings => new []
		{
			$"xpack.ssl.key={this.Node.FileSystem.NodePrivateKey}",
			$"xpack.ssl.certificate={this.Node.FileSystem.NodeCertificate}",
			$"xpack.ssl.certificate_authorities={this.Node.FileSystem.CaCertificate}",
			"xpack.security.transport.ssl.enabled=true",
			"xpack.security.http.ssl.enabled=true",
		};

		private static int _port = 9200;
		private int? _desiredPort;
		public override int DesiredPort
		{
			get
			{
				if (!this._desiredPort.HasValue)
					this._desiredPort = ++_port;
				return this._desiredPort.Value;
			}
		}

		public override ConnectionSettings ClusterConnectionSettings(ConnectionSettings s) =>
			this.ConnectionSettings(Authenticate(s));

		public virtual ConnectionSettings Authenticate(ConnectionSettings s) =>
			s.BasicAuthentication("es_admin", "es_admin");

		protected abstract ConnectionSettings ConnectionSettings(ConnectionSettings s);
	}

	public class EnableSslAndKpiOnCluster : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			//due to a bug in certgen this file needs to live in two places
			var silentModeConfigFile  = Path.Combine(fileSystem.ElasticsearchHome, "certgen") + ".yml";
			var silentModeConfigFileDuplicate  = Path.Combine(fileSystem.ConfigPath, "x-pack", "certgen") + ".yml";
			foreach(var file in new []{silentModeConfigFile, silentModeConfigFileDuplicate})
                if (!File.Exists(file)) File.WriteAllLines(file, new []
                {
                    "instances:",
                    $"    - name : \"{fileSystem.CertificateNodeName}\"",
                    $"    - name : \"{fileSystem.ClientCertificateName}\"",
                    $"      filename : \"{fileSystem.ClientCertificateFilename}\"",
                });

			this.GenerateCertificates(fileSystem, silentModeConfigFile);
			this.GenerateUnusedCertificates(fileSystem, silentModeConfigFile);
			this.AddClientCertificateUser(fileSystem);
		}

		private void AddClientCertificateUser(NodeFileSystem fileSystem)
		{
			var file = Path.Combine(fileSystem.ConfigPath, "x-pack", "role_mapping") + ".yml";
			var name = fileSystem.ClientCertificateName;
            if (!File.Exists(file) || !File.ReadAllLines(file).Any(f=>f.Contains(name))) File.WriteAllLines(file, new []
            {
             	"admin:",
                $"    - \"{name}\""
            });
		}
		private void GenerateCertificates(NodeFileSystem fileSystem, string silentModeConfigFile)
		{
			var name = fileSystem.CertificateFolderName;
			if (!File.Exists(fileSystem.CaCertificate))
				this.ExecuteBinary(fileSystem.CertGenBinary, "generating ssl certificates for this session",
					"-in", silentModeConfigFile, "-out", $"{name}.zip");

			if (Directory.Exists(fileSystem.CertificatesPath)) return;
			Directory.CreateDirectory(fileSystem.CertificatesPath);
			var zipLocation = Path.Combine(fileSystem.ConfigPath, "x-pack", name) + ".zip";
			ZipFile.ExtractToDirectory(zipLocation, fileSystem.CertificatesPath);
		}
		private void GenerateUnusedCertificates(NodeFileSystem fileSystem, string silentModeConfigFile)
		{
			var name = fileSystem.UnusedCertificateFolderName;
			if (!File.Exists(fileSystem.UnusedCaCertificate))
				this.ExecuteBinary(fileSystem.CertGenBinary, "generating ssl certificates for this session",
					"-in", silentModeConfigFile, "-out", $"{name}.zip");

			if (Directory.Exists(fileSystem.UnusedCertificatesPath)) return;
			Directory.CreateDirectory(fileSystem.UnusedCertificatesPath);
			var zipLocation = Path.Combine(fileSystem.ConfigPath, "x-pack", name) + ".zip";
			ZipFile.ExtractToDirectory(zipLocation, fileSystem.UnusedCertificatesPath);
		}
	}
}
