using System;
using System.Collections.Generic;

namespace Tests.Framework.Integration
{
	public interface INodeFileSystem
	{
		/// <summary>
		/// The path to elasticsearch.bat
		/// </summary>
		string Binary { get; }

		/// <summary>
		/// The path to (elasticsearch)-plugin.bat
		/// </summary>
		string PluginBinary { get; }

		/// <summary>
		/// The cluster this node should be part of
		/// </summary>
		string ClusterName { get; }
		string NodeName { get; }
		string ElasticsearchHome { get; }
		string ConfigPath { get; }
		string DataPath { get; }
		string LogsPath { get; }
		string RepositoryPath { get; }

		string RoamingFolder { get; }
		string DownloadZipLocation { get; }
		string AnalysisFolder { get; }

	}
}
