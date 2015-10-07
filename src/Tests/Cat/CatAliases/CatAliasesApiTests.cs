using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatAliases
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CatAliasesApiTests : ApiTestBase<ICatResponse<CatAliasesRecord>, ICatAliasesRequest, CatAliasesDescriptor, CatAliasesRequest>
	{
		public CatAliasesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public override bool ExpectIsValid => true;
		public override int ExpectStatusCode => 200;
		public override HttpMethod HttpMethod => HttpMethod.GET;
		public override string UrlPath => "/_cat/aliases";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatAliases(),
			fluentAsync: (client, f) => client.CatAliasesAsync(),
			request: (client, r) => client.CatAliases(r),
			requestAsync: (client, r) => client.CatAliasesAsync(r)
		);

		protected override object ExpectJson { get; } = new {};

		protected override Func<CatAliasesDescriptor, ICatAliasesRequest> Fluent => d => d;

		protected override CatAliasesRequest Initializer => new CatAliasesRequest();
	}

}
