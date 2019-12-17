using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Enrich.PutPolicy
{
	public class PutPolicyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_enrich/policy/policy_name")
			.Fluent(c => c.Enrich.PutPolicy<object>("policy_name", f => f))
			.Request(c => c.Enrich.PutPolicy(new PutEnrichPolicyRequest("policy_name")))
			.FluentAsync(c => c.Enrich.PutPolicyAsync<object>("policy_name", f => f))
			.RequestAsync(c => c.Enrich.PutPolicyAsync(new PutEnrichPolicyRequest("policy_name")));
	}
}
