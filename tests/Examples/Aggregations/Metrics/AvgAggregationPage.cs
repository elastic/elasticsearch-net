// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class AvgAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/avg-aggregation.asciidoc:10")]
		public void Line10()
		{
			// tag::d9d28e9e9d7021a72c983f8e79aa8c6c[]
			var response0 = new SearchResponse<object>();
			// end::d9d28e9e9d7021a72c983f8e79aa8c6c[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""avg_grade"" : { ""avg"" : { ""field"" : ""grade"" } }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/avg-aggregation.asciidoc:42")]
		public void Line42()
		{
			// tag::d05bbafb8c88850879b5990119a96f5e[]
			var response0 = new SearchResponse<object>();
			// end::d05bbafb8c88850879b5990119a96f5e[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""avg_grade"" : {
			            ""avg"" : {
			                ""script"" : {
			                    ""source"" : ""doc.grade.value""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/avg-aggregation.asciidoc:61")]
		public void Line61()
		{
			// tag::c04f4a48d0cb550a879fdc93454852de[]
			var response0 = new SearchResponse<object>();
			// end::c04f4a48d0cb550a879fdc93454852de[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""avg_grade"" : {
			            ""avg"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"": {
			                        ""field"": ""grade""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/avg-aggregation.asciidoc:85")]
		public void Line85()
		{
			// tag::91994d98e766230911b3e659b3e51f17[]
			var response0 = new SearchResponse<object>();
			// end::91994d98e766230911b3e659b3e51f17[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""avg_corrected_grade"" : {
			            ""avg"" : {
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
		[Description("aggregations/metrics/avg-aggregation.asciidoc:113")]
		public void Line113()
		{
			// tag::2ec33e09d6080723ee2013bad694f35a[]
			var response0 = new SearchResponse<object>();
			// end::2ec33e09d6080723ee2013bad694f35a[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grade_avg"" : {
			            ""avg"" : {
			                ""field"" : ""grade"",
			                ""missing"": 10 \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/avg-aggregation.asciidoc:139")]
		public void Line139()
		{
			// tag::da2382a59a4200333accd75be74c6136[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::da2382a59a4200333accd75be74c6136[]

			response0.MatchesExample(@"PUT metrics_index/_doc/1
			{
			  ""network.name"" : ""net-1"",
			  ""latency_histo"" : {
			      ""values"" : [0.1, 0.2, 0.3, 0.4, 0.5], <1>
			      ""counts"" : [3, 7, 23, 12, 6] <2>
			   }
			}");

			response1.MatchesExample(@"PUT metrics_index/_doc/2
			{
			  ""network.name"" : ""net-2"",
			  ""latency_histo"" : {
			      ""values"" :  [0.1, 0.2, 0.3, 0.4, 0.5], <1>
			      ""counts"" : [8, 17, 8, 7, 6] <2>
			   }
			}");

			response2.MatchesExample(@"POST /metrics_index/_search?size=0
			{
			    ""aggs"" : {
			        ""avg_latency"" :
			            { ""avg"" : { ""field"" : ""latency_histo"" }
			        }
			    }
			}");
		}
	}
}
