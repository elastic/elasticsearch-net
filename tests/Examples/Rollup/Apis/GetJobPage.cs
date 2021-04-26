/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Rollup.Apis
{
	public class GetJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rollup/apis/get-job.asciidoc:89")]
		public void Line89()
		{
			// tag::d095b422d9803c02b62c01adffc85376[]
			var response0 = new SearchResponse<object>();
			// end::d095b422d9803c02b62c01adffc85376[]

			response0.MatchesExample(@"GET _rollup/job/sensor");
		}

		[U(Skip = "Example not implemented")]
		[Description("rollup/apis/get-job.asciidoc:164")]
		public void Line164()
		{
			// tag::6d13e0721a7aac00adcdc5fe77198300[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::6d13e0721a7aac00adcdc5fe77198300[]

			response0.MatchesExample(@"PUT _rollup/job/sensor2 \<1>
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

			response1.MatchesExample(@"GET _rollup/job/_all \<2>");
		}
	}
}
