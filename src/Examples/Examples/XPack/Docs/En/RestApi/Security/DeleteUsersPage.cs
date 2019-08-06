using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class DeleteUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line36()
		{
			// tag::ffd63dd186ab81b893faec3b3358fa09[]
			var response0 = new SearchResponse<object>();
			// end::ffd63dd186ab81b893faec3b3358fa09[]

			response0.MatchesExample(@"DELETE /_security/user/jacknich");
		}
	}
}