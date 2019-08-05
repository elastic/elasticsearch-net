using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetUsersPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line41()
		{
			// tag::3924ee252581ebb96ac0e60046125ae8[]
			var response0 = new SearchResponse<object>();
			// end::3924ee252581ebb96ac0e60046125ae8[]

			response0.MatchesExample(@"GET /_security/user/jacknich");
		}

		[U]
		[SkipExample]
		public void Line51()
		{
			// tag::bac6203259754d2f09c1ebeecc9ded5d[]
			var response0 = new SearchResponse<object>();
			// end::bac6203259754d2f09c1ebeecc9ded5d[]

			response0.MatchesExample(@"{
			  ""jacknich"": {
			    ""username"": ""jacknich"",
			    ""roles"": [
			      ""admin"", ""other_role1""
			    ],
			    ""full_name"": ""Jack Nicholson"",
			    ""email"": ""jacknich@example.com"",
			    ""metadata"": { ""intelligence"" : 7 },
			    ""enabled"": true
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line73()
		{
			// tag::abdbc81e799e28c833556b1c29f03ba6[]
			var response0 = new SearchResponse<object>();
			// end::abdbc81e799e28c833556b1c29f03ba6[]

			response0.MatchesExample(@"GET /_security/user");
		}
	}
}