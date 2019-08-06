using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class EnableUsersPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line41()
		{
			// tag::adf36e2d8fc05c3719c91912481c4e19[]
			var response0 = new SearchResponse<object>();
			// end::adf36e2d8fc05c3719c91912481c4e19[]

			response0.MatchesExample(@"PUT /_security/user/jacknich/_enable");
		}
	}
}