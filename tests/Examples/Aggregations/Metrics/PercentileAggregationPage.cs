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
	public class PercentileAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-aggregation.asciidoc:28")]
		public void Line28()
		{
			// tag::9baaa0c37e787738507aceee7626c88b[]
			var response0 = new SearchResponse<object>();
			// end::9baaa0c37e787738507aceee7626c88b[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_outlier"" : {
			            ""percentiles"" : {
			                ""field"" : ""load_time"" \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-aggregation.asciidoc:79")]
		public void Line79()
		{
			// tag::4273ecf0448faf65b16952ada3d48a30[]
			var response0 = new SearchResponse<object>();
			// end::4273ecf0448faf65b16952ada3d48a30[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_outlier"" : {
			            ""percentiles"" : {
			                ""field"" : ""load_time"",
			                ""percents"" : [95, 99, 99.9] \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-aggregation.asciidoc:101")]
		public void Line101()
		{
			// tag::e6f49e5325fe0e9b816a837bd3e65a7c[]
			var response0 = new SearchResponse<object>();
			// end::e6f49e5325fe0e9b816a837bd3e65a7c[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""load_time_outlier"": {
			            ""percentiles"": {
			                ""field"": ""load_time"",
			                ""keyed"": false
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-aggregation.asciidoc:169")]
		public void Line169()
		{
			// tag::823b97820ce96abcc3a9292d14292849[]
			var response0 = new SearchResponse<object>();
			// end::823b97820ce96abcc3a9292d14292849[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_outlier"" : {
			            ""percentiles"" : {
			                ""script"" : {
			                    ""lang"": ""painless"",
			                    ""source"": ""doc['load_time'].value / params.timeUnit"", \<1>
			                    ""params"" : {
			                        ""timeUnit"" : 1000   \<2>
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-aggregation.asciidoc:197")]
		public void Line197()
		{
			// tag::dae483a5a412dcf4c20161fea25a87ba[]
			var response0 = new SearchResponse<object>();
			// end::dae483a5a412dcf4c20161fea25a87ba[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_outlier"" : {
			            ""percentiles"" : {
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
		[Description("aggregations/metrics/percentile-aggregation.asciidoc:268")]
		public void Line268()
		{
			// tag::829d345e5e15e371aeb820f4d62a1b2a[]
			var response0 = new SearchResponse<object>();
			// end::829d345e5e15e371aeb820f4d62a1b2a[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_outlier"" : {
			            ""percentiles"" : {
			                ""field"" : ""load_time"",
			                ""tdigest"": {
			                  ""compression"" : 200 \<1>
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-aggregation.asciidoc:320")]
		public void Line320()
		{
			// tag::db17a10cf64c84bd2fc4ebb073e59cec[]
			var response0 = new SearchResponse<object>();
			// end::db17a10cf64c84bd2fc4ebb073e59cec[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_outlier"" : {
			            ""percentiles"" : {
			                ""field"" : ""load_time"",
			                ""percents"" : [95, 99, 99.9],
			                ""hdr"": { \<1>
			                  ""number_of_significant_value_digits"" : 3 \<2>
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-aggregation.asciidoc:352")]
		public void Line352()
		{
			// tag::e557ce02e192939944ebc6bae87e98a6[]
			var response0 = new SearchResponse<object>();
			// end::e557ce02e192939944ebc6bae87e98a6[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""grade_percentiles"" : {
			            ""percentiles"" : {
			                ""field"" : ""grade"",
			                ""missing"": 10 \<1>
			            }
			        }
			    }
			}");
		}
	}
}
