// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class ScriptQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/script-query.asciidoc:15")]
		public void Line15()
		{
			// tag::b3aa46565d98f8a6750c571bb1c1bb8c[]
			var response0 = new SearchResponse<object>();
			// end::b3aa46565d98f8a6750c571bb1c1bb8c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""filter"" : {
			                ""script"" : {
			                    ""script"" : {
			                        ""source"": ""doc['num1'].value > 1"",
			                        ""lang"": ""painless""
			                     }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/script-query.asciidoc:52")]
		public void Line52()
		{
			// tag::c4459f98de5decb37b8c403885f4b226[]
			var response0 = new SearchResponse<object>();
			// end::c4459f98de5decb37b8c403885f4b226[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""filter"" : {
			                ""script"" : {
			                    ""script"" : {
			                        ""source"" : ""doc['num1'].value > params.param1"",
			                        ""lang""   : ""painless"",
			                        ""params"" : {
			                            ""param1"" : 5
			                        }
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
