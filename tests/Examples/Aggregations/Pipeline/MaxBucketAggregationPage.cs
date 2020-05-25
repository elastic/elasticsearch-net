// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Pipeline
{
	public class MaxBucketAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/max-bucket-aggregation.asciidoc:37")]
		public void Line37()
		{
			// tag::ce5d556d90d0fb077ab078e055005f3a[]
			var response0 = new SearchResponse<object>();
			// end::ce5d556d90d0fb077ab078e055005f3a[]

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
			        ""max_monthly_sales"": {
			            ""max_bucket"": {
			                ""buckets_path"": ""sales_per_month>sales"" \<1>
			            }
			        }
			    }
			}");
		}
	}
}
