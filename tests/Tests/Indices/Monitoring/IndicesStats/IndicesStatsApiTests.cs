// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.Monitoring.IndicesStats
{
	public class IndicesStatsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IndicesStatsResponse, IIndicesStatsRequest, IndicesStatsDescriptor, IndicesStatsRequest>
	{
		public IndicesStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<IndicesStatsDescriptor, IIndicesStatsRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override IndicesStatsRequest Initializer => new IndicesStatsRequest(Infer.AllIndices);
		protected override string UrlPath => "/_all/_stats";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Stats(Infer.AllIndices, f),
			(client, f) => client.Indices.StatsAsync(Infer.AllIndices, f),
			(client, r) => client.Indices.Stats(r),
			(client, r) => client.Indices.StatsAsync(r)
		);
	}
}
