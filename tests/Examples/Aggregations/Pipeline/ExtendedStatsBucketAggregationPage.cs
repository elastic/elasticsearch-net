using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Pipeline
{
	public class ExtendedStatsBucketAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line39()
		{
			// tag::b8f960415d10545f583d2eac94e07629[]
			var response0 = new SearchResponse<object>();
			// end::b8f960415d10545f583d2eac94e07629[]

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
			            ""extended_stats_bucket"": {
			                ""buckets_path"": ""sales_per_month>sales"" \<1>
			            }
			        }
			    }
			}");
		}
	}
}