using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class StopTokenfilterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line35()
		{
			// tag::a4575521493d8d1fbdd450821035f821[]
			var response0 = new SearchResponse<object>();
			// end::a4575521493d8d1fbdd450821035f821[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""settings"": {
			        ""analysis"": {
			            ""filter"": {
			                ""my_stop"": {
			                    ""type"":       ""stop"",
			                    ""stopwords"": [""and"", ""is"", ""the""]
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line55()
		{
			// tag::ded95888c2d68c48426b013284eb896a[]
			var response0 = new SearchResponse<object>();
			// end::ded95888c2d68c48426b013284eb896a[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""settings"": {
			        ""analysis"": {
			            ""filter"": {
			                ""my_stop"": {
			                    ""type"":       ""stop"",
			                    ""stopwords"":  ""_english_""
			                }
			            }
			        }
			    }
			}");
		}
	}
}