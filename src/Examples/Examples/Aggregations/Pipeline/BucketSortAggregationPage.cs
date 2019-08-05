using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Pipeline
{
	public class BucketSortAggregationPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line51()
		{
			// tag::7881659b181997486731d92712fbdca9[]
			var response0 = new SearchResponse<object>();
			// end::7881659b181997486731d92712fbdca9[]

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
			                ""sales_bucket_sort"": {
			                    ""bucket_sort"": {
			                        ""sort"": [
			                          {""total_sales"": {""order"": ""desc""}}\<1>
			                        ],
			                        ""size"": 3\<2>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line139()
		{
			// tag::541c4f4fb5959cf88423196e51c7e0ef[]
			var response0 = new SearchResponse<object>();
			// end::541c4f4fb5959cf88423196e51c7e0ef[]

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
			                ""bucket_truncate"": {
			                    ""bucket_sort"": {
			                        ""from"": 1,
			                        ""size"": 1
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}