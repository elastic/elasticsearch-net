using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.Enrich.ExecutePolicy
{
	[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
	public class ExecutePolicyApiTests
		: ApiTestBase<EnrichCluster, ExecuteEnrichPolicyResponse, IExecuteEnrichPolicyRequest, ExecuteEnrichPolicyDescriptor, ExecuteEnrichPolicyRequest>
	{
		public ExecutePolicyApiTests(EnrichCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => PUT;

		protected override string UrlPath => $"/_enrich/policy/{CallIsolatedValue}/_execute";

		protected override ExecuteEnrichPolicyRequest Initializer => new ExecuteEnrichPolicyRequest(CallIsolatedValue);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Enrich.ExecutePolicy(CallIsolatedValue, f),
			(client, f) => client.Enrich.ExecutePolicyAsync(CallIsolatedValue, f),
			(client, r) => client.Enrich.ExecutePolicy(r),
			(client, r) => client.Enrich.ExecutePolicyAsync(r)
		);
	}
}
