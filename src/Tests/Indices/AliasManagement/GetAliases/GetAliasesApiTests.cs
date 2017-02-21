using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Indices.AliasManagement.GetAliases
{
	public class GetAliasesApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetAliasesResponse, IGetAliasesRequest, GetAliasesDescriptor, GetAliasesRequest>
	{
		private static readonly Names Names = Infer.Names("alias, x", "y");

		public GetAliasesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
#pragma warning disable 618 //testing an obsolete message
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetAliases(f),
			fluentAsync: (client, f) => client.GetAliasesAsync(f),
			request: (client, r) => client.GetAliases(r),
			requestAsync: (client, r) => client.GetAliasesAsync(r)
		);
#pragma warning restore 618

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_all/_aliases/alias%2Cx%2Cy";

		protected override bool SupportsDeserialization => false;

		protected override Func<GetAliasesDescriptor, IGetAliasesRequest> Fluent => d=>d
			.AllIndices()
			.Name(Names)
		;
		protected override GetAliasesRequest Initializer => new GetAliasesRequest(Infer.AllIndices, Names);
	}
}
