// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatHelp
{
	public class CatHelpApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatHelpRecord>, ICatHelpRequest, CatHelpDescriptor, CatHelpRequest>
	{
		public CatHelpApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Help(),
			(client, f) => client.Cat.HelpAsync(),
			(client, r) => client.Cat.Help(r),
			(client, r) => client.Cat.HelpAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatHelpRecord> response) => response.Records.Should()
			.NotBeEmpty()
			.And.Contain(a => a.Endpoint == "/_cat/shards/{index}")
			.And.NotContain(a => a.Endpoint == "=^.^=");
	}
}
