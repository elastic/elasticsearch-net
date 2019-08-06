using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class KeepTypesTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line21()
		{
			// tag::928923befcb84cdcace229b027fd281f[]
			var response0 = new SearchResponse<object>();
			// end::928923befcb84cdcace229b027fd281f[]

			response0.MatchesExample(@"PUT /keep_types_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""lowercase"", ""extract_numbers""]
			                }
			            },
			            ""filter"" : {
			                ""extract_numbers"" : {
			                    ""type"" : ""keep_types"",
			                    ""types"" : [ ""<NUM>"" ]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line47()
		{
			// tag::b425b2194294437ac21df0b5606fb3d2[]
			var response0 = new SearchResponse<object>();
			// end::b425b2194294437ac21df0b5606fb3d2[]

			response0.MatchesExample(@"POST /keep_types_example/_analyze
			{
			  ""analyzer"" : ""my_analyzer"",
			  ""text"" : ""this is just 1 a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line82()
		{
			// tag::1658d704f26a06e8f37c6430361c3f26[]
			var response0 = new SearchResponse<object>();
			// end::1658d704f26a06e8f37c6430361c3f26[]

			response0.MatchesExample(@"PUT /keep_types_exclude_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""my_analyzer"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""lowercase"", ""remove_numbers""]
			                }
			            },
			            ""filter"" : {
			                ""remove_numbers"" : {
			                    ""type"" : ""keep_types"",
			                    ""mode"" : ""exclude"",
			                    ""types"" : [ ""<NUM>"" ]
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line109()
		{
			// tag::4d5ded2eede9a987df094dc4a91893d7[]
			var response0 = new SearchResponse<object>();
			// end::4d5ded2eede9a987df094dc4a91893d7[]

			response0.MatchesExample(@"POST /keep_types_exclude_example/_analyze
			{
			  ""analyzer"" : ""my_analyzer"",
			  ""text"" : ""hello 101 world""
			}");
		}
	}
}