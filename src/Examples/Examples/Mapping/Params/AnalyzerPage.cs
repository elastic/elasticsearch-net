using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class AnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line43()
		{
			// tag::0ae23713026515ec5047c7bbcf9842f7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::0ae23713026515ec5047c7bbcf9842f7[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""text"": { \<1>
			        ""type"": ""text"",
			        ""fields"": {
			          ""english"": { \<2>
			            ""type"":     ""text"",
			            ""analyzer"": ""english""
			          }
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"GET my_index/_analyze \<3>
			{
			  ""field"": ""text"",
			  ""text"": ""The quick Brown Foxes.""
			}");

			response2.MatchesExample(@"GET my_index/_analyze \<4>
			{
			  ""field"": ""text.english"",
			  ""text"": ""The quick Brown Foxes.""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line93()
		{
			// tag::5bf1e4194dce1e15eb7f48fd72b1fc6b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::5bf1e4194dce1e15eb7f48fd72b1fc6b[]

			response0.MatchesExample(@"PUT my_index
			{
			   ""settings"":{
			      ""analysis"":{
			         ""analyzer"":{
			            ""my_analyzer"":{ \<1>
			               ""type"":""custom"",
			               ""tokenizer"":""standard"",
			               ""filter"":[
			                  ""lowercase""
			               ]
			            },
			            ""my_stop_analyzer"":{ \<2>
			               ""type"":""custom"",
			               ""tokenizer"":""standard"",
			               ""filter"":[
			                  ""lowercase"",
			                  ""english_stop""
			               ]
			            }
			         },
			         ""filter"":{
			            ""english_stop"":{
			               ""type"":""stop"",
			               ""stopwords"":""_english_""
			            }
			         }
			      }
			   },
			   ""mappings"":{
			       ""properties"":{
			          ""title"": {
			             ""type"":""text"",
			             ""analyzer"":""my_analyzer"", \<3>
			             ""search_analyzer"":""my_stop_analyzer"", \<4>
			             ""search_quote_analyzer"":""my_analyzer"" \<5>
			         }
			      }
			   }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			   ""title"":""The Quick Brown Fox""
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			   ""title"":""A Quick Brown Fox""
			}");

			response3.MatchesExample(@"GET my_index/_search
			{
			   ""query"":{
			      ""query_string"":{
			         ""query"":""\""the quick brown fox\"""" \<6>
			      }
			   }
			}");
		}
	}
}