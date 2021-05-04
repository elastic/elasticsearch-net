// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class SamlPrepareAuthenticationApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/saml-prepare-authentication-api.asciidoc:70")]
		public void Line70()
		{
			// tag::a5dfcfd1cfb3558e7912456669c92eee[]
			var response0 = new SearchResponse<object>();
			// end::a5dfcfd1cfb3558e7912456669c92eee[]

			response0.MatchesExample(@"POST /_security/saml/prepare
			{
			  ""realm"" : ""saml1""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/saml-prepare-authentication-api.asciidoc:81")]
		public void Line81()
		{
			// tag::da3f280bc65b581fb3097be768061bee[]
			var response0 = new SearchResponse<object>();
			// end::da3f280bc65b581fb3097be768061bee[]

			response0.MatchesExample(@"POST /_security/saml/prepare
			{
			  ""acs"" : ""https://kibana.org/api/security/saml/callback""
			}");
		}
	}
}
