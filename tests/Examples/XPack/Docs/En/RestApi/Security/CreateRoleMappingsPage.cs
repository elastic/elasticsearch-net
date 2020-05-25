// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class CreateRoleMappingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-role-mappings.asciidoc:113")]
		public void Line113()
		{
			// tag::23b062c157235246d7c347b9047b2435[]
			var response0 = new SearchResponse<object>();
			// end::23b062c157235246d7c347b9047b2435[]

			response0.MatchesExample(@"POST /_security/role_mapping/mapping1
			{
			  ""roles"": [ ""user""],
			  ""enabled"": true, \<1>
			  ""rules"": {
			    ""field"" : { ""username"" : ""*"" }
			  },
			  ""metadata"" : { \<2>
			    ""version"" : 1
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-role-mappings.asciidoc:147")]
		public void Line147()
		{
			// tag::b176e0d428726705298184ef39ad5cb2[]
			var response0 = new SearchResponse<object>();
			// end::b176e0d428726705298184ef39ad5cb2[]

			response0.MatchesExample(@"POST /_security/role_mapping/mapping2
			{
			  ""roles"": [ ""user"", ""admin"" ],
			  ""enabled"": true,
			  ""rules"": {
			     ""field"" : { ""username"" : [ ""esadmin01"", ""esadmin02"" ] }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-role-mappings.asciidoc:160")]
		public void Line160()
		{
			// tag::e60b7f75ca806f2c74927c3d9409a986[]
			var response0 = new SearchResponse<object>();
			// end::e60b7f75ca806f2c74927c3d9409a986[]

			response0.MatchesExample(@"POST /_security/role_mapping/mapping3
			{
			  ""roles"": [ ""ldap-user"" ],
			  ""enabled"": true,
			  ""rules"": {
			    ""field"" : { ""realm.name"" : ""ldap1"" }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-role-mappings.asciidoc:175")]
		public void Line175()
		{
			// tag::7a23a385a63c87cab58fd494870450fd[]
			var response0 = new SearchResponse<object>();
			// end::7a23a385a63c87cab58fd494870450fd[]

			response0.MatchesExample(@"POST /_security/role_mapping/mapping4
			{
			  ""roles"": [ ""superuser"" ],
			  ""enabled"": true,
			  ""rules"": {
			    ""any"": [
			      {
			        ""field"": {
			          ""username"": ""esadmin""
			        }
			      },
			      {
			        ""field"": {
			          ""groups"": ""cn=admins,dc=example,dc=com""
			        }
			      }
			    ]
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-role-mappings.asciidoc:217")]
		public void Line217()
		{
			// tag::5ad365ed9e1a3c26093a0f09666c133a[]
			var response0 = new SearchResponse<object>();
			// end::5ad365ed9e1a3c26093a0f09666c133a[]

			response0.MatchesExample(@"POST /_security/role_mapping/mapping5
			{
			  ""role_templates"": [
			    {
			      ""template"": { ""source"": ""{{#tojson}}groups{{/tojson}}"" }, \<1>
			      ""format"" : ""json"" \<2>
			    }
			  ],
			  ""rules"": {
			    ""field"" : { ""realm.name"" : ""saml1"" }
			  },
			  ""enabled"": true
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-role-mappings.asciidoc:240")]
		public void Line240()
		{
			// tag::7e5faa551f2c95ffd627da352563d450[]
			var response0 = new SearchResponse<object>();
			// end::7e5faa551f2c95ffd627da352563d450[]

			response0.MatchesExample(@"POST /_security/role_mapping/mapping6
			{
			  ""roles"": [ ""example-user"" ],
			  ""enabled"": true,
			  ""rules"": {
			    ""field"" : { ""dn"" : ""*,ou=subtree,dc=example,dc=com"" }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-role-mappings.asciidoc:255")]
		public void Line255()
		{
			// tag::b80e1f5b26bae4f3c2f8a604b7caaf17[]
			var response0 = new SearchResponse<object>();
			// end::b80e1f5b26bae4f3c2f8a604b7caaf17[]

			response0.MatchesExample(@"POST /_security/role_mapping/mapping7
			{
			  ""roles"": [ ""ldap-example-user"" ],
			  ""enabled"": true,
			  ""rules"": {
			    ""all"": [
			      { ""field"" : { ""dn"" : ""*,ou=subtree,dc=example,dc=com"" } },
			      { ""field"" : { ""realm.name"" : ""ldap1"" } }
			    ]
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-role-mappings.asciidoc:279")]
		public void Line279()
		{
			// tag::0d94d76b7f00d0459d1f8c962c144dcd[]
			var response0 = new SearchResponse<object>();
			// end::0d94d76b7f00d0459d1f8c962c144dcd[]

			response0.MatchesExample(@"POST /_security/role_mapping/mapping8
			{
			  ""roles"": [ ""superuser"" ],
			  ""enabled"": true,
			  ""rules"": {
			    ""all"": [
			      {
			        ""any"": [
			          {
			            ""field"": {
			              ""dn"": ""*,ou=admin,dc=example,dc=com""
			            }
			          },
			          {
			            ""field"": {
			              ""username"": [ ""es-admin"", ""es-system"" ]
			            }
			          }
			        ]
			      },
			      {
			        ""field"": {
			          ""groups"": ""cn=people,dc=example,dc=com""
			        }
			      },
			      {
			        ""except"": {
			          ""field"": {
			            ""metadata.terminated_date"": null
			          }
			        }
			      }
			    ]
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/create-role-mappings.asciidoc:329")]
		public void Line329()
		{
			// tag::50dc35d3d8705bd62aed20a15209476c[]
			var response0 = new SearchResponse<object>();
			// end::50dc35d3d8705bd62aed20a15209476c[]

			response0.MatchesExample(@"POST /_security/role_mapping/mapping9
			{
			  ""rules"": { ""field"": { ""realm.name"": ""cloud-saml"" } },
			  ""role_templates"": [
			    { ""template"": { ""source"" : ""saml_user"" } }, \<1>
			    { ""template"": { ""source"" : ""_user_{{username}}"" } }
			  ],
			  ""enabled"": true
			}");
		}
	}
}
