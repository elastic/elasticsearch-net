using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class StemmerTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line14()
		{
			// tag::1ca618e7d72ec73c1064fa6eae3086d1[]
			var response0 = new SearchResponse<object>();
			// end::1ca618e7d72ec73c1064fa6eae3086d1[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""settings"": {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""lowercase"", ""my_stemmer""]
			                }
			            },
			            ""filter"" : {
			                ""my_stemmer"" : {
			                    ""type"" : ""stemmer"",
			                    ""name"" : ""light_german""
			                }
			            }
			        }
			    }
			}");
		}
	}
}