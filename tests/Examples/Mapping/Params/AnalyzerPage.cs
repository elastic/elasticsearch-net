// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Params
{
	public class AnalyzerPage : ExampleBase
	{

		[U(Skip = "Example not implemented")]
		[Description("mapping/params/analyzer.asciidoc:35")]
		public void Line35()
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
