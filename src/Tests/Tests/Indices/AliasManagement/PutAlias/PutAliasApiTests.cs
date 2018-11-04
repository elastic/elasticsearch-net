using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.AliasManagement.PutAlias
{
	public class PutAliasApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IPutAliasResponse, IPutAliasRequest, PutAliasDescriptor, PutAliasRequest>
	{
		public PutAliasApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<PutAliasDescriptor, IPutAliasRequest> Fluent => null;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override PutAliasRequest Initializer => new PutAliasRequest(CallIsolatedValue, CallIsolatedValue + "-alias");

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{CallIsolatedValue}/_alias/{CallIsolatedValue + "-alias"}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.PutAlias(CallIsolatedValue, CallIsolatedValue + "-alias"),
			(client, f) => client.PutAliasAsync(CallIsolatedValue, CallIsolatedValue + "-alias"),
			(client, r) => client.PutAlias(r),
			(client, r) => client.PutAliasAsync(r)
		);
	}
}
