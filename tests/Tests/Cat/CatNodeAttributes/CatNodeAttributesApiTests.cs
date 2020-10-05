// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatNodeAttributes
{
	public class CatNodeAttributesApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatNodeAttributesRecord>, ICatNodeAttributesRequest, CatNodeAttributesDescriptor,
			CatNodeAttributesRequest>
	{
		public CatNodeAttributesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/nodeattrs";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.NodeAttributes(),
			(client, f) => client.Cat.NodeAttributesAsync(),
			(client, r) => client.Cat.NodeAttributes(r),
			(client, r) => client.Cat.NodeAttributesAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatNodeAttributesRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Attribute == "testingcluster");
	}
}
