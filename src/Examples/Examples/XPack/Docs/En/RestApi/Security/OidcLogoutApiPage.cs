using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class OidcLogoutApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line44()
		{
			// tag::2a1eece9a59ac1773edcf0a932c26de0[]
			var response0 = new SearchResponse<object>();
			// end::2a1eece9a59ac1773edcf0a932c26de0[]

			response0.MatchesExample(@"POST /_security/oidc/logout
			{
			  ""token"" : ""dGhpcyBpcyBub3QgYSByZWFsIHRva2VuIGJ1dCBpdCBpcyBvbmx5IHRlc3QgZGF0YS4gZG8gbm90IHRyeSB0byByZWFkIHRva2VuIQ=="",
			  ""refresh_token"": ""vLBPvmAB6KvwvJZr27cS""
			}");
		}
	}
}