using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Indices.Monitoring.IndicesStats
{
	public class IndicesStatsWithShardsInformationApiTests : ApiIntegrationTestBase<WritableCluster, IIndicesStatsResponse,
		IIndicesStatsRequest, IndicesStatsDescriptor, IndicesStatsRequest>
	{
		public IndicesStatsWithShardsInformationApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var createShardedIndex = this.Client.CreateIndex(RandomString(), c => c
				.Settings(settings => settings
					.NumberOfShards(3)
				)
			);
			createShardedIndex.ShouldBeValid();
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.IndicesStats(Infer.AllIndices, f),
			fluentAsync: (client, f) => client.IndicesStatsAsync(Infer.AllIndices, f),
			request: (client, r) => client.IndicesStats(r),
			requestAsync: (client, r) => client.IndicesStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_stats?level=shards";

		protected override Func<IndicesStatsDescriptor, IIndicesStatsRequest> Fluent => d => d.Level(Level.Shards);

		protected override IndicesStatsRequest Initializer => new IndicesStatsRequest(Infer.AllIndices)
		{
			Level = Level.Shards
		};

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
