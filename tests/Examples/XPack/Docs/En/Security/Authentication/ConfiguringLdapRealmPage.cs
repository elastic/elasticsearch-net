// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.Security.Authentication
{
	public class ConfiguringLdapRealmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/configuring-ldap-realm.asciidoc:138")]
		public void Line138()
		{
			// tag::21e95d29bc37deb5689a654aa323b4ba[]
			var response0 = new SearchResponse<object>();
			// end::21e95d29bc37deb5689a654aa323b4ba[]

			response0.MatchesExample(@"PUT /_security/role_mapping/admins
			{
			  ""roles"" : [ ""monitoring"" , ""user"" ],
			  ""rules"" : { ""field"" : {
			    ""groups"" : ""cn=admins,dc=example,dc=com"" \<1>
			  } },
			  ""enabled"": true
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/authentication/configuring-ldap-realm.asciidoc:152")]
		public void Line152()
		{
			// tag::38ffa96674b5fd4042589af0ebb0437b[]
			var response0 = new SearchResponse<object>();
			// end::38ffa96674b5fd4042589af0ebb0437b[]

			response0.MatchesExample(@"PUT /_security/role_mapping/basic_users
			{
			  ""roles"" : [ ""user"" ],
			  ""rules"" : { ""field"" : {
			    ""groups"" : ""cn=users,dc=example,dc=com"" \<1>
			  } },
			  ""enabled"": true
			}");
		}
	}
}
