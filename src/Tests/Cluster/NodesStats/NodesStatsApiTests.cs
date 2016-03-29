using System.Collections.Generic;
using System.Linq;
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

		protected override void ExpectResponse(INodesStatsResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var kv = response.Nodes.First();
			kv.Key.Should().NotBeNullOrWhiteSpace();
			var node = kv.Value;
			Assert(node);
			Assert(node.Indices);
			Assert(node.OperatingSystem);
			Assert(node.Process);
			Assert(node.Transport);
			Assert(node.Script);
			Assert(node.Http);
			Assert(node.Breakers);
			Assert(node.FileSystem);
			Assert(node.ThreadPool);
			Assert(node.Jvm);
		}

		protected void Assert(NodeStats node)
		{
			node.Name.Should().NotBeNullOrWhiteSpace();
			node.Timestamp.Should().BeGreaterThan(0);
			node.TransportAddress.Should().NotBeNullOrWhiteSpace();
			node.Host.Should().NotBeNullOrWhiteSpace();
			node.Ip.Should().NotBeEmpty();
		}

		protected void Assert(IndexStats index)
		{
			index.Should().NotBeNull();

			index.Documents.Should().NotBeNull();
			index.Documents.Count.Should().BeGreaterThan(0);

			index.Store.Should().NotBeNull();
			index.Store.SizeInBytes.Should().BeGreaterThan(0);

			index.Completion.Should().NotBeNull();
			index.Fielddata.Should().NotBeNull();

			index.Flush.Should().NotBeNull();

			index.Get.Should().NotBeNull();
			index.Indexing.Should().NotBeNull();
			index.Merges.Should().NotBeNull();
			index.Percolate.Should().NotBeNull();
			index.QueryCache.Should().NotBeNull();
			index.Recovery.Should().NotBeNull();

			index.Segments.Should().NotBeNull();
			index.Segments.Count.Should().BeGreaterThan(0);
			index.Segments.DocValuesMemoryInBytes.Should().BeGreaterThan(0);
			index.Segments.IndexWriterMaxMemoryInBytes.Should().BeGreaterThan(0);
			index.Segments.MemoryInBytes.Should().BeGreaterThan(0);
			index.Segments.NormsMemoryInBytes.Should().BeGreaterThan(0);
			index.Segments.StoredFieldsMemoryInBytes.Should().BeGreaterThan(0);
			index.Segments.TermsMemoryInBytes.Should().BeGreaterThan(0);

			index.Store.Should().NotBeNull();
			index.Store.SizeInBytes.Should().BeGreaterThan(0);

			index.Suggest.Should().NotBeNull();
			index.Translog.Should().NotBeNull();
			index.Warmer.Should().NotBeNull();
		}

		protected void Assert(OperatingSystemStats os)
		{
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
		}

		protected void Assert(ProcessStats process)	
		{
			process.Should().NotBeNull();

			process.Timestamp.Should().BeGreaterThan(0);
			process.OpenFileDescriptors.Should().NotBe(0);

			process.CPU.Should().NotBeNull();
			process.CPU.TotalInMilliseconds.Should().BeGreaterThan(0);
			process.Memory.Should().NotBeNull();
			process.Memory.TotalVirtualInBytes.Should().BeGreaterThan(0);
		}

		protected void Assert(ScriptStats script)	
		{
			script.Should().NotBeNull();
		}

		protected void Assert(TransportStats transport)
		{
			transport.Should().NotBeNull();
			transport.RXCount.Should().BeGreaterThan(0);
			transport.RXSizeInBytes.Should().BeGreaterThan(0);
			transport.TXCount.Should().BeGreaterThan(0);
			transport.TXSizeInBytes.Should().BeGreaterThan(0);
		}

		protected void Assert(HttpStats http)
		{
			http.Should().NotBeNull();
			http.TotalOpened.Should().BeGreaterThan(0);
		}

		protected void Assert(Dictionary<string, BreakerStats> breakers)
		{
			breakers.Should().NotBeEmpty().And.ContainKey("request");
			var requestBreaker = breakers["request"];
			requestBreaker.LimitSizeInBytes.Should().BeGreaterThan(0);
			requestBreaker.Overhead.Should().BeGreaterThan(0);
		}

		protected void Assert(FileSystemStats fileSystem)
		{
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
		}

		protected void Assert(Dictionary<string, ThreadCountStats> threadPools)
		{
			threadPools.Should().NotBeEmpty().And.ContainKey("management");
			var threadPool = threadPools["management"];
			threadPool.Completed.Should().BeGreaterThan(0);
		}

		protected void Assert(NodeJvmStats jvm)
		{	
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
		}
	}
}
