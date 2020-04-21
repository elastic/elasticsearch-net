using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.Security.Authentication
{
	public class OidcGuidePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/oidc-guide.asciidoc:427")]
		public void Line427()
		{
			// tag::10de9fd4a38755020a07c4ec964d44c9[]
			var response0 = new SearchResponse<object>();
			// end::10de9fd4a38755020a07c4ec964d44c9[]

			response0.MatchesExample(@"PUT /_security/role_mapping/oidc-example
			{
			  ""roles"": [ ""example_role"" ], <1>
			  ""enabled"": true,
			  ""rules"": {
			    ""field"": { ""realm.name"": ""oidc1"" }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/oidc-guide.asciidoc:466")]
		public void Line466()
		{
			// tag::f3ab820e1f2f54ea718017aeae865742[]
			var response0 = new SearchResponse<object>();
			// end::f3ab820e1f2f54ea718017aeae865742[]

			response0.MatchesExample(@"PUT /_security/role_mapping/oidc-finance
			{
			  ""roles"": [ ""finance_data"" ],
			  ""enabled"": true,
			  ""rules"": { ""all"": [
			        { ""field"": { ""realm.name"": ""oidc1"" } },
			        { ""field"": { ""groups"": ""finance-team"" } }
			  ] }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/oidc-guide.asciidoc:601")]
		public void Line601()
		{
			// tag::a325f31e94fb1e8739258910593504a8[]
			var response0 = new SearchResponse<object>();
			// end::a325f31e94fb1e8739258910593504a8[]

			response0.MatchesExample(@"POST /_security/role/facilitator-role
			{
			  ""cluster"" : [""manage_oidc"", ""manage_token""]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/oidc-guide.asciidoc:610")]
		public void Line610()
		{
			// tag::53e4ac5a4009fd21024f4b31e54aa83f[]
			var response0 = new SearchResponse<object>();
			// end::53e4ac5a4009fd21024f4b31e54aa83f[]

			response0.MatchesExample(@"POST /_security/user/facilitator
			{
			  ""password"" : ""<somePasswordHere>"",
			  ""roles""    : [ ""facilitator-role""]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/oidc-guide.asciidoc:630")]
		public void Line630()
		{
			// tag::e3019fd5f23458ae49ad9854c97d321c[]
			var response0 = new SearchResponse<object>();
			// end::e3019fd5f23458ae49ad9854c97d321c[]

			response0.MatchesExample(@"POST /_security/oidc/prepare
			{
			  ""realm"" : ""oidc1""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/oidc-guide.asciidoc:653")]
		public void Line653()
		{
			// tag::9c01db07c9ac395b6370e3b33965c21f[]
			var response0 = new SearchResponse<object>();
			// end::9c01db07c9ac395b6370e3b33965c21f[]

			response0.MatchesExample(@"POST /_security/oidc/authenticate
			{
			  ""redirect_uri"" : ""https://oidc-kibana.elastic.co:5603/api/security/oidc/callback?code=jtI3Ntt8v3_XvcLzCFGq&state=4dbrihtIAt3wBTwo6DxK-vdk-sSyDBV8Yf0AjdkdT5I"",
			  ""state"" : ""4dbrihtIAt3wBTwo6DxK-vdk-sSyDBV8Yf0AjdkdT5I"",
			  ""nonce"" : ""WaBPH0KqPVdG5HHdSxPRjfoZbXMCicm5v1OiAj0DUFM"",
			  ""realm"" : ""oidc1""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/oidc-guide.asciidoc:671")]
		public void Line671()
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