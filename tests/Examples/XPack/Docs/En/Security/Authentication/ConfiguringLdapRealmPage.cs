using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.Security.Authentication
{
	public class ConfiguringLdapRealmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line150()
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
		public void Line164()
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