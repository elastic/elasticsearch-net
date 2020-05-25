using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Enrich.ExecutePolicy
{
	public class ExecutePolicyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_enrich/policy/policy_name/_execute")
			.Fluent(c => c.Enrich.ExecutePolicy("policy_name"))
			.Request(c => c.Enrich.ExecutePolicy(new ExecuteEnrichPolicyRequest("policy_name")))
			.FluentAsync(c => c.Enrich.ExecutePolicyAsync("policy_name"))
			.RequestAsync(c => c.Enrich.ExecutePolicyAsync(new ExecuteEnrichPolicyRequest("policy_name")));
	}
}
