using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Matrix
{
	public class StatsAggregationPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line39()
		{
			// tag::8ab89e635fcbc485d1728c13dfeeb1ae[]
			var response0 = new SearchResponse<object>();
			// end::8ab89e635fcbc485d1728c13dfeeb1ae[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"": {
			        ""statistics"": {
			            ""matrix_stats"": {
			                ""fields"": [""poverty"", ""income""]
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line123()
		{
			// tag::7ee2877f8f031b9a4e56a40b371421fb[]
			var response0 = new SearchResponse<object>();
			// end::7ee2877f8f031b9a4e56a40b371421fb[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"": {
			        ""matrixstats"": {
			            ""matrix_stats"": {
			                ""fields"": [""poverty"", ""income""],
			                ""missing"": {""income"" : 50000} \<1>
			            }
			        }
			    }
			}");
		}
	}
}