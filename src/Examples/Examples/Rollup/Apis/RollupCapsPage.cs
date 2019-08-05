using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Rollup.Apis
{
	public class RollupCapsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line55()
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

		[U]
		[SkipExample]
		public void Line90()
		{
			// tag::a00311843b5f8f3e9f7d511334a828b1[]
			var response0 = new SearchResponse<object>();
			// end::a00311843b5f8f3e9f7d511334a828b1[]

			response0.MatchesExample(@"GET _rollup/data/sensor-*");
		}

		[U]
		[SkipExample]
		public void Line160()
		{
			// tag::944806221eb89f5af2298ccdf2902277[]
			var response0 = new SearchResponse<object>();
			// end::944806221eb89f5af2298ccdf2902277[]

			response0.MatchesExample(@"GET _rollup/data/_all");
		}

		[U]
		[SkipExample]
		public void Line169()
		{
			// tag::f8cb1a04c2e487ff006b5ae0e1a7afbd[]
			var response0 = new SearchResponse<object>();
			// end::f8cb1a04c2e487ff006b5ae0e1a7afbd[]

			response0.MatchesExample(@"GET _rollup/data/sensor-1");
		}
	}
}