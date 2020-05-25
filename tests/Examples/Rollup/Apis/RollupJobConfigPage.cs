// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Rollup.Apis
{
	public class RollupJobConfigPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line17()
		{
			// tag::1cf5e58ea0f2ca39abfee4361207b939[]
			var response0 = new SearchResponse<object>();
			// end::1cf5e58ea0f2ca39abfee4361207b939[]

			response0.MatchesExample(@"PUT _rollup/job/sensor
			{
			    ""index_pattern"": ""sensor-*"",
			    ""rollup_index"": ""sensor_rollup"",
			    ""cron"": ""*/30 * * * * ?"",
			    ""page_size"" :1000,
			    ""groups"" : {
			      ""date_histogram"": {
			        ""field"": ""timestamp"",
			        ""fixed_interval"": ""60m"",
			        ""delay"": ""7d""
			      },
			      ""terms"": {
			        ""fields"": [""hostname"", ""datacenter""]
			      },
			      ""histogram"": {
			        ""fields"": [""load"", ""net_in"", ""net_out""],
			        ""interval"": 5
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
	}
}
