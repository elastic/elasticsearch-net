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

namespace Examples.Rollup
{
	public class RollupGettingStartedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rollup/rollup-getting-started.asciidoc:35")]
		public void Line35()
		{
			// tag::3acad8c67832b281b9f15349492b8328[]
			var response0 = new SearchResponse<object>();
			// end::3acad8c67832b281b9f15349492b8328[]

			response0.MatchesExample(@"PUT _rollup/job/sensor
			{
			    ""index_pattern"": ""sensor-*"",
			    ""rollup_index"": ""sensor_rollup"",
			    ""cron"": ""*/30 * * * * ?"",
			    ""page_size"" :1000,
			    ""groups"" : {
			      ""date_histogram"": {
			        ""field"": ""timestamp"",
			        ""fixed_interval"": ""60m""
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
		[Description("rollup/rollup-getting-started.asciidoc:139")]
		public void Line139()
		{
			// tag::618c9d42284c067891fb57034a4fd834[]
			var response0 = new SearchResponse<object>();
			// end::618c9d42284c067891fb57034a4fd834[]

			response0.MatchesExample(@"POST _rollup/job/sensor/_start");
		}

		[U(Skip = "Example not implemented")]
		[Description("rollup/rollup-getting-started.asciidoc:153")]
		public void Line153()
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
		[Description("rollup/rollup-getting-started.asciidoc:210")]
		public void Line210()
		{
			// tag::e0f8ecc665f547d5365699ab8773e298[]
			var response0 = new SearchResponse<object>();
			// end::e0f8ecc665f547d5365699ab8773e298[]

			response0.MatchesExample(@"GET /sensor_rollup/_rollup_search
			{
			    ""size"": 0,
			    ""aggregations"": {
			        ""timeline"": {
			            ""date_histogram"": {
			                ""field"": ""timestamp"",
			                ""fixed_interval"": ""7d""
			            },
			            ""aggs"": {
			                ""nodes"": {
			                    ""terms"": {
			                        ""field"": ""node""
			                    },
			                    ""aggs"": {
			                        ""max_temperature"": {
			                            ""max"": {
			                                ""field"": ""temperature""
			                            }
			                        },
			                        ""avg_voltage"": {
			                            ""avg"": {
			                                ""field"": ""voltage""
			                            }
			                        }
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
