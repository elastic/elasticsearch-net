using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class KeepWordsTokenfilterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line22()
		{
			// tag::44cb20732770bb9a5f114a7517db774f[]
			var response0 = new SearchResponse<object>();
			// end::44cb20732770bb9a5f114a7517db774f[]

			response0.MatchesExample(@"PUT /keep_words_example
			{
			    ""settings"" : {
			        ""analysis"" : {
			            ""analyzer"" : {
			                ""example_1"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""lowercase"", ""words_till_three""]
			                },
			                ""example_2"" : {
			                    ""tokenizer"" : ""standard"",
			                    ""filter"" : [""lowercase"", ""words_in_file""]
			                }
			            },
			            ""filter"" : {
			                ""words_till_three"" : {
			                    ""type"" : ""keep"",
			                    ""keep_words"" : [ ""one"", ""two"", ""three""]
			                },
			                ""words_in_file"" : {
			                    ""type"" : ""keep"",
			                    ""keep_words_path"" : ""analysis/example_word_list.txt""
			                }
			            }
			        }
			    }
			}");
		}
	}
}