// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class BoostingQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/boosting-query.asciidoc:18")]
		public void Line18()
		{
			// tag::292e4c6567378fc7b70033b53b04ce12[]
			var response0 = new SearchResponse<object>();
			// end::292e4c6567378fc7b70033b53b04ce12[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""boosting"" : {
			            ""positive"" : {
			                ""term"" : {
			                    ""text"" : ""apple""
			                }
			            },
			            ""negative"" : {
			                 ""term"" : {
			                     ""text"" : ""pie tart fruit crumble tree""
			                }
			            },
			            ""negative_boost"" : 0.5
			        }
			    }
			}");
		}
	}
}
