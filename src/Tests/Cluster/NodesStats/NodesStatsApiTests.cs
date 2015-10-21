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

namespace Tests.Cluster.NodesStats
{
	[Collection(IntegrationContext.ReadOnly)]
	public class NodesStatsApiTests : ApiIntegrationTestBase<INodesStatsResponse, INodesStatsRequest, NodesStatsDescriptor, NodesStatsRequest>
	{
		public NodesStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.NodesStats(),
			fluentAsync: (client, f) => client.NodesStatsAsync(),
			request: (client, r) => client.NodesStats(r),
			requestAsync: (client, r) => client.NodesStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_nodes/stats";

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.ClusterName.Should().NotBeNullOrWhiteSpace();
			r.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = r.Nodes.First();
			node.Key.Should().NotBeNullOrWhiteSpace();
		});

		[I] public async Task NodeResponse() => await this.AssertOnAllResponses(r =>
		{
			var node = r.Nodes.First().Value;
			node.Name.Should().NotBeNullOrWhiteSpace();
			node.Timestamp.Should().BeGreaterThan(0);
			node.TransportAddress.Should().NotBeNullOrWhiteSpace();
			node.Host.Should().NotBeNullOrWhiteSpace();
			node.Ip.Should().NotBeEmpty();
		});

		[I] public async Task NodeIndicesResponse() => await this.AssertOnAllResponses(r =>
		{
			var i = r.Nodes.First().Value.Indices;
			i.Should().NotBeNull();

			i.Documents.Should().NotBeNull();
			i.Documents.Count.Should().BeGreaterThan(0);

			i.Store.Should().NotBeNull();
			i.Store.SizeInBytes.Should().BeGreaterThan(0);

			i.Completion.Should().NotBeNull();
			i.Fielddata.Should().NotBeNull();

			i.Flush.Should().NotBeNull();
			i.Flush.Total.Should().BeGreaterThan(0);
			i.Flush.TotalTimeInMilliseconds.Should().BeGreaterThan(0);

			i.Get.Should().NotBeNull();
			i.Indexing.Should().NotBeNull();
			i.Merges.Should().NotBeNull();
			i.Percolate.Should().NotBeNull();
			i.QueryCache.Should().NotBeNull();
			i.Recovery.Should().NotBeNull();

			i.Segments.Should().NotBeNull();
			i.Segments.Count.Should().BeGreaterThan(0);
			i.Segments.DocValuesMemoryInBytes.Should().BeGreaterThan(0);
			i.Segments.IndexWriterMaxMemoryInBytes.Should().BeGreaterThan(0);
			i.Segments.MemoryInBytes.Should().BeGreaterThan(0);
			i.Segments.NormsMemoryInBytes.Should().BeGreaterThan(0);
			i.Segments.StoredFieldsMemoryInBytes.Should().BeGreaterThan(0);
			i.Segments.TermsMemoryInBytes.Should().BeGreaterThan(0);

			i.Store.Should().NotBeNull();
			i.Store.SizeInBytes.Should().BeGreaterThan(0);

			i.Suggest.Should().NotBeNull();
			i.Translog.Should().NotBeNull();
			i.Warmer.Should().NotBeNull();
		});

		[I] public async Task NodeOsResponse() => await this.AssertOnAllResponses(r =>
		{
			var os = r.Nodes.First().Value.OperatingSystem;
			os.Should().NotBeNull();

			os.Timestamp.Should().BeGreaterThan(0);
			os.LoadAverage.Should().NotBe(0);

			os.Memory.Should().NotBeNull();
			os.Memory.TotalInBytes.Should().BeGreaterThan(0);
			os.Memory.FreeInBytes.Should().BeGreaterThan(0);
			os.Memory.UsedInBytes.Should().BeGreaterThan(0);
			os.Memory.FreePercent.Should().BeGreaterThan(0);
			os.Memory.UsedPercent.Should().BeGreaterThan(0);

			os.Swap.Should().NotBeNull();
			os.Swap.TotalInBytes.Should().BeGreaterThan(0);
			os.Swap.FreeInBytes.Should().BeGreaterThan(0);
			os.Swap.UsedInBytes.Should().BeGreaterThan(0);
		});

		[I] public async Task NodeProcessResponse() => await this.AssertOnAllResponses(r =>
		{
			var p = r.Nodes.First().Value.Process;
			p.Should().NotBeNull();

			p.Timestamp.Should().BeGreaterThan(0);
			p.OpenFileDescriptors.Should().NotBe(0);

			p.CPU.Should().NotBeNull();
			p.CPU.TotalInMilliseconds.Should().BeGreaterThan(0);
			p.Memory.Should().NotBeNull();
			p.Memory.TotalVirtualInBytes.Should().BeGreaterThan(0);
		});

		[I] public async Task NodeScriptResponse() => await this.AssertOnAllResponses(r =>
		{
			var script = r.Nodes.First().Value.Script;
			script.Should().NotBeNull();
		});

		[I] public async Task NodeTransportResponse() => await this.AssertOnAllResponses(r =>
		{
			var transport = r.Nodes.First().Value.Transport;
			transport.Should().NotBeNull();
			transport.RXCount.Should().BeGreaterThan(0);
			transport.RXSizeInBytes.Should().BeGreaterThan(0);
			transport.TXCount.Should().BeGreaterThan(0);
			transport.TXSizeInBytes.Should().BeGreaterThan(0);
		});

		[I] public async Task NodeHttpResponse() => await this.AssertOnAllResponses(r =>
		{
			var http = r.Nodes.First().Value.Http;
			http.Should().NotBeNull();
			http.TotalOpened.Should().BeGreaterThan(0);
		});

		[I] public async Task NodeBreakersResponse() => await this.AssertOnAllResponses(r =>
		{
			var breakers = r.Nodes.First().Value.Breakers;
			breakers.Should().NotBeEmpty().And.ContainKey("request");

			var requestBreaker = breakers["request"];
			requestBreaker.LimitSizeInBytes.Should().BeGreaterThan(0);
			requestBreaker.Overhead.Should().BeGreaterThan(0);
		});

		[I] public async Task NodeFileSystemResponse() => await this.AssertOnAllResponses(r =>
		{
			var fileSystem = r.Nodes.First().Value.FileSystem;
			fileSystem.Should().NotBeNull();
			fileSystem.Timestamp.Should().BeGreaterThan(0);
			fileSystem.Total.Should().NotBeNull();
			fileSystem.Total.AvailableInBytes.Should().BeGreaterThan(0);
			fileSystem.Total.FreeInBytes.Should().BeGreaterThan(0);
			fileSystem.Total.TotalInBytes.Should().BeGreaterThan(0);

			fileSystem.Data.Should().NotBeEmpty();
			var path = fileSystem.Data.First();
			path.AvailableInBytes.Should().BeGreaterThan(0);
			path.FreeInBytes.Should().BeGreaterThan(0);
			path.TotalInBytes.Should().BeGreaterThan(0);
			path.Mount.Should().NotBeNullOrWhiteSpace();
			path.Path.Should().NotBeNullOrWhiteSpace();
			path.Type.Should().NotBeNullOrWhiteSpace();
		});

		[I] public async Task NodeThreadPoolResponse() => await this.AssertOnAllResponses(r =>
		{
			var threadPools = r.Nodes.First().Value.ThreadPool;
			threadPools.Should().NotBeEmpty().And.ContainKey("management");
			var threadPool = threadPools["management"];
			threadPool.Completed.Should().BeGreaterThan(0);
		});

		[I] public async Task NodeJvmResponse() => await this.AssertOnAllResponses(r =>
		{
			var jvm = r.Nodes.First().Value.Jvm;
			jvm.Should().NotBeNull();

			jvm.Timestamp.Should().BeGreaterThan(0);
			jvm.UptimeInMilliseconds.Should().BeGreaterThan(0);

			jvm.BufferPools.Should().NotBeEmpty().And.ContainKey("direct");
			var bufferPool = jvm.BufferPools["direct"];
			bufferPool.Count.Should().BeGreaterThan(0);
			bufferPool.TotalCapacityInBytes.Should().BeGreaterThan(0);
			bufferPool.UsedInBytes.Should().BeGreaterThan(0);

			jvm.Classes.Should().NotBeNull();
			jvm.Classes.CurrentLoadedCount.Should().BeGreaterThan(0);
			jvm.Classes.TotalLoadedCount.Should().BeGreaterThan(0);
			jvm.Classes.TotalUnloadedCount.Should().BeGreaterOrEqualTo(0);

			jvm.GarbageCollection.Should().NotBeNull();
			jvm.GarbageCollection.Collectors.Should().NotBeEmpty().And.ContainKey("young");
			var youngGc=  jvm.GarbageCollection.Collectors["young"];
			youngGc.CollectionCount.Should().BeGreaterThan(0);
			youngGc.CollectionTimeInMilliseconds.Should().BeGreaterThan(0);

			jvm.Memory.Should().NotBeNull();
			jvm.Memory.HeapCommittedInBytes.Should().BeGreaterThan(0);
			jvm.Memory.HeapMaxInBytes.Should().BeGreaterThan(0);
			jvm.Memory.HeapUsedInBytes.Should().BeGreaterThan(0);
			jvm.Memory.HeapUsedPercent.Should().BeGreaterThan(0);
			jvm.Memory.NonHeapCommittedInBytes.Should().BeGreaterThan(0);
			jvm.Memory.NonHeapUsedInBytes.Should().BeGreaterThan(0);

			jvm.Memory.Pools.Should().NotBeEmpty().And.ContainKey("young");
			var youngMemoryPool = jvm.Memory.Pools["young"];
			youngMemoryPool.MaxInBytes.Should().BeGreaterThan(0);
			youngMemoryPool.PeakMaxInBytes.Should().BeGreaterThan(0);
			youngMemoryPool.PeakUsedInBytes.Should().BeGreaterThan(0);

			jvm.Threads.Should().NotBeNull();
			jvm.Threads.Count.Should().BeGreaterThan(0);
			jvm.Threads.PeakCount.Should().BeGreaterThan(0);

		});

	}

}
