// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class OidcPrepareAuthenticationApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/oidc-prepare-authentication-api.asciidoc:69")]
		public void Line69()
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
		[Description("../../x-pack/docs/en/rest-api/security/oidc-prepare-authentication-api.asciidoc:95")]
		public void Line95()
		{
			// tag::57dc15e5ad663c342fd5c1d86fcd1b29[]
			var response0 = new SearchResponse<object>();
			// end::57dc15e5ad663c342fd5c1d86fcd1b29[]

			response0.MatchesExample(@"POST /_security/oidc/prepare
			{
			  ""realm"" : ""oidc1"",
			  ""state"" : ""lGYK0EcSLjqH6pkT5EVZjC6eIW5YCGgywj2sxROO"",
			  ""nonce"" : ""zOBXLJGUooRrbLbQk5YCcyC8AXw3iloynvluYhZ5""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/oidc-prepare-authentication-api.asciidoc:121")]
		public void Line121()
		{
			// tag::d35c8cf7a98b3f112e1de8797ec6689d[]
			var response0 = new SearchResponse<object>();
			// end::d35c8cf7a98b3f112e1de8797ec6689d[]

			response0.MatchesExample(@"POST /_security/oidc/prepare
			{
			  ""iss"" : ""http://127.0.0.1:8080"",
			  ""login_hint"": ""this_is_an_opaque_string""
			}");
		}
	}
}
