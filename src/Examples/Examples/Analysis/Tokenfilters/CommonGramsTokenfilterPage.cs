using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class CommonGramsTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line43()
		{
			// tag::dc0d7dd6fb47db03df4cb11bdb00b125[]
			var response0 = new SearchResponse<object>();
			// end::dc0d7dd6fb47db03df4cb11bdb00b125[]

			response0.MatchesExample(@"PUT /common_grams_example
			{
			    ""settings"": {
			        ""analysis"": {
			            ""analyzer"": {
			                ""index_grams"": {
			                    ""tokenizer"": ""whitespace"",
			                    ""filter"": [""common_grams""]
			                },
			                ""search_grams"": {
			                    ""tokenizer"": ""whitespace"",
			                    ""filter"": [""common_grams_query""]
			                }
			            },
			            ""filter"": {
			                ""common_grams"": {
			                    ""type"": ""common_grams"",
			                    ""common_words"": [""the"", ""is"", ""a""]
			                },
			                ""common_grams_query"": {
			                    ""type"": ""common_grams"",
			                    ""query_mode"": true,
			                    ""common_words"": [""the"", ""is"", ""a""]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line78()
		{
			// tag::817be849e2c568a21766d6ce2ffafadd[]
			var response0 = new SearchResponse<object>();
			// end::817be849e2c568a21766d6ce2ffafadd[]

			response0.MatchesExample(@"POST /common_grams_example/_analyze
			{
			  ""analyzer"" : ""index_grams"",
			  ""text"" : ""the quick brown is a fox""
			}");
		}
	}
}