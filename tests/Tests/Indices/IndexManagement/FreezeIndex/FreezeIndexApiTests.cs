// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexManagement.FreezeIndex
{
	public class FreezeIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<WritableCluster, FreezeIndexResponse, IFreezeIndexRequest, FreezeIndexDescriptor, FreezeIndexRequest>
	{
		public FreezeIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override FreezeIndexRequest Initializer => new FreezeIndexRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}/_freeze";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Freeze(CallIsolatedValue),
			(client, f) => client.Indices.FreezeAsync(CallIsolatedValue),
			(client, r) => client.Indices.Freeze(r),
			(client, r) => client.Indices.FreezeAsync(r)
		);

		protected override void ExpectResponse(FreezeIndexResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
