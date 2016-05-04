using System;
using System.Collections.Generic;

namespace Tests.Framework.Integration
{
	public interface INodeFileSystem : IDisposable
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

		/// <summary>
		/// Called by ElasticsearchNode just before a node is started
		/// </summary>
		/// <param name="settings">The settings the node wants to start with</param>
		void BeforeStart(IEnumerable<string> settings);
	}
}
