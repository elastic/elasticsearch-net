using System;
using System.Collections.Generic;
using System.IO;
using Elastic.Managed.Configuration;
using Elastic.Xunit;
using Elastic.Xunit.XunitPlumbing;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.FileSystem;
using Tests.Framework.Configuration;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;
using Xunit.Abstractions;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class ClientTestClusterConfiguration : XunitClusterConfiguration
	{
		public ITestConfiguration TestConfiguration { get; }

		public ClientTestClusterConfiguration(ClusterFeatures features = ClusterFeatures.None, int numberOfNodes = 1)
			: base(TestClient.Configuration.ElasticsearchVersion, features, numberOfNodes)
		{
			this.TestConfiguration = TestClient.Configuration;

			this.Add(this.AttributeKey("testingcluster"), "true");
			this.Add(this.AttributeKey("gateway"), "true");
			this.Add("search.remote.connect", "true");

			this.Add($"script.max_compilations_per_minute", "10000", "<6.0.0-rc1");
			this.Add($"script.max_compilations_rate", "10000/1m", ">=6.0.0-rc1");

			this.Add($"script.inline", "true", "<5.5.0");
			this.Add($".script.stored", "true", ">5.0.0-alpha1 <5.5.0");
			this.Add($".script.indexed", "true", "<5.0.0-alpha1");
			this.Add($".script.allowed_types", "inline,stored", ">=5.5.0");

			this.AdditionalInstallationTasks.Add(new WriteAnalysisFiles());
		}

		private static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;
		public string BinarySuffix => IsMono || Path.PathSeparator == '/' ? "" : ".bat";

		public string AnalysisFolder => Path.Combine(this.FileSystem.ConfigPath, "analysis");

	}

	public class ClientTestClusterBase : XunitClusterBase<ClientTestClusterConfiguration>
	{
		public ClientTestClusterBase(ClientTestClusterConfiguration configuration) : base(configuration) { }
		public ClientTestClusterBase() : base(new ClientTestClusterConfiguration()) { }
	}

	public class BenchmarkCluster : ClientTestClusterBase { }

}
