// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class SpanContainingQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-containing-query.asciidoc:11")]
		public void Line11()
		{
			// tag::73094e82ce3850cbb6f9d071cc8a2d14[]
			var response0 = new SearchResponse<object>();
			// end::73094e82ce3850cbb6f9d071cc8a2d14[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_containing"" : {
			            ""little"" : {
			                ""span_term"" : { ""field1"" : ""foo"" }
			            },
			            ""big"" : {
			                ""span_near"" : {
			                    ""clauses"" : [
			                        { ""span_term"" : { ""field1"" : ""bar"" } },
			                        { ""span_term"" : { ""field1"" : ""baz"" } }
			                    ],
			                    ""slop"" : 5,
			                    ""in_order"" : true
			                }
			            }
			        }
			    }
			}");
		}
	}
}
