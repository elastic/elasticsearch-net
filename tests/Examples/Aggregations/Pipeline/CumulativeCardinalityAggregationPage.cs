using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Pipeline
{
	public class CumulativeCardinalityAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line42()
		{
			// tag::4d8d2c66e4f3ccd760bfe3008c5a4b65[]
			var response0 = new SearchResponse<object>();
			// end::4d8d2c66e4f3ccd760bfe3008c5a4b65[]

			response0.MatchesExample(@"GET /user_hits/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""users_per_day"" : {
			            ""date_histogram"" : {
			                ""field"" : ""timestamp"",
			                ""calendar_interval"" : ""day""
			            },
			            ""aggs"": {
			                ""distinct_users"": {
			                    ""cardinality"": {
			                        ""field"": ""user_id""
			                    }
			                },
			                ""total_new_users"": {
			                    ""cumulative_cardinality"": {
			                        ""buckets_path"": ""distinct_users"" <1>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line141()
		{
			// tag::dd5d84526ecb6a33e96ff2c047b8066d[]
			var response0 = new SearchResponse<object>();
			// end::dd5d84526ecb6a33e96ff2c047b8066d[]

			response0.MatchesExample(@"GET /user_hits/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""users_per_day"" : {
			            ""date_histogram"" : {
			                ""field"" : ""timestamp"",
			                ""calendar_interval"" : ""day""
			            },
			            ""aggs"": {
			                ""distinct_users"": {
			                    ""cardinality"": {
			                        ""field"": ""user_id""
			                    }
			                },
			                ""total_new_users"": {
			                    ""cumulative_cardinality"": {
			                        ""buckets_path"": ""distinct_users""
			                    }
			                },
			                ""incremental_new_users"": {
			                    ""derivative"": {
			                        ""buckets_path"": ""total_new_users""
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}