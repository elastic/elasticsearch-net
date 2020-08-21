// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;

namespace Tests.ClientConcepts.Certificates
{
	public class SslAndKpiClusterConfiguration : XPackClusterConfiguration
	{
		public SslAndKpiClusterConfiguration() => SkipBuiltInAfterStartTasks = true;
	}

	[IntegrationOnly]
	public abstract class SslAndKpiXPackCluster : XPackCluster
	{
		public SslAndKpiXPackCluster() : this(new SslAndKpiClusterConfiguration()) { }

		public SslAndKpiXPackCluster(SslAndKpiClusterConfiguration configuration) : base(configuration) { }

		protected override void SeedNode() { }
	}
}
