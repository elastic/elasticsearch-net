using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.Plugins;
using Tests.Framework.Versions;

namespace Tests.Framework.ManagedElasticsearch.Nodes
{
	public class NodeConfiguration : ITestConfiguration
	{
		private readonly ClusterBase _cluster;
		public TestMode Mode { get; }
		public ElasticsearchVersion ElasticsearchVersion { get; }
		public bool ForceReseed { get; }
		public bool TestAgainstAlreadyRunningElasticsearch { get; }
		public bool RunIntegrationTests { get; }
		public bool RunUnitTests { get; }
		public string ClusterFilter { get; }
		public string TestFilter { get; }
		public NodeFileSystem FileSystem { get; }
		public int DesiredPort { get; }

		public ElasticsearchPlugin[] RequiredPlugins { get; } = { };

		public bool XPackEnabled => this.RequiredPlugins.Contains(ElasticsearchPlugin.XPack);
		public bool EnableSsl { get; }
		public ConnectionSettings ClusterConnectionSettings(ConnectionSettings s) => _cluster.ClusterConnectionSettings(s);

		private readonly string _uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6);
		public string ClusterMoniker => this._cluster.GetType().Name.Replace("Cluster", "").ToLowerInvariant();
		public string ClusterName => $"{this.ClusterMoniker}-cluster-{_uniqueSuffix}";
		public string NodeName => $"{this.ClusterMoniker}-node-{_uniqueSuffix}";

		private List<string> DefaultNodeSettings { get; }

		public NodeConfiguration(ITestConfiguration configuration, ClusterBase cluster)
		{
			this._cluster = cluster;
			this.EnableSsl = cluster.SkipValidation;

			this.RequiredPlugins = ClusterRequiredPlugins(cluster);
			this.Mode = configuration.Mode;

			var v = configuration.ElasticsearchVersion;
			this.ElasticsearchVersion = v;
			this.ForceReseed = configuration.ForceReseed;
			this.TestAgainstAlreadyRunningElasticsearch = configuration.TestAgainstAlreadyRunningElasticsearch;
			this.RunIntegrationTests = configuration.RunIntegrationTests;
			this.RunUnitTests = configuration.RunUnitTests;
			this.ClusterFilter = configuration.ClusterFilter;
			this.TestFilter = configuration.TestFilter;
			this.FileSystem = new NodeFileSystem(configuration.ElasticsearchVersion, this.ClusterName, this.NodeName);
			this.DesiredPort = cluster.DesiredPort;

			var attr = v.Major >= 5 ? "attr." : "";
			var indexedOrStored = v > ElasticsearchVersion.GetOrAdd("5.0.0-alpha1") ? "stored" : "indexed";
			var shieldOrSecurity = v > ElasticsearchVersion.GetOrAdd("5.0.0-alpha1") ? "xpack.security" : "shield";
			var es = v > ElasticsearchVersion.GetOrAdd("5.0.0-alpha2") ? "" : "es.";
			var b = this.XPackEnabled.ToString().ToLowerInvariant();
			var sslEnabled = this.EnableSsl.ToString().ToLowerInvariant();
			this.DefaultNodeSettings = new List<string>
			{
				$"{es}cluster.name={this.ClusterName}",
				$"{es}node.name={this.NodeName}",
				$"{es}path.repo={this.FileSystem.RepositoryPath}",
				$"{es}path.data={this.FileSystem.DataPath}",
				$"{es}http.port={this.DesiredPort}",
				$"{es}script.max_compilations_per_minute=10000",
				$"{es}node.{attr}testingcluster=true",
				$"{es}node.{attr}gateway=true",
				$"{es}{shieldOrSecurity}.enabled={b}",
				$"{es}{shieldOrSecurity}.http.ssl.enabled={sslEnabled}",
				$"{es}{shieldOrSecurity}.authc.realms.pki1.enabled={sslEnabled}",
				$"{es}search.remote.connect=true"
			};

			if (v < ElasticsearchVersion.GetOrAdd("5.5.0"))
			{
				this.DefaultNodeSettings.AddRange(new [] {
					$"{es}script.inline=true",
					$"{es}script.{indexedOrStored}=true",
				});
			}
			else
			{
				this.DefaultNodeSettings.AddRange(new [] {
					$"script.allowed_types=inline,stored",
				});
			}
		}

		public string[] CreateSettings(string[] additionalSettings)
		{
			var settingMarker = this.ElasticsearchVersion.Major >= 5 ? "-E " : "-D";
			return DefaultNodeSettings
				.Concat(additionalSettings ?? Enumerable.Empty<string>())
				//allow additional settings to take precedence over already DefaultNodeSettings
				//without relying on elasticsearch to dedup, 5.4.0 no longer allows passing the same setting twice
				//on the command with the latter taking precedence
				.GroupBy(setting => setting.Split(new [] {'='}, 2, StringSplitOptions.RemoveEmptyEntries)[0])
				.Select(g => g.Last())
				.Select(s => $"{settingMarker}{s}")
				.ToArray();
		}

		private static ElasticsearchPlugin[] ClusterRequiredPlugins(ClusterBase cluster) =>
			cluster.GetType().GetAttributes<RequiresPluginAttribute>().SelectMany(a => a.Plugins).ToArray();
	}
}
