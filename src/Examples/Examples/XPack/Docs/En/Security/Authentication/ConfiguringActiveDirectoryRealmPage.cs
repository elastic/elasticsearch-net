using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.Security.Authentication
{
	public class ConfiguringActiveDirectoryRealmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line188()
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
		public void Line202()
		{
			// tag::bd0d30a7683037e1ebadd163514765d4[]
			var response0 = new SearchResponse<object>();
			// end::bd0d30a7683037e1ebadd163514765d4[]

			response0.MatchesExample(@"PUT /_security/role_mapping/basic_users
			{
			  ""roles"" : [ ""user"" ],
			  ""rules"" : { ""any"": [
			    { ""field"" : {
			      ""groups"" : ""cn=users,dc=example,dc=com"" \<1>
			    } },
			    { ""field"" : {
			      ""dn"" : ""cn=John Doe,cn=contractors,dc=example,dc=com"" \<2>
			    } }
			  ] },
			  ""enabled"": true
			}");
		}
	}
}