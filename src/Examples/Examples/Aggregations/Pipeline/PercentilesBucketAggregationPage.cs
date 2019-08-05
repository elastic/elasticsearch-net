using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Pipeline
{
	public class PercentilesBucketAggregationPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line38()
		{
			// tag::cff65c0f9fbc53c26c60abe9fb7e4044[]
			var response0 = new SearchResponse<object>();
			// end::cff65c0f9fbc53c26c60abe9fb7e4044[]

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
			        ""percentiles_monthly_sales"": {
			            ""percentiles_bucket"": {
			                ""buckets_path"": ""sales_per_month>sales"", \<1>
			                ""percents"": [ 25.0, 50.0, 75.0 ] \<2>
			            }
			        }
			    }
			}");
		}
	}
}