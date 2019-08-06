using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class SegmentsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line12()
		{
			// tag::940e8c2c7ff92d71f489bdb7183c1ce6[]
			var response0 = new SearchResponse<object>();
			// end::940e8c2c7ff92d71f489bdb7183c1ce6[]

			response0.MatchesExample(@"GET /test/_segments");
		}

		[U(Skip = "Example not implemented")]
		public void Line22()
		{
			// tag::975b4b92464d52068516aa2f0f955cc1[]
			var response0 = new SearchResponse<object>();
			// end::975b4b92464d52068516aa2f0f955cc1[]

			response0.MatchesExample(@"GET /test1,test2/_segments");
		}

		[U(Skip = "Example not implemented")]
		public void Line31()
		{
			// tag::6414b9276ba1c63898c3ff5cbe03c54e[]
			var response0 = new SearchResponse<object>();
			// end::6414b9276ba1c63898c3ff5cbe03c54e[]

			response0.MatchesExample(@"GET /_segments");
		}

		[U(Skip = "Example not implemented")]
		public void Line129()
		{
			// tag::1b21d886f6e9619c73079d14581ccbe4[]
			var response0 = new SearchResponse<object>();
			// end::1b21d886f6e9619c73079d14581ccbe4[]

			response0.MatchesExample(@"GET /test/_segments?verbose=true");
		}
	}
}