// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class SpanFieldMaskingQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/span-field-masking-query.asciidoc:16")]
		public void Line16()
		{
			// tag::b59861ad84352fee3e78bc869ccbe8b0[]
			var response0 = new SearchResponse<object>();
			// end::b59861ad84352fee3e78bc869ccbe8b0[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": {
			    ""span_near"": {
			      ""clauses"": [
			        {
			          ""span_term"": {
			            ""text"": ""quick brown""
			          }
			        },
			        {
			          ""field_masking_span"": {
			            ""query"": {
			              ""span_term"": {
			                ""text.stems"": ""fox""
			              }
			            },
			            ""field"": ""text""
			          }
			        }
			      ],
			      ""slop"": 5,
			      ""in_order"": false
			    }
			  }
			}");
		}
	}
}
