// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class OidcLogoutApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/oidc-logout-api.asciidoc:44")]
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
