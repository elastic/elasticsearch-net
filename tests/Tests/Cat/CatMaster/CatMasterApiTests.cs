// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatMaster
{
	public class CatMasterApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatMasterRecord>, ICatMasterRequest, CatMasterDescriptor, CatMasterRequest>
	{
		public CatMasterApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/master";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Master(),
			(client, f) => client.Cat.MasterAsync(),
			(client, r) => client.Cat.Master(r),
			(client, r) => client.Cat.MasterAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatMasterRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Node));
	}
}
