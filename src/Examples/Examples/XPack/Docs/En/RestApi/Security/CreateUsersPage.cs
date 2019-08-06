using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class CreateUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line100()
		{
			// tag::4c514b787945952a223cde8a4a09e826[]
			var response0 = new SearchResponse<object>();
			// end::4c514b787945952a223cde8a4a09e826[]

			response0.MatchesExample(@"POST /_security/user/jacknich
			{
			  ""password"" : ""j@rV1s"",
			  ""roles"" : [ ""admin"", ""other_role1"" ],
			  ""full_name"" : ""Jack Nicholson"",
			  ""email"" : ""jacknich@example.com"",
			  ""metadata"" : {
			    ""intelligence"" : 7
			  }
			}");
		}
	}
}