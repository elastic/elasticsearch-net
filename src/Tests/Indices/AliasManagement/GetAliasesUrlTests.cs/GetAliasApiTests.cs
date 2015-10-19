using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.AliasesManagement.GetAliases
{
	[Collection(IntegrationContext.ReadOnly)]
	public class GetAliasesApiTests : ApiTestBase<IGetAliasesResponse, IGetAliasesRequest, GetAliasesDescriptor, GetAliasesRequest>
	{
		private readonly static Names Names = Static.Names("alias, x", "y");

		public GetAliasesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetAliases(f),
			fluentAsync: (client, f) => client.GetAliasesAsync(f),
			request: (client, r) => client.GetAliases(r),
			requestAsync: (client, r) => client.GetAliasesAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_all/_aliases/alias,x,y";

		protected override bool SupportsDeserialization => false;

		protected override Func<GetAliasesDescriptor, IGetAliasesRequest> Fluent => d=>d
			.AllIndices()
			.Name(Names)
		;
		protected override GetAliasesRequest Initializer => new GetAliasesRequest(Static.AllIndices, Names);
	}
}
