using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class DisableUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line45()
		{
			// tag::bb293e1bdf0c6f6d9069eeb7edc9d399[]
			var response0 = new SearchResponse<object>();
			// end::bb293e1bdf0c6f6d9069eeb7edc9d399[]

			response0.MatchesExample(@"PUT /_security/user/jacknich/_disable");
		}
	}
}