// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class SamlLogoutApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/saml-logout-api.asciidoc:62")]
		public void Line62()
		{
			// tag::8d4dda5d988d568f4f4210a6387e026f[]
			var response0 = new SearchResponse<object>();
			// end::8d4dda5d988d568f4f4210a6387e026f[]

			response0.MatchesExample(@"POST /_security/saml/logout
			{
			  ""token"" : ""46ToAxZVaXVVZTVKOVF5YU04ZFJVUDVSZlV3"",
			  ""refresh_token"" : ""mJdXLtmvTUSpoLwMvdBt_w""
			}");
		}
	}
}
