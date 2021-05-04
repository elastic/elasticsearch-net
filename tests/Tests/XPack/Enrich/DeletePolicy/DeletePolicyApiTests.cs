// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Transport.HttpMethod;

namespace Tests.XPack.Enrich.DeletePolicy
{
	[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
	public class DeletePolicyApiTests
		: ApiTestBase<EnrichCluster, DeleteEnrichPolicyResponse, IDeleteEnrichPolicyRequest, DeleteEnrichPolicyDescriptor, DeleteEnrichPolicyRequest>
	{
		public DeletePolicyApiTests(EnrichCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => DELETE;
		protected override string UrlPath => $"/_enrich/policy/{CallIsolatedValue}";

		protected override DeleteEnrichPolicyRequest Initializer => new DeleteEnrichPolicyRequest(CallIsolatedValue);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Enrich.DeletePolicy(CallIsolatedValue, f),
			(client, f) => client.Enrich.DeletePolicyAsync(CallIsolatedValue, f),
			(client, r) => client.Enrich.DeletePolicy(r),
			(client, r) => client.Enrich.DeletePolicyAsync(r)
		);
	}
}
