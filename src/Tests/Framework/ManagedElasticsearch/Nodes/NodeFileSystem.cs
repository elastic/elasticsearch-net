using System;
using System.IO;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.Versions;

namespace Tests.Framework.ManagedElasticsearch.Nodes
{
	/// <summary> Strongly types file system paths for a node </summary>
	public class NodeFileSystem
	{
		private readonly ElasticsearchVersion _version;
		private readonly string _clusterName;

		public string ElasticsearchHome { get; }
		private static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;
		public string BinarySuffix => IsMono || Path.PathSeparator == '/' ? "" : ".bat";
		public string Binary => Path.Combine(this.ElasticsearchHome, "bin", "elasticsearch") + BinarySuffix;
		public string PluginBinary =>
			Path.Combine(this.ElasticsearchHome, "bin", (this._version.Major >= 5 ? "elasticsearch-" : "" ) +"plugin") + BinarySuffix;
		public string ConfigPath => Path.Combine(ElasticsearchHome, "config");
		public string DataPath => Path.Combine(ElasticsearchHome, "data", this._clusterName);
		public string LogsPath => Path.Combine(ElasticsearchHome, "logs");
		public string RepositoryPath => Path.Combine(RoamingFolder, "repositories");

		public string RoamingFolder { get; }
		public string AnalysisFolder => Path.Combine(this.ConfigPath, "analysis");
		public string DownloadZipLocation => Path.Combine(this.RoamingFolder, this._version.Zip);
		public string TaskRunnerFile => Path.Combine(this.RoamingFolder, "taskrunner.log");


		//certificates
		public string CertGenBinary => Path.Combine(this.ElasticsearchHome, "bin", "x-pack", "certgen") + BinarySuffix;
		public string XPackEnvBinary => Path.Combine(this.ElasticsearchHome, "bin", "x-pack", "x-pack-env") + BinarySuffix;

		public string CertificateFolderName => "node-certificates";
		public string CertificateNodeName => "node01";
		public string ClientCertificateName => "cn=John Doe,ou=example,o=com";
		public string ClientCertificateFilename => "john_doe";
		public string CertificatesPath => Path.Combine(this.ConfigPath, this.CertificateFolderName);
		public string CaCertificate => Path.Combine(this.CertificatesPath, "ca", "ca") + ".crt";
		public string NodePrivateKey => Path.Combine(this.CertificatesPath, this.CertificateNodeName, this.CertificateNodeName) + ".key";
		public string NodeCertificate => Path.Combine(this.CertificatesPath, this.CertificateNodeName, this.CertificateNodeName) + ".crt";
		public string ClientCertificate => Path.Combine(this.CertificatesPath, this.ClientCertificateFilename, this.ClientCertificateFilename) + ".crt";
		public string ClientPrivateKey => Path.Combine(this.CertificatesPath, this.ClientCertificateFilename, this.ClientCertificateFilename) + ".key";

		public string UnusedCertificateFolderName => $"unused-{CertificateFolderName}";
		public string UnusedCertificatesPath => Path.Combine(this.ConfigPath, this.UnusedCertificateFolderName);
		public string UnusedCaCertificate => Path.Combine(this.UnusedCertificatesPath, "ca", "ca") + ".crt";
		public string UnusedClientCertificate => Path.Combine(this.UnusedCertificatesPath, this.ClientCertificateFilename, this.ClientCertificateFilename) + ".crt";

		public NodeFileSystem(ElasticsearchVersion version, string clusterName, string nodeName)
		{
			this._version = version;
			this._clusterName = clusterName;

			var appData = GetApplicationDataDirectory() ?? "/tmp/NEST";
			this.RoamingFolder = Path.Combine(appData, "NEST", this._version.FullyQualifiedVersion);
			this.ElasticsearchHome = Path.Combine(this.RoamingFolder, this._version.FolderInZip);
		}

		private static string GetApplicationDataDirectory()
		{
#if DOTNETCORE
			return Environment.GetEnvironmentVariable("APPDATA");
#else
			return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
#endif
		}
	}
}
