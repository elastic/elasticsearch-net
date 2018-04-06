using System.IO;
using System.IO.Compression;
using System.Linq;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Managed.Ephemeral.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.ClientConcepts.Certificates
{
	public class SslAndKpiClusterConfiguration : XPackClusterConfiguration
	{
		public SslAndKpiClusterConfiguration() : base()
		{
			// Skipping bootstrap validation because they call out to elasticsearch and would force
			// The ServerCertificateValidationCallback to return true. Since its cached this would mess with later assertations.
			this.SkipValidation = true;
			this.AdditionalInstallationTasks.Add(new EnableSslAndKpiOnCluster());
		}
	}


	[IntegrationOnly, RequiresPlugin(ElasticsearchPlugin.XPack)]
	public abstract class SslAndKpiXPackCluster : XPackCluster
	{
		public SslAndKpiXPackCluster() : this(new SslAndKpiClusterConfiguration()) { }
		public SslAndKpiXPackCluster(SslAndKpiClusterConfiguration configuration) : base(configuration) { }
	}

	public class EnableSslAndKpiOnCluster : ClusterComposeTask<EphemeralClusterConfiguration>
	{

		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			//TODO in a bind here where I shouldn't be
			var config = cluster.ClusterConfiguration as XPackClusterConfiguration;
			if (config == null) return;

			var fileSystem = cluster.FileSystem;
			//due to a bug in certgen this file needs to live in two places
			var silentModeConfigFile = Path.Combine(fileSystem.ElasticsearchHome, "certgen") + ".yml";
			var silentModeConfigFileDuplicate = Path.Combine(fileSystem.ConfigPath, "x-pack", "certgen") + ".yml";
			foreach (var file in new[] {silentModeConfigFile, silentModeConfigFileDuplicate})
				if (!File.Exists(file))
					File.WriteAllLines(file, new[]
					{
						"instances:",
						$"    - name : \"{config.CertificateNodeName}\"",
						$"    - name : \"{config.ClientCertificateName}\"",
						$"      filename : \"{config.ClientCertificateFilename}\"",
					});

			this.GenerateCertificates(config, silentModeConfigFile, cluster.Writer);
			this.GenerateUnusedCertificates(config, silentModeConfigFile, cluster.Writer);
			this.AddClientCertificateUser(config);
		}

		private void AddClientCertificateUser(XPackClusterConfiguration config)
		{
			var file = Path.Combine(config.FileSystem.ConfigPath, "x-pack", "role_mapping") + ".yml";
			var name = config.ClientCertificateName;
			if (!File.Exists(file) || !File.ReadAllLines(file).Any(f => f.Contains(name)))
				File.WriteAllLines(file, new[]
				{
					"admin:",
					$"    - \"{name}\""
				});
		}

		private void GenerateCertificates(XPackClusterConfiguration config, string silentModeConfigFile, IConsoleLineWriter writer)
		{
			var name = config.CertificateFolderName;
			var zipLocation = Path.Combine(config.FileSystem.ConfigPath, "x-pack", name) + ".zip";
			var @out = config.Version.Major < 6 ? $"{name}.zip" : zipLocation;
			if (!File.Exists(config.CaCertificate))
				ExecuteBinary(writer, config.CertGenBinary, "generating ssl certificates for this session",
					"-in", silentModeConfigFile, "-out", @out);

			if (Directory.Exists(config.CertificatesPath)) return;
			Directory.CreateDirectory(config.CertificatesPath);
			ZipFile.ExtractToDirectory(zipLocation, config.CertificatesPath);
		}

		private void GenerateUnusedCertificates(XPackClusterConfiguration config, string silentModeConfigFile, IConsoleLineWriter writer)
		{
			var name = config.UnusedCertificateFolderName;
			var zipLocation = Path.Combine(config.FileSystem.ConfigPath, "x-pack", name) + ".zip";
			var @out = config.Version.Major < 6 ? $"{name}.zip" : zipLocation;
			if (!File.Exists(config.UnusedCaCertificate))
				ExecuteBinary(writer, config.CertGenBinary, "generating ssl certificates for this session",
					"-in", silentModeConfigFile, "-out", @out);

			if (Directory.Exists(config.UnusedCertificatesPath)) return;
			Directory.CreateDirectory(config.UnusedCertificatesPath);
			ZipFile.ExtractToDirectory(zipLocation, config.UnusedCertificatesPath);
		}

	}
}
