// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class GlobalAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/global-aggregation.asciidoc:15")]
		public void Line15()
		{
			// tag::d209f2447584a37e7f1480912b40a52d[]
			var response0 = new SearchResponse<object>();
			// end::d209f2447584a37e7f1480912b40a52d[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""query"" : {
			        ""match"" : { ""type"" : ""t-shirt"" }
			    },
			    ""aggs"" : {
			        ""all_products"" : {
			            ""global"" : {}, \<1>
			            ""aggs"" : { \<2>
			                ""avg_price"" : { ""avg"" : { ""field"" : ""price"" } }
			            }
			        },
			        ""t_shirts"": { ""avg"" : { ""field"" : ""price"" } }
			    }
			}");
		}
	}
}
