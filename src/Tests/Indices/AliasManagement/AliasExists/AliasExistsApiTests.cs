using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.AliasManagement.AliasExists
{
	[Collection(IntegrationContext.ReadOnly)]
	public class AliasExistsApiTests : ApiIntegrationTestBase<IExistsResponse, IAliasExistsRequest, AliasExistsDescriptor, AliasExistsRequest>
	{
		public AliasExistsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.AliasExists(f),
			fluentAsync: (client, f) => client.AliasExistsAsync(f),
			request: (client, r) => client.AliasExists(r),
			requestAsync: (client, r) => client.AliasExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/_alias/alias,x,y";

		protected override bool SupportsDeserialization => false;

		protected override Func<AliasExistsDescriptor, IAliasExistsRequest> Fluent => d => d
			.Name("alias, x,y");

		protected override AliasExistsRequest Initializer => new AliasExistsRequest(Static.Names("alias, x","y"));

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
