// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.Monitoring.IndicesStats
{
	public class IndicesStatsWithShardsInformationApiTests
		: ApiIntegrationTestBase<WritableCluster, IndicesStatsResponse,
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

		protected override string UrlPath => "/_all/_stats?level=shards";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var createShardedIndex = Client.Indices.Create(RandomString(), c => c
				.Settings(settings => settings
					.NumberOfShards(3)
				)
			);
			createShardedIndex.ShouldBeValid();
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Stats(Infer.AllIndices, f),
			(client, f) => client.Indices.StatsAsync(Infer.AllIndices, f),
			(client, r) => client.Indices.Stats(r),
			(client, r) => client.Indices.StatsAsync(r)
		);

		protected override void ExpectResponse(IndicesStatsResponse response)
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
			first.Fielddata.Should().NotBeNull();
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
