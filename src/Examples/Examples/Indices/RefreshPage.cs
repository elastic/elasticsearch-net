using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class RefreshPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::c2ac42934e4b76197032b2fc429e317d[]
			var response0 = new SearchResponse<object>();
			// end::c2ac42934e4b76197032b2fc429e317d[]

			response0.MatchesExample(@"POST /twitter/_refresh");
		}

		[U(Skip = "Example not implemented")]
		public void Line91()
		{
			// tag::0e98949e80e665795bc6cfc997165241[]
			var response0 = new SearchResponse<object>();
			// end::0e98949e80e665795bc6cfc997165241[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_refresh");
		}

		[U(Skip = "Example not implemented")]
		public void Line101()
		{
			// tag::d7898526d239d2aea83727fb982f8f77[]
			var response0 = new SearchResponse<object>();
			// end::d7898526d239d2aea83727fb982f8f77[]

			response0.MatchesExample(@"POST /_refresh");
		}
	}
}