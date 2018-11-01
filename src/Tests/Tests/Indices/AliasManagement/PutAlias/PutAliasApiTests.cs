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

	[SkipVersion("<6.4.0", "is_write_index is a new feature")]
	public class PutAliasIsWriteIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IPutAliasResponse, IPutAliasRequest, PutAliasDescriptor, PutAliasRequest>
	{
		public PutAliasIsWriteIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

		protected override object ExpectJson => new { is_write_index = true };

		protected override PutAliasDescriptor NewDescriptor() => new PutAliasDescriptor(CallIsolatedValue, CallIsolatedValue + "-alias");

		protected override Func<PutAliasDescriptor, IPutAliasRequest> Fluent => f => f.IsWriteIndex();

		protected override PutAliasRequest Initializer => new PutAliasRequest(CallIsolatedValue, CallIsolatedValue + "-alias")
		{
			IsWriteIndex = true
		};
	}
}
