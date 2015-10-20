using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.AliasManagement.DeleteAlias
{
	[Collection(IntegrationContext.Indexing)]
	public class DeleteAliasApiTests : ApiIntegrationTestBase<IDeleteAliasResponse, IDeleteAliasRequest, DeleteAliasDescriptor, DeleteAliasRequest>
	{
		private readonly static Names Names = Static.Names("alias, x", "y");

		public DeleteAliasApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteAlias(Static.AllIndices, Names),
			fluentAsync: (client, f) => client.DeleteAliasAsync(Static.AllIndices, Names),
			request: (client, r) => client.DeleteAlias(r),
			requestAsync: (client, r) => client.DeleteAliasAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_all/_alias/alias,x,y";

		protected override bool SupportsDeserialization => false;

		protected override Func<DeleteAliasDescriptor, IDeleteAliasRequest> Fluent => null;
		protected override DeleteAliasRequest Initializer => new DeleteAliasRequest(Static.AllIndices, Names);

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
