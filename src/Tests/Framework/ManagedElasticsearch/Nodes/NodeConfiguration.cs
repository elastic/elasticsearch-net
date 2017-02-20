using System;
using System.Collections.Generic;
using System.Linq;
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

		public ElasticsearchPlugin[] RequiredPlugins { get; } = { };

		public bool XPackEnabled => this.RequiredPlugins.Contains(ElasticsearchPlugin.XPack);

		private readonly string _uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6);
		public string ClusterMoniker => this._cluster.GetType().Name.Replace("Cluster", "").ToLowerInvariant();
		public string ClusterName => $"{this.ClusterMoniker}-cluster-{_uniqueSuffix}";
		public string NodeName => $"{this.ClusterMoniker}-node-{_uniqueSuffix}";

		private List<string> DefaultNodeSettings { get; }

		public NodeConfiguration(ITestConfiguration configuration, ClusterBase cluster)
		{
			this._cluster = cluster;

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

			var attr = v.Major >= 5 ? "attr." : "";
			var indexedOrStored = v > new ElasticsearchVersion("5.0.0-alpha1") ? "stored" : "indexed";
			var shieldOrSecurity = v > new ElasticsearchVersion("5.0.0-alpha1") ? "security" : "shield";
			var es = v > new ElasticsearchVersion("5.0.0-alpha2") ? "" : "es.";
			var b = this.XPackEnabled.ToString().ToLowerInvariant();
			this.DefaultNodeSettings = new List<string>
			{
				$"{es}cluster.name={this.ClusterName}",
				$"{es}node.name={this.NodeName}",
				$"{es}path.repo={this.FileSystem.RepositoryPath}",
				$"{es}path.data={this.FileSystem.DataPath}",
				$"{es}script.inline=true",
				$"{es}script.max_compilations_per_minute=10000",
				$"{es}script.{indexedOrStored}=true",
				$"{es}node.{attr}testingcluster=true",
				$"{es}xpack.{shieldOrSecurity}.enabled={b}"
			};
		}

		public string[] CreateSettings(string[] additionalSettings)
		{
			var settingMarker = this.ElasticsearchVersion.Major >= 5 ? "-E " : "-D";
			return DefaultNodeSettings
				.Concat(additionalSettings ?? Enumerable.Empty<string>())
				.Select(s => $"{settingMarker}{s}")
				.ToArray();
		}

		private static ElasticsearchPlugin[] ClusterRequiredPlugins(ClusterBase cluster) =>
			cluster.GetType().GetAttributes<RequiresPluginAttribute>().SelectMany(a => a.Plugins).ToArray();
	}
}
