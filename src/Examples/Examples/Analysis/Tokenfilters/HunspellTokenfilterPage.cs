using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class HunspellTokenfilterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line44()
		{
			// tag::0af002734dd884f9385da6c3a4ca87a1[]
			var response0 = new SearchResponse<object>();
			// end::0af002734dd884f9385da6c3a4ca87a1[]

			response0.MatchesExample(@"PUT /hunspell_example
			{
			    ""settings"": {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""en"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [ ""lowercase"", ""en_US"" ]
			                }
			            },
			            ""filter"" : {
			                ""en_US"" : {
			                    ""type"" : ""hunspell"",
			                    ""locale"" : ""en_US"",
			                    ""dedup"" : true
			                }
			            }
			        }
			    }
			}");
		}
	}
}