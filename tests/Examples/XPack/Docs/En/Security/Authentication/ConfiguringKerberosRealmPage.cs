// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.Security.Authentication
{
	public class ConfiguringKerberosRealmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/configuring-kerberos-realm.asciidoc:179")]
		public void Line179()
		{
			// tag::9584b042223982e0bfde8d12d42c9705[]
			var response0 = new SearchResponse<object>();
			// end::9584b042223982e0bfde8d12d42c9705[]

			response0.MatchesExample(@"POST /_security/role_mapping/kerbrolemapping
			{
			  ""roles"" : [ ""monitoring_user"" ],
			  ""enabled"": true,
			  ""rules"" : {
			    ""field"" : { ""username"" : ""user@REALM"" }
			  }
			}");
		}
	}
}
