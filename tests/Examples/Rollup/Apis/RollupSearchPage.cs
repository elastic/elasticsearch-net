using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Rollup.Apis
{
	public class RollupSearchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line71()
		{
			// tag::2d20c42e9664febeccaff61581605cbe[]
			var response0 = new SearchResponse<object>();
			// end::2d20c42e9664febeccaff61581605cbe[]

			response0.MatchesExample(@"PUT _rollup/job/sensor
			{
			    ""index_pattern"": ""sensor-*"",
			    ""rollup_index"": ""sensor_rollup"",
			    ""cron"": ""*/30 * * * * ?"",
			    ""page_size"" :1000,
			    ""groups"" : {
			      ""date_histogram"": {
			        ""field"": ""timestamp"",
			        ""fixed_interval"": ""1h"",
			        ""delay"": ""7d""
			      },
			      ""terms"": {
			        ""fields"": [""node""]
			      }
			    },
			    ""metrics"": [
			        {
			            ""field"": ""temperature"",
			            ""metrics"": [""min"", ""max"", ""sum""]
			        },
			        {
			            ""field"": ""voltage"",
			            ""metrics"": [""avg""]
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line108()
		{
			// tag::4e63a0fd56cc5d59595baa0b0721f971[]
			var response0 = new SearchResponse<object>();
			// end::4e63a0fd56cc5d59595baa0b0721f971[]

			response0.MatchesExample(@"GET /sensor_rollup/_rollup_search
			{
			    ""size"": 0,
			    ""aggregations"": {
			        ""max_temperature"": {
			            ""max"": {
			                ""field"": ""temperature""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line163()
		{
			// tag::3d1cea1ad861d1ee62e5f34b84371943[]
			var response0 = new SearchResponse<object>();
			// end::3d1cea1ad861d1ee62e5f34b84371943[]

			response0.MatchesExample(@"GET sensor_rollup/_rollup_search
			{
			    ""size"": 0,
			    ""aggregations"": {
			        ""avg_temperature"": {
			            ""avg"": {
			                ""field"": ""temperature""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line206()
		{
			// tag::adcd760ef029f744ab59460818d2342e[]
			var response0 = new SearchResponse<object>();
			// end::adcd760ef029f744ab59460818d2342e[]

			response0.MatchesExample(@"GET sensor-1,sensor_rollup/_rollup_search \<1>
			{
			    ""size"": 0,
			    ""aggregations"": {
			        ""max_temperature"": {
			            ""max"": {
			                ""field"": ""temperature""
			            }
			        }
			    }
			}");
		}
	}
}