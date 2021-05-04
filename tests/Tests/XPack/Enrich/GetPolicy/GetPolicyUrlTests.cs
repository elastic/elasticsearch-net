// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Enrich.GetEnrichPolicy
{
	public class GetEnrichPolicyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_enrich/policy/policy_name")
				.Fluent(c => c.Enrich.GetPolicy("policy_name", f => f))
				.Request(c => c.Enrich.GetPolicy(new GetEnrichPolicyRequest("policy_name")))
				.FluentAsync(c => c.Enrich.GetPolicyAsync("policy_name", f => f))
				.RequestAsync(c => c.Enrich.GetPolicyAsync(new GetEnrichPolicyRequest("policy_name")));

			await GET("/_enrich/policy/policy_name_1%2Cpolicy_name_2")
				.Fluent(c => c.Enrich.GetPolicy(new [] { "policy_name_1", "policy_name_2" }))
				.Request(c => c.Enrich.GetPolicy(new GetEnrichPolicyRequest(new [] { "policy_name_1", "policy_name_2" })))
				.FluentAsync(c => c.Enrich.GetPolicyAsync(new [] { "policy_name_1", "policy_name_2" }))
				.RequestAsync(c => c.Enrich.GetPolicyAsync(new GetEnrichPolicyRequest(new [] { "policy_name_1", "policy_name_2" })));

			await GET("/_enrich/policy/")
				.Fluent(c => c.Enrich.GetPolicy())
				.Request(c => c.Enrich.GetPolicy(new GetEnrichPolicyRequest()))
				.FluentAsync(c => c.Enrich.GetPolicyAsync())
				.RequestAsync(c => c.Enrich.GetPolicyAsync(new GetEnrichPolicyRequest()));
		}
	}
}
