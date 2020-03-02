using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class StopTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line38()
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

		[U(Skip = "Example not implemented")]
		public void Line57()
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