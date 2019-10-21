using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search
{
	public class ExplainPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line8()
		{
			// tag::abfec22fbe7d571711cc65661ca887ee[]
			var response0 = new SearchResponse<object>();
			// end::abfec22fbe7d571711cc65661ca887ee[]

			response0.MatchesExample(@"GET /twitter/_explain/0
			{
			      ""query"" : {
			        ""match"" : { ""message"" : ""elasticsearch"" }
			      }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line182()
		{
			// tag::5032518611d928d1f802e215cf79c550[]
			var response0 = new SearchResponse<object>();
			// end::5032518611d928d1f802e215cf79c550[]

			response0.MatchesExample(@"GET /twitter/_explain/0?q=message:search");
		}
	}
}