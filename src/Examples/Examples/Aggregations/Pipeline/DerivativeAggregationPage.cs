using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Pipeline
{
	public class DerivativeAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line38()
		{
			// tag::469bc2e7b9e65b3b1e38a547f63bd2f9[]
			var response0 = new SearchResponse<object>();
			// end::469bc2e7b9e65b3b1e38a547f63bd2f9[]

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
			                ""sales_deriv"": {
			                    ""derivative"": {
			                        ""buckets_path"": ""sales"" \<1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line132()
		{
			// tag::d683ed8c4a72f82200bbad0c3921e427[]
			var response0 = new SearchResponse<object>();
			// end::d683ed8c4a72f82200bbad0c3921e427[]

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
			                ""sales_deriv"": {
			                    ""derivative"": {
			                        ""buckets_path"": ""sales""
			                    }
			                },
			                ""sales_2nd_deriv"": {
			                    ""derivative"": {
			                        ""buckets_path"": ""sales_deriv"" \<1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line232()
		{
			// tag::8553b0c396e9de7d841fcc6373e017e2[]
			var response0 = new SearchResponse<object>();
			// end::8553b0c396e9de7d841fcc6373e017e2[]

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
			                ""sales_deriv"": {
			                    ""derivative"": {
			                        ""buckets_path"": ""sales"",
			                        ""unit"": ""day"" \<1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}