// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Pipeline
{
	public class BucketSelectorAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/bucket-selector-aggregation.asciidoc:48")]
		public void Line48()
		{
			// tag::7851d52ed462f0a1bdfd4f676e4a4363[]
			var response0 = new SearchResponse<object>();
			// end::7851d52ed462f0a1bdfd4f676e4a4363[]

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
			                ""total_sales"": {
			                    ""sum"": {
			                        ""field"": ""price""
			                    }
			                },
			                ""sales_bucket_filter"": {
			                    ""bucket_selector"": {
			                        ""buckets_path"": {
			                          ""totalSales"": ""total_sales""
			                        },
			                        ""script"": ""params.totalSales > 200""
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
