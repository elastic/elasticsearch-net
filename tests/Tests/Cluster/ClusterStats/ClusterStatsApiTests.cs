// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Elastic.Clients.Elasticsearch.Cluster;
using Elastic.Clients.Elasticsearch.Cluster.Stats;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.ClusterStats
{
	public class ClusterStatsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClusterStatsResponse, ClusterStatsRequestDescriptor, ClusterStatsRequest>
	{
		public ClusterStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string ExpectedUrlPathAndQuery => "/_cluster/stats";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.Stats(),
			(client, f) => client.Cluster.StatsAsync(),
			(client, r) => client.Cluster.Stats(r),
			(client, r) => client.Cluster.StatsAsync(r)
		);

		// TODO: Fix missing properties
		protected override void ExpectResponse(ClusterStatsResponse response)
		{
			// TODO: Use primitive types on responses
			response.ClusterName.ToString().Should().NotBeNullOrWhiteSpace();

			//response.ClusterUUID.Should().NotBeNullOrWhiteSpace();
			//response.NodeStatistics.Should().NotBeNull();
			response.Status.Should().NotBe(HealthStatus.Red);
			//response.Timestamp.Should().BeGreaterThan(0);
			Assert(response.Nodes);
			Assert(response.Indices);
		}

		protected void Assert(ClusterNodes nodes)
		{
			nodes.Should().NotBeNull();
			nodes.Count.Should().NotBeNull();
			nodes.Count.Master.Should().BeGreaterOrEqualTo(1);

			//nodes.FileSystem.Should().NotBeNull();
			//nodes.FileSystem.AvailableInBytes.Should().BeGreaterThan(0);
			//nodes.FileSystem.FreeInBytes.Should().BeGreaterThan(0);
			//nodes.FileSystem.TotalInBytes.Should().BeGreaterThan(0);

			nodes.Jvm.Should().NotBeNull();
			//nodes.Jvm.MaxUptimeInMilliseconds.Should().BeGreaterThan(0);
			//nodes.Jvm.Threads.Should().BeGreaterThan(0);
			//nodes.Jvm.Memory.Should().NotBeNull();
			//nodes.Jvm.Memory.HeapMaxInBytes.Should().BeGreaterThan(0);
			//nodes.Jvm.Memory.HeapUsedInBytes.Should().BeGreaterThan(0);

			nodes.Jvm.Versions.Should().NotBeEmpty();
			var version = nodes.Jvm.Versions.First();
			version.Count.Should().BeGreaterThan(0);
			version.Version.Should().NotBeNullOrWhiteSpace();
			version.VmName.Should().NotBeNullOrWhiteSpace();
			version.VmVendor.Should().NotBeNullOrWhiteSpace();
			version.VmVersion.Should().NotBeNullOrWhiteSpace();

			//nodes.OperatingSystem.Should().NotBeNull();
			//nodes.OperatingSystem.AvailableProcessors.Should().BeGreaterThan(0);
			//nodes.OperatingSystem.AllocatedProcessors.Should().BeGreaterThan(0);

			if (Cluster.ClusterConfiguration.Version.InRange(">=7.12.0"))
			{
				//nodes.OperatingSystem.Architectures.Should().NotBeNull();
				//nodes.OperatingSystem.Architectures.Count.Should().BeGreaterThan(0);
				//nodes.OperatingSystem.Architectures.First().Architecture.Should().NotBeNullOrEmpty();
				//nodes.OperatingSystem.Architectures.First().Count.Should().BeGreaterThan(0);
			}

			//nodes.OperatingSystem.Names.Should().NotBeEmpty();

			//nodes.OperatingSystem.Memory.Should().NotBeNull();
			//nodes.OperatingSystem.PrettyNames.Should().NotBeNull();
			
			var plugins = nodes.Plugins;
			plugins.Should().NotBeEmpty();

			var plugin = plugins.First();
			//plugin.Name.Should().NotBeNullOrWhiteSpace();
			plugin.Description.Should().NotBeNullOrWhiteSpace();
			plugin.Version.Should().NotBeNullOrWhiteSpace();
			//plugin.ClassName.Should().NotBeNullOrWhiteSpace();

			nodes.Process.Should().NotBeNull();
			nodes.Process.Cpu.Should().NotBeNull();
			nodes.Process.OpenFileDescriptors.Should().NotBeNull();
			nodes.Process.OpenFileDescriptors.Max.Should().NotBe(0);
			nodes.Process.OpenFileDescriptors.Min.Should().NotBe(0);

			nodes.Versions.Should().NotBeEmpty();

			if (Cluster.ClusterConfiguration.Version >= "7.6.0")
				nodes.Ingest.Should().NotBeNull();
		}

		protected void Assert(ClusterIndices indices)
		{
			indices.Should().NotBeNull();
			//indices.Count.Should().BeGreaterThan(0);

			indices.Docs.Should().NotBeNull();
			//indices.Docs.Count.Should().BeGreaterThan(0);

			indices.Completion.Should().NotBeNull();
			indices.Fielddata.Should().NotBeNull();
			indices.QueryCache.Should().NotBeNull();

			indices.Segments.Should().NotBeNull();
			indices.Segments.Count.Should().BeGreaterThan(0);
			indices.Segments.DocValuesMemoryInBytes.Should().BeGreaterThan(0);
			indices.Segments.MemoryInBytes.Should().BeGreaterThan(0);
			indices.Segments.NormsMemoryInBytes.Should().BeGreaterThan(0);
			indices.Segments.StoredFieldsMemoryInBytes.Should().BeGreaterThan(0);
			indices.Segments.TermsMemoryInBytes.Should().BeGreaterThan(0);

			indices.Shards.Should().NotBeNull();
			indices.Shards.Primaries.Should().BeGreaterThan(0);
			indices.Shards.Total.Should().BeGreaterThan(0);
			indices.Shards.Index.Primaries.Should().NotBeNull();
			indices.Shards.Index.Primaries.Avg.Should().BeGreaterThan(0);
			indices.Shards.Index.Primaries.Min.Should().BeGreaterThan(0);
			indices.Shards.Index.Primaries.Max.Should().BeGreaterThan(0);
			indices.Shards.Index.Replication.Should().NotBeNull();
			indices.Shards.Index.Shards.Should().NotBeNull();
			indices.Shards.Index.Shards.Avg.Should().BeGreaterThan(0);
			indices.Shards.Index.Shards.Min.Should().BeGreaterThan(0);
			indices.Shards.Index.Shards.Max.Should().BeGreaterThan(0);

			indices.Store.Should().NotBeNull();
			indices.Store.SizeInBytes.Should().BeGreaterThan(0);
		}
	}
}
