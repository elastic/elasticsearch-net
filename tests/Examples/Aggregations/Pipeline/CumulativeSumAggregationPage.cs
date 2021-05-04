// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Pipeline
{
	public class CumulativeSumAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/cumulative-sum-aggregation.asciidoc:35")]
		public void Line35()
		{
			// tag::1ae73d3fcc39bef9ddc654bb82d5d239[]
			var response0 = new SearchResponse<object>();
			// end::1ae73d3fcc39bef9ddc654bb82d5d239[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""sales_per_month"" : {
			            ""date_histogram"" : {
			                ""field"" : ""date"",
			                ""calendar_interval"" : ""month""
			            },
			            ""aggs"": {
			                ""sales"": {
			                    ""sum"": {
			                        ""field"": ""price""
			                    }
			                },
			                ""cumulative_sales"": {
			                    ""cumulative_sum"": {
			                        ""buckets_path"": ""sales"" \<1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
