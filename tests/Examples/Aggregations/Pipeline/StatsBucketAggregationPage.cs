// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Pipeline
{
	public class StatsBucketAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/stats-bucket-aggregation.asciidoc:36")]
		public void Line36()
		{
			// tag::8a187c87964774530ef99f8962d83da5[]
			var response0 = new SearchResponse<object>();
			// end::8a187c87964774530ef99f8962d83da5[]

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
			                }
			            }
			        },
			        ""stats_monthly_sales"": {
			            ""stats_bucket"": {
			                ""buckets_path"": ""sales_per_month>sales"" \<1>
			            }
			        }
			    }
			}");
		}
	}
}
