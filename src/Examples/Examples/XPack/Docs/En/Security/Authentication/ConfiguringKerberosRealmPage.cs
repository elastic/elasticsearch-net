using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.Security.Authentication
{
	public class ConfiguringKerberosRealmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line155()
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