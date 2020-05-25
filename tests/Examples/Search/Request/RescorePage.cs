// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class RescorePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/request/rescore.asciidoc:43")]
		public void Line43()
		{
			// tag::829a40d484c778a8c58340c7bf09e1d8[]
			var response0 = new SearchResponse<object>();
			// end::829a40d484c778a8c58340c7bf09e1d8[]

			response0.MatchesExample(@"POST /_search
			{
			   ""query"" : {
			      ""match"" : {
			         ""message"" : {
			            ""operator"" : ""or"",
			            ""query"" : ""the quick brown""
			         }
			      }
			   },
			   ""rescore"" : {
			      ""window_size"" : 50,
			      ""query"" : {
			         ""rescore_query"" : {
			            ""match_phrase"" : {
			               ""message"" : {
			                  ""query"" : ""the quick brown"",
			                  ""slop"" : 2
			               }
			            }
			         },
			         ""query_weight"" : 0.7,
			         ""rescore_query_weight"" : 1.2
			      }
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/request/rescore.asciidoc:90")]
		public void Line90()
		{
			// tag::7d7855afd9882a665bbabda810f94f61[]
			var response0 = new SearchResponse<object>();
			// end::7d7855afd9882a665bbabda810f94f61[]

			response0.MatchesExample(@"POST /_search
			{
			   ""query"" : {
			      ""match"" : {
			         ""message"" : {
			            ""operator"" : ""or"",
			            ""query"" : ""the quick brown""
			         }
			      }
			   },
			   ""rescore"" : [ {
			      ""window_size"" : 100,
			      ""query"" : {
			         ""rescore_query"" : {
			            ""match_phrase"" : {
			               ""message"" : {
			                  ""query"" : ""the quick brown"",
			                  ""slop"" : 2
			               }
			            }
			         },
			         ""query_weight"" : 0.7,
			         ""rescore_query_weight"" : 1.2
			      }
			   }, {
			      ""window_size"" : 10,
			      ""query"" : {
			         ""score_mode"": ""multiply"",
			         ""rescore_query"" : {
			            ""function_score"" : {
			               ""script_score"": {
			                  ""script"": {
			                    ""source"": ""Math.log10(doc.likes.value + 2)""
			                  }
			               }
			            }
			         }
			      }
			   } ]
			}");
		}
	}
}
