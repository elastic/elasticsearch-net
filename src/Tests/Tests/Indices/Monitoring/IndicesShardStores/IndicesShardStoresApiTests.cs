using System;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.Monitoring.IndicesShardStores
{
	public class IndicesShardStoresApiTests
		: ApiIntegrationTestBase<WritableCluster, IIndicesShardStoresResponse, IIndicesShardStoresRequest, IndicesShardStoresDescriptor,
			IndicesShardStoresRequest>
	{
		private static readonly string IndexWithUnassignedShards = "nest-" + RandomString();

		public IndicesShardStoresApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> Fluent => s =>
			s.Index(IndexWithUnassignedShards)
				.Status("all");

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override IndicesShardStoresRequest Initializer =>
			new IndicesShardStoresRequest(IndexWithUnassignedShards)
			{
				Status = new[] { "all" }
			};

		protected override string UrlPath => $"/{IndexWithUnassignedShards}/_shard_stores?status=all";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			client.CreateIndex(IndexWithUnassignedShards, s => s
				.Settings(settings => settings
					.NumberOfShards(1)
					.NumberOfReplicas(2)
				)
			);
			client.Index(new IndexRequest<object>((IndexName)IndexWithUnassignedShards)
			{
				Document = new { x = 1 },
				Refresh = Refresh.True
			});
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.IndicesShardStores(f),
			(client, f) => client.IndicesShardStoresAsync(f),
			(client, r) => client.IndicesShardStores(r),
			(client, r) => client.IndicesShardStoresAsync(r)
		);

		[I] public Task AssertResponse() => AssertOnAllResponses(r =>
		{
			r.Indices.Should().NotBeEmpty();
			var indicesShardStore = r.Indices[IndexWithUnassignedShards];
			indicesShardStore.Should().NotBeNull();
			indicesShardStore.Shards.Should().NotBeEmpty().And.ContainKey("0");
			var shardStoreWrapper = indicesShardStore.Shards["0"];
			shardStoreWrapper.Stores.Should().NotBeNullOrEmpty();

			var shardStore = shardStoreWrapper.Stores.First();
			shardStore.Id.Should().NotBeNullOrWhiteSpace();
			shardStore.Name.Should().NotBeNullOrWhiteSpace();
			shardStore.TransportAddress.Should().NotBeNullOrWhiteSpace();
			shardStore.LegacyVersion.Should().Be(null);
			shardStore.AllocationId.Should().NotBeNullOrWhiteSpace();
			shardStore.Allocation.Should().Be(ShardStoreAllocation.Primary);
		});
	}
}
