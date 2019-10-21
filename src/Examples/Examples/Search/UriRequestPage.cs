using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search
{
	public class UriRequestPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line7()
		{
			// tag::68188db64fc50a9b35e5646493b00d2c[]
			var response0 = new SearchResponse<object>();
			// end::68188db64fc50a9b35e5646493b00d2c[]

			response0.MatchesExample(@"GET twitter/_search?q=user:kimchy");
		}
	}
}