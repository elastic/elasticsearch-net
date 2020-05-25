// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Pipeline
{
	public class MinBucketAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/min-bucket-aggregation.asciidoc:37")]
		public void Line37()
		{
			// tag::e668549ff72fd0b9568667d1a817fc6e[]
			var response0 = new SearchResponse<object>();
			// end::e668549ff72fd0b9568667d1a817fc6e[]

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
			        ""min_monthly_sales"": {
			            ""min_bucket"": {
			                ""buckets_path"": ""sales_per_month>sales"" \<1>
			            }
			        }
			    }
			}");
		}
	}
}
