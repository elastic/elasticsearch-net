// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class SpanNotQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-not-query.asciidoc:13")]
		public void Line13()
		{
			// tag::4a3b37cdf27279800355ccdef0e13128[]
			var response0 = new SearchResponse<object>();
			// end::4a3b37cdf27279800355ccdef0e13128[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_not"" : {
			            ""include"" : {
			                ""span_term"" : { ""field1"" : ""hoya"" }
			            },
			            ""exclude"" : {
			                ""span_near"" : {
			                    ""clauses"" : [
			                        { ""span_term"" : { ""field1"" : ""la"" } },
			                        { ""span_term"" : { ""field1"" : ""hoya"" } }
			                    ],
			                    ""slop"" : 0,
			                    ""in_order"" : true
			                }
			            }
			        }
			    }
			}");
		}
	}
}
