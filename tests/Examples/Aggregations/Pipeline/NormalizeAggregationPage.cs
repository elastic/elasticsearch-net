// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Aggregations.Pipeline
{
	public class NormalizeAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/pipeline/normalize-aggregation.asciidoc:91")]
		public void Line91()
		{
			// tag::8ea49742567a22f1cb57f895b3880e6a[]
			var response0 = new SearchResponse<object>();
			// end::8ea49742567a22f1cb57f895b3880e6a[]

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
			                ""percent_of_total_sales"": {
			                    ""normalize"": {
			                        ""buckets_path"": ""sales"", <1>
			                        ""method"": ""percent_of_sum"",  <2>
			                        ""format"": ""00.00%"" <3>
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}