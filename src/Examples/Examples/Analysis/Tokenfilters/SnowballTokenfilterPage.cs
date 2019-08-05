using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class SnowballTokenfilterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line14()
		{
			// tag::e776311ef67c972f322b669dc4ab9926[]
			var response0 = new SearchResponse<object>();
			// end::e776311ef67c972f322b669dc4ab9926[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""settings"": {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""lowercase"", ""my_snow""]
			                }
			            },
			            ""filter"" : {
			                ""my_snow"" : {
			                    ""type"" : ""snowball"",
			                    ""language"" : ""Lovins""
			                }
			            }
			        }
			    }
			}");
		}
	}
}