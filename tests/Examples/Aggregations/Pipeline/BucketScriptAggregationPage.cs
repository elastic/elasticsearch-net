// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Pipeline
{
	public class BucketScriptAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/bucket-script-aggregation.asciidoc:45")]
		public void Line45()
		{
			// tag::c2d90e1c88ff5b1857ed4a5b169c9689[]
			var response0 = new SearchResponse<object>();
			// end::c2d90e1c88ff5b1857ed4a5b169c9689[]

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
			                ""t-shirts"": {
			                  ""filter"": {
			                    ""term"": {
			                      ""type"": ""t-shirt""
			                    }
			                  },
			                  ""aggs"": {
			                    ""sales"": {
			                      ""sum"": {
			                        ""field"": ""price""
			                      }
			                    }
			                  }
			                },
			                ""t-shirt-percentage"": {
			                    ""bucket_script"": {
			                        ""buckets_path"": {
			                          ""tShirtSales"": ""t-shirts>sales"",
			                          ""totalSales"": ""total_sales""
			                        },
			                        ""script"": ""params.tShirtSales / params.totalSales * 100""
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
