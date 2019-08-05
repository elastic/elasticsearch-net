using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Pipeline
{
	public class AvgBucketAggregationPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line37()
		{
			// tag::b3e8697874ed65ed6cb62f2568bcc55e[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::b3e8697874ed65ed6cb62f2568bcc55e[]

			response0.MatchesExample(@"POST /_search
			{
			  ""size"": 0,
			  ""aggs"": {
			    ""sales_per_month"": {
			      ""date_histogram"": {
			        ""field"": ""date"",
			        ""calendar_interval"": ""month""
			      },
			      ""aggs"": {
			        ""sales"": {
			          ""sum"": {
			            ""field"": ""price""
			          }
			        }
			      }
			    },
			    ""avg_monthly_sales"": {
			      ""avg_bucket"": {
			        ""buckets_path"": ""sales_per_month>sales"" \<1>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"");
		}
	}
}