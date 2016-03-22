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

namespace Tests.Indices.Monitoring.IndicesShardStores
{
	[Collection(IntegrationContext.OwnIndex)]
	public class IndicesShardStoresApiTests : ApiIntegrationTestBase<IIndicesShardStoresResponse, IIndicesShardStoresRequest, IndicesShardStoresDescriptor, IndicesShardStoresRequest>
	{
		public IndicesShardStoresApiTests(OwnIndexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static string IndexWithUnassignedShards = "nest-" + RandomString();

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientMethod, string> values)
		{
			client.CreateIndex(IndexWithUnassignedShards, s => s
				.Settings(settings => settings
					.NumberOfShards(1)
					.NumberOfReplicas(2)
				)
			);
			client.Index(new IndexRequest<object>(IndexWithUnassignedShards)
			{
				Document = new { x = 1 },
				Refresh = true
			});
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.IndicesShardStores(f),
			fluentAsync: (client, f) => client.IndicesShardStoresAsync(f),
			request: (client, r) => client.IndicesShardStores(r),
			requestAsync: (client, r) => client.IndicesShardStoresAsync(r)
		);
		protected override IndicesShardStoresRequest Initializer =>
			new IndicesShardStoresRequest(IndexWithUnassignedShards)
			{
				Status = new[] { "all" }
			};
		protected override Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> Fluent => s =>
			s.Index(IndexWithUnassignedShards)
			.Status("all");

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/{IndexWithUnassignedShards}/_shard_stores?status=all";

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
			shardStore.Version.Should().BeGreaterThan(0);
			shardStore.Allocation.Should().Be(ShardStoreAllocation.Primary);
		});
	}
}
