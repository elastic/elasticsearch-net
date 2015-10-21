using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.AliasManagement.PutAlias
{
	[Collection(IntegrationContext.Indexing)]
	public class PutAliasApiTests : ApiIntegrationTestBase<IPutAliasResponse, IPutAliasRequest, PutAliasDescriptor, PutAliasRequest>
	{

		public PutAliasApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutAlias(Static.AllIndices, CallIsolatedValue),
			fluentAsync: (client, f) => client.PutAliasAsync(Static.AllIndices, CallIsolatedValue),
			request: (client, r) => client.PutAlias(r),
			requestAsync: (client, r) => client.PutAliasAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_all/_alias/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		protected override Func<PutAliasDescriptor, IPutAliasRequest> Fluent => null;
		protected override PutAliasRequest Initializer => new PutAliasRequest(Static.AllIndices, CallIsolatedValue);

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
