using System.IO;
using System.IO.Compression;
using System.Linq;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.ManagedElasticsearch.Plugins;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;
using Tests.Framework.Versions;

namespace Tests.ClientConcepts.Certificates
{
	[IntegrationOnly, RequiresPlugin(ElasticsearchPlugin.XPack)]
	public abstract class SslAndKpiXPackCluster : XPackCluster
	{
		/// <summary>
		/// Skipping bootstrap validation because they call out to elasticsearch and would force
		/// The ServerCertificateValidationCallback to return true. Since its cached this would mess with later assertations.
		/// </summary>
		public override bool SkipValidation { get; } = true;

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

			this.GenerateCertificates(fileSystem, silentModeConfigFile, config.ElasticsearchVersion);
			this.GenerateUnusedCertificates(fileSystem, silentModeConfigFile, config.ElasticsearchVersion);
			this.AddClientCertificateUser(fileSystem, config.ElasticsearchVersion);
		}

		private void AddClientCertificateUser(NodeFileSystem fileSystem, ElasticsearchVersion version)
		{
			var file = Path.Combine(fileSystem.ConfigPath, "x-pack", "role_mapping") + ".yml";
			var name = fileSystem.ClientCertificateName;
            if (!File.Exists(file) || !File.ReadAllLines(file).Any(f=>f.Contains(name))) File.WriteAllLines(file, new []
            {
             	"admin:",
                $"    - \"{name}\""
            });
		}
		private void GenerateCertificates(NodeFileSystem fileSystem, string silentModeConfigFile, ElasticsearchVersion version)
		{
			var name = fileSystem.CertificateFolderName;
			var zipLocation = Path.Combine(fileSystem.ConfigPath, "x-pack", name) + ".zip";
			var @out = version.Major < 6 ? $"{name}.zip" : zipLocation;
			if (!File.Exists(fileSystem.CaCertificate))
				this.ExecuteBinary(fileSystem.CertGenBinary, "generating ssl certificates for this session",
					"-in", silentModeConfigFile, "-out", @out);

			if (Directory.Exists(fileSystem.CertificatesPath)) return;
			Directory.CreateDirectory(fileSystem.CertificatesPath);
			ZipFile.ExtractToDirectory(zipLocation, fileSystem.CertificatesPath);
		}
		private void GenerateUnusedCertificates(NodeFileSystem fileSystem, string silentModeConfigFile, ElasticsearchVersion version)
		{
			var name = fileSystem.UnusedCertificateFolderName;
			var zipLocation = Path.Combine(fileSystem.ConfigPath, "x-pack", name) + ".zip";
			var @out = version.Major < 6 ? $"{name}.zip" : zipLocation;
			if (!File.Exists(fileSystem.UnusedCaCertificate))
				this.ExecuteBinary(fileSystem.CertGenBinary, "generating ssl certificates for this session",
					"-in", silentModeConfigFile, "-out", @out);

			if (Directory.Exists(fileSystem.UnusedCertificatesPath)) return;
			Directory.CreateDirectory(fileSystem.UnusedCertificatesPath);
			ZipFile.ExtractToDirectory(zipLocation, fileSystem.UnusedCertificatesPath);
		}
	}
}
