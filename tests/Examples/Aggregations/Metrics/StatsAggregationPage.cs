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
	public class StatsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/stats-aggregation.asciidoc:11")]
		public void Line11()
		{
			// tag::6f04f3c1afe94e03d26ff5966fd4b98d[]
			var response0 = new SearchResponse<object>();
			// end::6f04f3c1afe94e03d26ff5966fd4b98d[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grades_stats"" : { ""stats"" : { ""field"" : ""grade"" } }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/stats-aggregation.asciidoc:49")]
		public void Line49()
		{
			// tag::9ed80262680e67c629a08f6754a7c5c9[]
			var response0 = new SearchResponse<object>();
			// end::9ed80262680e67c629a08f6754a7c5c9[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grades_stats"" : {
			             ""stats"" : {
			                 ""script"" : {
			                     ""lang"": ""painless"",
			                     ""source"": ""doc['grade'].value""
			                 }
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/stats-aggregation.asciidoc:69")]
		public void Line69()
		{
			// tag::2ba8575100b37b85d0052d46a00ce4cd[]
			var response0 = new SearchResponse<object>();
			// end::2ba8575100b37b85d0052d46a00ce4cd[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""stats"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"" : {
			                        ""field"" : ""grade""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/stats-aggregation.asciidoc:93")]
		public void Line93()
		{
			// tag::1341888a2677cf6e1db11e6cab2dd8ce[]
			var response0 = new SearchResponse<object>();
			// end::1341888a2677cf6e1db11e6cab2dd8ce[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""stats"" : {
			                ""field"" : ""grade"",
			                ""script"" : {
			                    ""lang"": ""painless"",
			                    ""source"": ""_value * params.correction"",
			                    ""params"" : {
			                        ""correction"" : 1.2
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/stats-aggregation.asciidoc:121")]
		public void Line121()
		{
			// tag::7371dcfe4adb43996f4c26684318302b[]
			var response0 = new SearchResponse<object>();
			// end::7371dcfe4adb43996f4c26684318302b[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""stats"" : {
			                ""field"" : ""grade"",
			                ""missing"": 0 \<1>
			            }
			        }
			    }
			}");
		}
	}
}
