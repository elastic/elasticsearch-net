// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Enrich.DeletePolicy
{
	public class DeletePolicyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_enrich/policy/policy_name")
			.Fluent(c => c.Enrich.DeletePolicy("policy_name", f => f))
			.Request(c => c.Enrich.DeletePolicy(new DeleteEnrichPolicyRequest("policy_name")))
			.FluentAsync(c => c.Enrich.DeletePolicyAsync("policy_name", f => f))
			.RequestAsync(c => c.Enrich.DeletePolicyAsync(new DeleteEnrichPolicyRequest("policy_name")));
	}
}
