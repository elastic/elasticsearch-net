using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetTokensPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line102()
		{
			// tag::cee591c1fc70d4f180c623a3a6d07755[]
			var response0 = new SearchResponse<object>();
			// end::cee591c1fc70d4f180c623a3a6d07755[]

			response0.MatchesExample(@"POST /_security/oauth2/token
			{
			  ""grant_type"" : ""client_credentials""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line137()
		{
			// tag::e1337c6b76defd5a46d05220f9d9c9fc[]
			var response0 = new SearchResponse<object>();
			// end::e1337c6b76defd5a46d05220f9d9c9fc[]

			response0.MatchesExample(@"POST /_security/oauth2/token
			{
			  ""grant_type"" : ""password"",
			  ""username"" : ""test_admin"",
			  ""password"" : ""x-pack-test-password""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line167()
		{
			// tag::1873f8a8a291e6fcd6c1c83ea6928759[]
			var response0 = new SearchResponse<object>();
			// end::1873f8a8a291e6fcd6c1c83ea6928759[]

			response0.MatchesExample(@"POST /_security/oauth2/token
			{
			    ""grant_type"": ""refresh_token"",
			    ""refresh_token"": ""vLBPvmAB6KvwvJZr27cS""
			}");
		}
	}
}