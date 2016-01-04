using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.AliasManagement.AliasExists
{
	[Collection(IntegrationContext.ReadOnly)]
	public class AliasExistsApiTests : ApiIntegrationTestBase<IExistsResponse, IAliasExistsRequest, AliasExistsDescriptor, AliasExistsRequest>
	{
		public AliasExistsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientMethod, string> values)
		{
			foreach (var index in values.Values)
				client.CreateIndex(index, c=>c
					.Aliases(aa=>aa.Alias(index + "-alias"))
				);
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.AliasExists(f),
			fluentAsync: (client, f) => client.AliasExistsAsync(f),
			request: (client, r) => client.AliasExists(r),
			requestAsync: (client, r) => client.AliasExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/_alias/{CallIsolatedValue}-alias";

		protected override bool SupportsDeserialization => false;

		protected override Func<AliasExistsDescriptor, IAliasExistsRequest> Fluent => d => d
			.Name(CallIsolatedValue + "-alias");

		protected override AliasExistsRequest Initializer => new AliasExistsRequest(Names(CallIsolatedValue + "-alias"));
	}
}
