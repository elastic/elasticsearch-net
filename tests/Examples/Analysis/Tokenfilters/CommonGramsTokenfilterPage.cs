using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class CommonGramsTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line28()
		{
			// tag::2fd0b3c132b46aa34cc9d92dd2d4bc85[]
			var response0 = new SearchResponse<object>();
			// end::2fd0b3c132b46aa34cc9d92dd2d4bc85[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""whitespace"",
			  ""filter"" : [
			    {
			      ""type"": ""common_grams"",
			      ""common_words"": [""is"", ""the""]
			    }
			  ],
			  ""text"" : ""the quick fox is brown""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line126()
		{
			// tag::63de16d533d65708cf794eb50da02fbd[]
			var response0 = new SearchResponse<object>();
			// end::63de16d533d65708cf794eb50da02fbd[]

			response0.MatchesExample(@"PUT /common_grams_example
			{
			    ""settings"": {
			        ""analysis"": {
			            ""analyzer"": {
			              ""index_grams"": {
			                  ""tokenizer"": ""whitespace"",
			                  ""filter"": [""common_grams""]
			              }
			            },
			            ""filter"": {
			              ""common_grams"": {
			                  ""type"": ""common_grams"",
			                  ""common_words"": [""a"", ""is"", ""the""]
			              }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line206()
		{
			// tag::d2d5a5fa4ab40787db87c85e1dd2bd06[]
			var response0 = new SearchResponse<object>();
			// end::d2d5a5fa4ab40787db87c85e1dd2bd06[]

			response0.MatchesExample(@"PUT /common_grams_example
			{
			    ""settings"": {
			        ""analysis"": {
			            ""analyzer"": {
			              ""index_grams"": {
			                  ""tokenizer"": ""whitespace"",
			                  ""filter"": [""common_grams_query""]
			              }
			            },
			            ""filter"": {
			              ""common_grams_query"": {
			                  ""type"": ""common_grams"",
			                  ""common_words"": [""a"", ""is"", ""the""],
			                  ""ignore_case"": true,
			                  ""query_mode"": true
			              }
			            }
			        }
			    }
			}");
		}
	}
}