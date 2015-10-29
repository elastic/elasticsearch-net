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

namespace Tests.Cluster.NodesInfo
{
	[Collection(IntegrationContext.ReadOnly)]
	public class NodesInfoApiTests : ApiIntegrationTestBase<INodesInfoResponse, INodesInfoRequest, NodesInfoDescriptor, NodesInfoRequest>
	{
		public NodesInfoApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.NodesInfo(),
			fluentAsync: (client, f) => client.NodesInfoAsync(),
			request: (client, r) => client.NodesInfo(r),
			requestAsync: (client, r) => client.NodesInfoAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_nodes";

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
			node.TransportAddress.Should().NotBeNullOrWhiteSpace();
			node.Hostname.Should().NotBeNullOrWhiteSpace();
			node.Ip.Should().NotBeNullOrWhiteSpace();
			node.Version.Should().NotBeNullOrWhiteSpace();
			node.Build.Should().NotBeNullOrWhiteSpace();
			node.HttpAddress.Should().NotBeNullOrWhiteSpace();
		});

		[I] public async Task NodeOperatingSystemResponse() => await this.AssertOnAllResponses(r =>
		{
			var os = r.Nodes.First().Value.OperatingSystem;
			os.Should().NotBeNull();
			os.RefreshInterval.Should().Be(1000);
			os.AvailableProcessors.Should().BeGreaterThan(0);
			os.Name.Should().NotBeNullOrWhiteSpace();
			os.Architecture.Should().NotBeNullOrWhiteSpace();
			os.Version.Should().NotBeNullOrWhiteSpace();
		});

		[I] public async Task NodePluginsResponse() => await this.AssertOnAllResponses(r =>
		{
			var plugins = r.Nodes.First().Value.Plugins;
			plugins.Should().NotBeEmpty();

			var plugin = plugins.First();
			plugin.Name.Should().NotBeNullOrWhiteSpace();
			plugin.Description.Should().NotBeNullOrWhiteSpace();
			plugin.Version.Should().NotBeNullOrWhiteSpace();
			plugin.ClassName.Should().NotBeNullOrWhiteSpace();
		});

		[I] public async Task NodeProcessResponse() => await this.AssertOnAllResponses(r =>
		{
			var process = r.Nodes.First().Value.Process;
			process.Id.Should().BeGreaterThan(0);
			process.RefreshIntervalInMilliseconds.Should().BeGreaterThan(0);
		});

		[I] public async Task NodeJvmResponse() => await this.AssertOnAllResponses(r =>
		{
			var jvm = r.Nodes.First().Value.Jvm;
			jvm.Should().NotBeNull();
			jvm.PID.Should().BeGreaterThan(0);
			jvm.StartTime.Should().BeGreaterThan(0);
			jvm.Version.Should().NotBeNullOrWhiteSpace();
			jvm.VMName.Should().NotBeNullOrWhiteSpace();
			jvm.VMVendor.Should().NotBeNullOrWhiteSpace();
			jvm.VMVersion.Should().NotBeNullOrWhiteSpace();
			jvm.GCCollectors.Should().NotBeEmpty();
			jvm.MemoryPools.Should().NotBeEmpty();
			jvm.Memory.Should().NotBeNull();
			jvm.Memory.DirectMaxInBytes.Should().BeGreaterThan(0);
			jvm.Memory.NonHeapMaxInBytes.Should().BeGreaterOrEqualTo(0);
			jvm.Memory.NonHeapInitInBytes.Should().BeGreaterThan(0);
			jvm.Memory.HeapMaxInBytes.Should().BeGreaterThan(0);
			jvm.Memory.HeapInitInBytes.Should().BeGreaterThan(0);
		});

		[I] public async Task NodeThreadPoolResponse() => await this.AssertOnAllResponses(r =>
		{
			var pools = r.Nodes.First().Value.ThreadPool;
			pools.Should().NotBeEmpty().And.ContainKey("fetch_shard_store");

			var pool = pools["fetch_shard_store"];
			pool.KeepAlive.Should().NotBeNullOrWhiteSpace();
			pool.Type.Should().Be("scaling");
			pool.Min.Should().BeGreaterThan(0);
			pool.Max.Should().BeGreaterThan(0);
			pool.QueueSize.Should().BeGreaterOrEqualTo(-1);
		});

		[I] public async Task NodeTransportResponse() => await this.AssertOnAllResponses(r =>
		{
			var transport = r.Nodes.First().Value.Transport;
			transport.Should().NotBeNull();

			transport.BoundAddress.Should().NotBeEmpty();
			transport.PublishAddress.Should().NotBeNullOrWhiteSpace();
		});

		[I] public async Task NodeHttpResponse() => await this.AssertOnAllResponses(r =>
		{
			var http = r.Nodes.First().Value.Http;
			http.Should().NotBeNull();

			http.BoundAddress.Should().NotBeEmpty();
			http.PublishAddress.Should().NotBeNullOrWhiteSpace();
		});
	}

}
