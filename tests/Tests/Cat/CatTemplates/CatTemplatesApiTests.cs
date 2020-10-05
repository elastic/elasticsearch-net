// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatTemplates
{
	[SkipVersion("<5.1.0", "CatTemplates is an API introduced in 5.1")]
	public class CatTemplatesApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatTemplatesRecord>, ICatTemplatesRequest, CatTemplatesDescriptor, CatTemplatesRequest>
	{
		public CatTemplatesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/templates";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Templates(),
			(client, f) => client.Cat.TemplatesAsync(),
			(client, r) => client.Cat.Templates(r),
			(client, r) => client.Cat.TemplatesAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatTemplatesRecord> response)
		{
#pragma warning disable CS0618 // Type or member is obsolete
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.IndexPatterns));
#pragma warning restore CS0618 // Type or member is obsolete
		}
	}
}
