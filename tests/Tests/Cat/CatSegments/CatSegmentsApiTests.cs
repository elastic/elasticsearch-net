// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatSegments
{
	public class CatSegmentsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatSegmentsRecord>, ICatSegmentsRequest, CatSegmentsDescriptor, CatSegmentsRequest>
	{
		public CatSegmentsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/segments";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Segments(),
			(client, f) => client.Cat.SegmentsAsync(),
			(client, r) => client.Cat.Segments(r),
			(client, r) => client.Cat.SegmentsAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatSegmentsRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Index));
	}
}
