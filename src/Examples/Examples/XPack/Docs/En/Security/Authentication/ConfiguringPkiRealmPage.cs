using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.Security.Authentication
{
	public class ConfiguringPkiRealmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line140()
		{
			// tag::70bbe14bc4d5a5d58e81ab2b02408817[]
			var response0 = new SearchResponse<object>();
			// end::70bbe14bc4d5a5d58e81ab2b02408817[]

			response0.MatchesExample(@"PUT /_security/role_mapping/users
			{
			  ""roles"" : [ ""user"" ],
			  ""rules"" : { ""field"" : {
			    ""dn"" : ""cn=John Doe,ou=example,o=com"" \<1>
			  } },
			  ""enabled"": true
			}");
		}
	}
}