using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.Versions;
using Tests.XPack.Security;

namespace Tests.Framework.Integration
{
	/// <summary>
	/// Keeps track of paths on disk. When run in integration mode in charge of making sure the filesystem is all configured
	/// and ready to for Elasticsearch and the cluster's required plugin be started
	/// </summary>
	public class TestRunnerFileSystem : INodeFileSystem
	{
		private readonly string _name;
		private readonly NodeConfiguration _config;
		private ElasticsearchVersion Version => _config.ElasticsearchVersion;

		private string TypeOfCluster => _name.ToLowerInvariant();
		public string ClusterName { get; }
		public string NodeName { get; }

		public string ElasticsearchHome { get; }
		public string Binary => Path.Combine(this.ElasticsearchHome, "bin", "elasticsearch") + ".bat";
		public string PluginBinary =>
			Path.Combine(this.ElasticsearchHome, "bin", (this.Version.Major >= 5 ? "elasticsearch-" : "" ) +"plugin") + ".bat";
		public string ConfigPath => Path.Combine(ElasticsearchHome, "config");
		public string DataPath => Path.Combine(ElasticsearchHome, "data", this.ClusterName);
		public string LogsPath => Path.Combine(ElasticsearchHome, "logs");
		public string RepositoryPath => Path.Combine(RoamingFolder, "repositories");

		public string RoamingFolder { get; }
		public string AnalysisFolder => Path.Combine(this.ConfigPath, "analysis");
		public string DownloadZipLocation => Path.Combine(this.RoamingFolder, this.Version.Zip);

		public TestRunnerFileSystem(NodeConfiguration config)
		{
			this._config = config;
			this._name = config.TypeOfCluster;

			var prefix = this._name.ToLowerInvariant();
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			this.ClusterName = $"{prefix}-cluster-{suffix}";
			this.NodeName = $"{prefix}-node-{suffix}";

			var appData = GetApplicationDataDirectory() ?? "/tmp/NEST";
			this.RoamingFolder = Path.Combine(appData, "NEST", this.Version.FullyQualifiedVersion);
			this.ElasticsearchHome = Path.Combine(this.RoamingFolder, this.Version.FolderInZip);
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
