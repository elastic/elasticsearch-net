using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Bucket
{
	public class AutodatehistogramAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line14()
		{
			// tag::9f9123f67baff22429bca73f7cf48622[]
			var response0 = new SearchResponse<object>();
			// end::9f9123f67baff22429bca73f7cf48622[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""auto_date_histogram"" : {
			                ""field"" : ""date"",
			                ""buckets"" : 10
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line41()
		{
			// tag::941466b290eaa9a2685bbe32c73e887a[]
			var response0 = new SearchResponse<object>();
			// end::941466b290eaa9a2685bbe32c73e887a[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sales_over_time"" : {
			            ""auto_date_histogram"" : {
			                ""field"" : ""date"",
			                ""buckets"" : 5,
			                ""format"" : ""yyyy-MM-dd"" \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line123()
		{
			// tag::64b6ca54baf9dba659887051de87440b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::64b6ca54baf9dba659887051de87440b[]

			response0.MatchesExample(@"PUT my_index/log/1?refresh
			{
			  ""date"": ""2015-10-01T00:30:00Z""
			}");

			response1.MatchesExample(@"PUT my_index/log/2?refresh
			{
			  ""date"": ""2015-10-01T01:30:00Z""
			}");

			response2.MatchesExample(@"PUT my_index/log/3?refresh
			{
			  ""date"": ""2015-10-01T02:30:00Z""
			}");

			response3.MatchesExample(@"GET my_index/_search?size=0
			{
			  ""aggs"": {
			    ""by_day"": {
			      ""auto_date_histogram"": {
			        ""field"":     ""date"",
			        ""buckets"" : 3
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line190()
		{
			// tag::e16449c0f4eadb394761e9c2aff50fe6[]
			var response0 = new SearchResponse<object>();
			// end::e16449c0f4eadb394761e9c2aff50fe6[]

			response0.MatchesExample(@"GET my_index/_search?size=0
			{
			  ""aggs"": {
			    ""by_day"": {
			      ""auto_date_histogram"": {
			        ""field"":     ""date"",
			        ""buckets"" : 3,
			        ""time_zone"": ""-01:00""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line277()
		{
			// tag::00abcf63bffec42e5d2c15011e989b37[]
			var response0 = new SearchResponse<object>();
			// end::00abcf63bffec42e5d2c15011e989b37[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sale_date"" : {
			             ""auto_date_histogram"" : {
			                 ""field"" : ""date"",
			                 ""buckets"": 10,
			                 ""minimum_interval"": ""minute""
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line301()
		{
			// tag::89fe7b404791770a2075f2870fd65c3e[]
			var response0 = new SearchResponse<object>();
			// end::89fe7b404791770a2075f2870fd65c3e[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""sale_date"" : {
			             ""auto_date_histogram"" : {
			                 ""field"" : ""date"",
			                 ""buckets"": 10,
			                 ""missing"": ""2000/01/01"" \<1>
			             }
			         }
			    }
			}");
		}
	}
}