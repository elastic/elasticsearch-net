using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class FuzzyQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line19()
		{
			// tag::d1e20f8f8c64f8e2cadea9e5c8376504[]
			var response0 = new SearchResponse<object>();
			// end::d1e20f8f8c64f8e2cadea9e5c8376504[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			       ""fuzzy"" : { ""user"" : ""ki"" }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line32()
		{
			// tag::f2f4631d427b04207285227d1ca6114d[]
			var response0 = new SearchResponse<object>();
			// end::f2f4631d427b04207285227d1ca6114d[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""fuzzy"" : {
			            ""user"" : {
			                ""value"": ""ki"",
			                ""boost"": 1.0,
			                ""fuzziness"": 2,
			                ""prefix_length"": 0,
			                ""max_expansions"": 100
			            }
			        }
			    }
			}");
		}
	}
}