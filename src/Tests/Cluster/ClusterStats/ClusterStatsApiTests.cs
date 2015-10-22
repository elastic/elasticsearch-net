using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cluster.ClusterStats
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClusterStatsApiTests : ApiIntegrationTestBase<IClusterStatsResponse, IClusterStatsRequest, ClusterStatsDescriptor, ClusterStatsRequest>
	{
		public ClusterStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterStats(),
			fluentAsync: (client, f) => client.ClusterStatsAsync(),
			request: (client, r) => client.ClusterStats(r),
			requestAsync: (client, r) => client.ClusterStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/stats";

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.ClusterName.Should().NotBeNullOrWhiteSpace();
			r.Status.Should().NotBe(ClusterStatus.Red);
			r.Timestamp.Should().BeGreaterThan(0);
		});

		[I] public async Task NodesResponse() => await this.AssertOnAllResponses(r =>
		{
			var nodes = r.Nodes;
			nodes.Should().NotBeNull();
			nodes.Count.Should().NotBeNull();
			nodes.Count.MasterData.Should().BeGreaterOrEqualTo(1);

			nodes.FileSystem.Should().NotBeNull();
			nodes.FileSystem.AvailableInBytes.Should().BeGreaterThan(0);
			nodes.FileSystem.FreeInBytes.Should().BeGreaterThan(0);
			nodes.FileSystem.TotalInBytes.Should().BeGreaterThan(0);

			nodes.Jvm.Should().NotBeNull();
			nodes.Jvm.MaxUptimeInMilliseconds.Should().BeGreaterThan(0);
			nodes.Jvm.Threads.Should().BeGreaterThan(0);
			nodes.Jvm.Memory.Should().NotBeNull();
			nodes.Jvm.Memory.HeapMaxInBytes.Should().BeGreaterThan(0);
			nodes.Jvm.Memory.HeapUsedInBytes.Should().BeGreaterThan(0);

			nodes.Jvm.Versions.Should().NotBeEmpty();
			var version = nodes.Jvm.Versions.First();
			version.Count.Should().BeGreaterThan(0);
			version.Version.Should().NotBeNullOrWhiteSpace();
			version.VmName.Should().NotBeNullOrWhiteSpace();
			version.VmVendor.Should().NotBeNullOrWhiteSpace();
			version.VmVersion.Should().NotBeNullOrWhiteSpace();

			nodes.OperatingSystem.Should().NotBeNull();
			nodes.OperatingSystem.AvailableProcessors.Should().BeGreaterThan(0);
			nodes.OperatingSystem.Memory.Should().NotBeNull();

			nodes.OperatingSystem.Names.Should().NotBeEmpty();

			var plugins = nodes.Plugins;
			plugins.Should().NotBeEmpty();

			var plugin = plugins.First();
			plugin.Name.Should().NotBeNullOrWhiteSpace();
			plugin.Description.Should().NotBeNullOrWhiteSpace();
			plugin.Version.Should().NotBeNullOrWhiteSpace();
			plugin.ClassName.Should().NotBeNullOrWhiteSpace();

			nodes.Process.Should().NotBeNull();
			nodes.Process.Cpu.Should().NotBeNull();
			nodes.Process.OpenFileDescriptors.Should().NotBeNull();
			nodes.Process.OpenFileDescriptors.Max.Should().NotBe(0);
			nodes.Process.OpenFileDescriptors.Min.Should().NotBe(0);

			nodes.Versions.Should().NotBeEmpty();

		});

		[I] public async Task IndicesResponse() => await this.AssertOnAllResponses(r =>
		{
			var i = r.Indices;

			i.Should().NotBeNull();
			i.Count.Should().BeGreaterThan(0);

			i.Documents.Should().NotBeNull();
			i.Documents.Count.Should().BeGreaterThan(0);

			i.Completion.Should().NotBeNull();
			i.Fielddata.Should().NotBeNull();
			i.Percolate.Should().NotBeNull();
			i.QueryCache.Should().NotBeNull();

			i.Segments.Should().NotBeNull();
			i.Segments.Count.Should().BeGreaterThan(0);
			i.Segments.DocValuesMemoryInBytes.Should().BeGreaterThan(0);
			i.Segments.IndexWriterMaxMemoryInBytes.Should().BeGreaterThan(0);
			i.Segments.MemoryInBytes.Should().BeGreaterThan(0);
			i.Segments.NormsMemoryInBytes.Should().BeGreaterThan(0);
			i.Segments.StoredFieldsMemoryInBytes.Should().BeGreaterThan(0);
			i.Segments.TermsMemoryInBytes.Should().BeGreaterThan(0);

			i.Shards.Should().NotBeNull();
			i.Shards.Primaries.Should().BeGreaterThan(0);
			i.Shards.Total.Should().BeGreaterThan(0);
			i.Shards.Index.Primaries.Should().NotBeNull();
			i.Shards.Index.Primaries.Avg.Should().BeGreaterThan(0);
			i.Shards.Index.Primaries.Min.Should().BeGreaterThan(0);
			i.Shards.Index.Primaries.Max.Should().BeGreaterThan(0);
			i.Shards.Index.Replication.Should().NotBeNull();
			i.Shards.Index.Shards.Should().NotBeNull();
			i.Shards.Index.Shards.Avg.Should().BeGreaterThan(0);
			i.Shards.Index.Shards.Min.Should().BeGreaterThan(0);
			i.Shards.Index.Shards.Max.Should().BeGreaterThan(0);

			i.Store.Should().NotBeNull();
			i.Store.SizeInBytes.Should().BeGreaterThan(0);
		});

	}

}
