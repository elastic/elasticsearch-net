using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class FuzzyQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line29()
		{
			// tag::10dd8b5da64f1f6af031706dd50bc9b5[]
			var response0 = new SearchResponse<object>();
			// end::10dd8b5da64f1f6af031706dd50bc9b5[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""fuzzy"": {
			            ""user"": {
			                ""value"": ""ki""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line46()
		{
			// tag::8baebb670ca5624d7920ccac4afdff06[]
			var response0 = new SearchResponse<object>();
			// end::8baebb670ca5624d7920ccac4afdff06[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""fuzzy"": {
			            ""user"": {
			                ""value"": ""ki"",
			                ""fuzziness"": ""AUTO"",
			                ""max_expansions"": 50,
			                ""prefix_length"": 0,
			                ""transpositions"": true,
			                ""rewrite"": ""constant_score""
			            }
			        }
			    }
			}");
		}
	}
}