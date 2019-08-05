using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class ElisionTokenfilterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line15()
		{
			// tag::c0fa4f18231d7495c39b62bb4e56fe50[]
			var response0 = new SearchResponse<object>();
			// end::c0fa4f18231d7495c39b62bb4e56fe50[]

			response0.MatchesExample(@"PUT /elision_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""default"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""elision""]
			                }
			            },
			            ""filter"" : {
			                ""elision"" : {
			                    ""type"" : ""elision"",
			                    ""articles_case"": true,
			                    ""articles"" : [""l"", ""m"", ""t"", ""qu"", ""n"", ""s"", ""j""]
			                }
			            }
			        }
			    }
			}");
		}
	}
}