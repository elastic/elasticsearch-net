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
		public string Binary => Path.Combine(this.ElasticsearchHome, "bin", "elasticsearch") + ".bat";
		public string PluginBinary =>
			Path.Combine(this.ElasticsearchHome, "bin", (this._version.Major >= 5 ? "elasticsearch-" : "" ) +"plugin") + ".bat";
		public string ConfigPath => Path.Combine(ElasticsearchHome, "config");
		public string DataPath => Path.Combine(ElasticsearchHome, "data", this._clusterName);
		public string LogsPath => Path.Combine(ElasticsearchHome, "logs");
		public string RepositoryPath => Path.Combine(RoamingFolder, "repositories");

		public string RoamingFolder { get; }
		public string AnalysisFolder => Path.Combine(this.ConfigPath, "analysis");
		public string DownloadZipLocation => Path.Combine(this.RoamingFolder, this._version.Zip);
		public string TaskRunnerFile => Path.Combine(this.RoamingFolder, "taskrunner.log");

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
