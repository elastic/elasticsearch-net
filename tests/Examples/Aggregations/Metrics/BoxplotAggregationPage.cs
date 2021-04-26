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

namespace Examples.Aggregations.Metrics
{
	public class BoxplotAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/boxplot-aggregation.asciidoc:30")]
		public void Line30()
		{
			// tag::2203588f4793e0e99ccd9240b5afdff7[]
			var response0 = new SearchResponse<object>();
			// end::2203588f4793e0e99ccd9240b5afdff7[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_boxplot"" : {
			            ""boxplot"" : {
			                ""field"" : ""load_time"" <1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/boxplot-aggregation.asciidoc:73")]
		public void Line73()
		{
			// tag::c2d1756ceca8fdc40a2b97ea275de676[]
			var response0 = new SearchResponse<object>();
			// end::c2d1756ceca8fdc40a2b97ea275de676[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_boxplot"" : {
			            ""boxplot"" : {
			                ""script"" : {
			                    ""lang"": ""painless"",
			                    ""source"": ""doc['load_time'].value / params.timeUnit"", <1>
			                    ""params"" : {
			                        ""timeUnit"" : 1000   <2>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/boxplot-aggregation.asciidoc:102")]
		public void Line102()
		{
			// tag::ae2331b7e35af4bbc4df7b98f2527c7f[]
			var response0 = new SearchResponse<object>();
			// end::ae2331b7e35af4bbc4df7b98f2527c7f[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_boxplot"" : {
			            ""boxplot"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"": {
			                        ""field"": ""load_time""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/boxplot-aggregation.asciidoc:143")]
		public void Line143()
		{
			// tag::195af3fda6cb2811d40e22fc54cd3286[]
			var response0 = new SearchResponse<object>();
			// end::195af3fda6cb2811d40e22fc54cd3286[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_boxplot"" : {
			            ""boxplot"" : {
			                ""field"" : ""load_time"",
			                ""compression"" : 200 <1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/boxplot-aggregation.asciidoc:170")]
		public void Line170()
		{
			// tag::71387a61dd965479b767c8dcea1478a9[]
			var response0 = new SearchResponse<object>();
			// end::71387a61dd965479b767c8dcea1478a9[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""grade_boxplot"" : {
			            ""boxplot"" : {
			                ""field"" : ""grade"",
			                ""missing"": 10 <1>
			            }
			        }
			    }
			}");
		}
	}
}
