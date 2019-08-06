using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class RefreshPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::c2ac42934e4b76197032b2fc429e317d[]
			var response0 = new SearchResponse<object>();
			// end::c2ac42934e4b76197032b2fc429e317d[]

			response0.MatchesExample(@"POST /twitter/_refresh");
		}

		[U(Skip = "Example not implemented")]
		public void Line24()
		{
			// tag::104c5a6faa3052d18567c1ae57278638[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::104c5a6faa3052d18567c1ae57278638[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_refresh");

			response1.MatchesExample(@"POST /_refresh");
		}
	}
}