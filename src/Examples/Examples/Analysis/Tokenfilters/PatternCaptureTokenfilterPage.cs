using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class PatternCaptureTokenfilterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line48()
		{
			// tag::f733b25cd4c448b226bb76862974eef2[]
			var response0 = new SearchResponse<object>();
			// end::f733b25cd4c448b226bb76862974eef2[]

			response0.MatchesExample(@"PUT test
			{
			   ""settings"" : {
			      ""analysis"" : {
			         ""filter"" : {
			            ""code"" : {
			               ""type"" : ""pattern_capture"",
			               ""preserve_original"" : true,
			               ""patterns"" : [
			                  ""(\\p{Ll}+|\\p{Lu}\\p{Ll}+|\\p{Lu}+)"",
			                  ""(\\d+)""
			               ]
			            }
			         },
			         ""analyzer"" : {
			            ""code"" : {
			               ""tokenizer"" : ""pattern"",
			               ""filter"" : [ ""code"", ""lowercase"" ]
			            }
			         }
			      }
			   }
			}");
		}

		[U]
		[SkipExample]
		public void Line89()
		{
			// tag::080c34d8151d02b760571e3a2899fa97[]
			var response0 = new SearchResponse<object>();
			// end::080c34d8151d02b760571e3a2899fa97[]

			response0.MatchesExample(@"PUT test
			{
			   ""settings"" : {
			      ""analysis"" : {
			         ""filter"" : {
			            ""email"" : {
			               ""type"" : ""pattern_capture"",
			               ""preserve_original"" : true,
			               ""patterns"" : [
			                  ""([^@]+)"",
			                  ""(\\p{L}+)"",
			                  ""(\\d+)"",
			                  ""@(.+)""
			               ]
			            }
			         },
			         ""analyzer"" : {
			            ""email"" : {
			               ""tokenizer"" : ""uax_url_email"",
			               ""filter"" : [ ""email"", ""lowercase"",  ""unique"" ]
			            }
			         }
			      }
			   }
			}");
		}
	}
}