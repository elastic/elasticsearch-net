using System;
using Elastic.Xunit.XunitPlumbing;
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

	[SkipVersion("<6.4.0", "is_write_index is a new feature")]
	public class PutAliasIsWriteIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IPutAliasResponse, IPutAliasRequest, PutAliasDescriptor, PutAliasRequest>
	{
		public PutAliasIsWriteIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new { is_write_index = true };
		protected override int ExpectStatusCode => 200;

		protected override Func<PutAliasDescriptor, IPutAliasRequest> Fluent => f => f.IsWriteIndex();
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutAliasRequest Initializer => new PutAliasRequest(CallIsolatedValue, CallIsolatedValue + "-alias")
		{
			IsWriteIndex = true
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{CallIsolatedValue}/_alias/{CallIsolatedValue + "-alias"}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.PutAlias(CallIsolatedValue, CallIsolatedValue + "-alias"),
			(client, f) => client.PutAliasAsync(CallIsolatedValue, CallIsolatedValue + "-alias"),
			(client, r) => client.PutAlias(r),
			(client, r) => client.PutAliasAsync(r)
		);

		protected override PutAliasDescriptor NewDescriptor() => new PutAliasDescriptor(CallIsolatedValue, CallIsolatedValue + "-alias");
	}
}
