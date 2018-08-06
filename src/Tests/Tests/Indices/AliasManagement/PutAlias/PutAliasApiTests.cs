using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Indices.AliasManagement.PutAlias
{
	public class PutAliasApiTests : ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IPutAliasResponse, IPutAliasRequest, PutAliasDescriptor, PutAliasRequest>
	{
		public PutAliasApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutAlias(CallIsolatedValue, CallIsolatedValue + "-alias"),
			fluentAsync: (client, f) => client.PutAliasAsync(CallIsolatedValue, CallIsolatedValue + "-alias"),
			request: (client, r) => client.PutAlias(r),
			requestAsync: (client, r) => client.PutAliasAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/{CallIsolatedValue}/_alias/{CallIsolatedValue + "-alias"}";

		protected override bool SupportsDeserialization => false;

		protected override Func<PutAliasDescriptor, IPutAliasRequest> Fluent => null;
		protected override PutAliasRequest Initializer => new PutAliasRequest(CallIsolatedValue, CallIsolatedValue + "-alias");
	}
}
