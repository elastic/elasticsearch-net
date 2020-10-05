// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatAliases
{
	public class CatAliasesApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatAliasesRecord>, ICatAliasesRequest, CatAliasesDescriptor, CatAliasesRequest>
	{
		public CatAliasesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/aliases";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Aliases(),
			(client, f) => client.Cat.AliasesAsync(),
			(client, r) => client.Cat.Aliases(r),
			(client, r) => client.Cat.AliasesAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatAliasesRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => a.Alias == DefaultSeeder.ProjectsAliasName);
	}
}
