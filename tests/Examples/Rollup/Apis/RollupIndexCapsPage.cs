// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Rollup.Apis
{
	public class RollupIndexCapsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rollup/apis/rollup-index-caps.asciidoc:53")]
		public void Line53()
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
		[Description("rollup/apis/rollup-index-caps.asciidoc:88")]
		public void Line88()
		{
			// tag::73d1a6c5ef90b7e35d43a0bfdc1e158d[]
			var response0 = new SearchResponse<object>();
			// end::73d1a6c5ef90b7e35d43a0bfdc1e158d[]

			response0.MatchesExample(@"GET /sensor_rollup/_rollup/data");
		}

		[U(Skip = "Example not implemented")]
		[Description("rollup/apis/rollup-index-caps.asciidoc:163")]
		public void Line163()
		{
			// tag::642161d70dacf7d153767d37d3726838[]
			var response0 = new SearchResponse<object>();
			// end::642161d70dacf7d153767d37d3726838[]

			response0.MatchesExample(@"GET /*_rollup/_rollup/data");
		}
	}
}
