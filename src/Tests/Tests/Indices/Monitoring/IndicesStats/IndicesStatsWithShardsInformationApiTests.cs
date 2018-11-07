using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.Monitoring.IndicesStats
{
	public class IndicesStatsWithShardsInformationApiTests
		: ApiIntegrationTestBase<WritableCluster, IIndicesStatsResponse,
			IIndicesStatsRequest, IndicesStatsDescriptor, IndicesStatsRequest>
	{
		public IndicesStatsWithShardsInformationApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<IndicesStatsDescriptor, IIndicesStatsRequest> Fluent => d => d.Level(Level.Shards);
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override IndicesStatsRequest Initializer => new IndicesStatsRequest(Infer.AllIndices)
		{
			Level = Level.Shards
		};

		protected override string UrlPath => "/_stats?level=shards";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var createShardedIndex = Client.CreateIndex(RandomString(), c => c
				.Settings(settings => settings
					.NumberOfShards(3)
				)
			);
			createShardedIndex.ShouldBeValid();
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.IndicesStats(Infer.AllIndices, f),
			(client, f) => client.IndicesStatsAsync(Infer.AllIndices, f),
			(client, r) => client.IndicesStats(r),
			(client, r) => client.IndicesStatsAsync(r)
		);

		protected override void ExpectResponse(IIndicesStatsResponse response)
		{
			var firstIndex = response.Indices.First().Value;
			firstIndex.Shards.Should().NotBeNull();

			var firstShard = firstIndex.Shards.Values.First();
			firstShard.Length.Should().Be(1);

			var first = firstShard.First();
			first.Routing.Should().NotBeNull();
			first.Documents.Should().NotBeNull();
			first.Store.Should().NotBeNull();
			first.Indexing.Should().NotBeNull();
			first.Get.Should().NotBeNull();
			first.Search.Should().NotBeNull();
			first.Merges.Should().NotBeNull();
			first.Refresh.Should().NotBeNull();
			first.Flush.Should().NotBeNull();
			first.Warmer.Should().NotBeNull();
			first.QueryCache.Should().NotBeNull();
			first.FieldData.Should().NotBeNull();
			first.Completion.Should().NotBeNull();
			first.Segments.Should().NotBeNull();
			first.TransactionLog.Should().NotBeNull();
			first.RequestCache.Should().NotBeNull();
			first.Recovery.Should().NotBeNull();
			first.Commit.Should().NotBeNull();
			first.SequenceNumber.Should().NotBeNull();
			first.Path.Should().NotBeNull();
		}
	}
}
