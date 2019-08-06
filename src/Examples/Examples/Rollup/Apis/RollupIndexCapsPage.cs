using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Rollup.Apis
{
	public class RollupIndexCapsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line46()
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
		public void Line82()
		{
			// tag::73d1a6c5ef90b7e35d43a0bfdc1e158d[]
			var response0 = new SearchResponse<object>();
			// end::73d1a6c5ef90b7e35d43a0bfdc1e158d[]

			response0.MatchesExample(@"GET /sensor_rollup/_rollup/data");
		}

		[U(Skip = "Example not implemented")]
		public void Line155()
		{
			// tag::642161d70dacf7d153767d37d3726838[]
			var response0 = new SearchResponse<object>();
			// end::642161d70dacf7d153767d37d3726838[]

			response0.MatchesExample(@"GET /*_rollup/_rollup/data");
		}
	}
}