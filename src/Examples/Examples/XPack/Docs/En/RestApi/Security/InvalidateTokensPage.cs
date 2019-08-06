using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class InvalidateTokensPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line61()
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
		public void Line73()
		{
			// tag::da2bb6894d95489812b653be2feeeb5b[]
			var response0 = new SearchResponse<object>();
			// end::da2bb6894d95489812b653be2feeeb5b[]

			response0.MatchesExample(@"{
			  ""access_token"" : ""dGhpcyBpcyBub3QgYSByZWFsIHRva2VuIGJ1dCBpdCBpcyBvbmx5IHRlc3QgZGF0YS4gZG8gbm90IHRyeSB0byByZWFkIHRva2VuIQ=="",
			  ""type"" : ""Bearer"",
			  ""expires_in"" : 1200
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line87()
		{
			// tag::dbf9abc37899352751dab0ede62af2fd[]
			var response0 = new SearchResponse<object>();
			// end::dbf9abc37899352751dab0ede62af2fd[]

			response0.MatchesExample(@"DELETE /_security/oauth2/token
			{
			  ""token"" : ""dGhpcyBpcyBub3QgYSByZWFsIHRva2VuIGJ1dCBpdCBpcyBvbmx5IHRlc3QgZGF0YS4gZG8gbm90IHRyeSB0byByZWFkIHRva2VuIQ==""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line101()
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
		public void Line115()
		{
			// tag::774740abbecda50b03d75dbff8cbe60f[]
			var response0 = new SearchResponse<object>();
			// end::774740abbecda50b03d75dbff8cbe60f[]

			response0.MatchesExample(@"{
			  ""access_token"" : ""dGhpcyBpcyBub3QgYSByZWFsIHRva2VuIGJ1dCBpdCBpcyBvbmx5IHRlc3QgZGF0YS4gZG8gbm90IHRyeSB0byByZWFkIHRva2VuIQ=="",
			  ""type"" : ""Bearer"",
			  ""expires_in"" : 1200,
			  ""refresh_token"": ""vLBPvmAB6KvwvJZr27cS""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line131()
		{
			// tag::0c6f9c9da75293fae69659ac1d6329de[]
			var response0 = new SearchResponse<object>();
			// end::0c6f9c9da75293fae69659ac1d6329de[]

			response0.MatchesExample(@"DELETE /_security/oauth2/token
			{
			  ""refresh_token"" : ""vLBPvmAB6KvwvJZr27cS""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line145()
		{
			// tag::4bc4db44b8c74610b73f21a421099a13[]
			var response0 = new SearchResponse<object>();
			// end::4bc4db44b8c74610b73f21a421099a13[]

			response0.MatchesExample(@"DELETE /_security/oauth2/token
			{
			  ""realm_name"" : ""saml1""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line158()
		{
			// tag::0280247e0cf2e561c548f22c9fb31163[]
			var response0 = new SearchResponse<object>();
			// end::0280247e0cf2e561c548f22c9fb31163[]

			response0.MatchesExample(@"DELETE /_security/oauth2/token
			{
			  ""username"" : ""myuser""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line171()
		{
			// tag::6dd2a107bc64fd6f058fb17c21640649[]
			var response0 = new SearchResponse<object>();
			// end::6dd2a107bc64fd6f058fb17c21640649[]

			response0.MatchesExample(@"DELETE /_security/oauth2/token
			{
			  ""username"" : ""myuser"",
			  ""realm_name"" : ""saml1""
			}");
		}
	}
}