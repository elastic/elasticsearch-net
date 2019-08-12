using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Analyzers
{
	public class CustomAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line55()
		{
			// tag::ef2ea91fb3fa26c740bca994af85e150[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::ef2ea91fb3fa26c740bca994af85e150[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_custom_analyzer"": {
			          ""type"":      ""custom"", \<1>
			          ""tokenizer"": ""standard"",
			          ""char_filter"": [
			            ""html_strip""
			          ],
			          ""filter"": [
			            ""lowercase"",
			            ""asciifolding""
			          ]
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_custom_analyzer"",
			  ""text"": ""Is this \<b>déjà vu</b>?""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line159()
		{
			// tag::c729a5ef7a671154bb82e308d915cf9f[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::c729a5ef7a671154bb82e308d915cf9f[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_custom_analyzer"": {
			          ""type"": ""custom"",
			          ""char_filter"": [
			            ""emoticons"" \<1>
			          ],
			          ""tokenizer"": ""punctuation"", \<1>
			          ""filter"": [
			            ""lowercase"",
			            ""english_stop"" \<1>
			          ]
			        }
			      },
			      ""tokenizer"": {
			        ""punctuation"": { \<1>
			          ""type"": ""pattern"",
			          ""pattern"": ""[ .,!?]""
			        }
			      },
			      ""char_filter"": {
			        ""emoticons"": { \<1>
			          ""type"": ""mapping"",
			          ""mappings"": [
			            "":) => _happy_"",
			            "":( => _sad_""
			          ]
			        }
			      },
			      ""filter"": {
			        ""english_stop"": { \<1>
			          ""type"": ""stop"",
			          ""stopwords"": ""_english_""
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_custom_analyzer"",
			  ""text"":     ""I'm a :) person, and you?""
			}");
		}
	}
}