// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Transport.HttpMethod;

namespace Tests.XPack.Enrich.GetPolicy
{
	[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
	public class GetPolicyApiTests
		: ApiTestBase<EnrichCluster, GetEnrichPolicyResponse, IGetEnrichPolicyRequest, GetEnrichPolicyDescriptor, GetEnrichPolicyRequest>
	{
		private static readonly string PolicyName = "example_policy";

		public GetPolicyApiTests(EnrichCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => GET;

		protected override string UrlPath => $"/_enrich/policy/{PolicyName}";

		protected override GetEnrichPolicyRequest Initializer => new GetEnrichPolicyRequest(PolicyName);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Enrich.GetPolicy(PolicyName, f),
			(client, f) => client.Enrich.GetPolicyAsync(PolicyName, f),
			(client, r) => client.Enrich.GetPolicy(r),
			(client, r) => client.Enrich.GetPolicyAsync(r)
		);
	}
}
