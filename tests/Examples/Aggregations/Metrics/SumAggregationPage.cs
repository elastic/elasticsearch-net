// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class SumAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/sum-aggregation.asciidoc:12")]
		public void Line12()
		{
			// tag::43159621ffaa30dbfd60459a5e7b8e54[]
			var response0 = new SearchResponse<object>();
			// end::43159621ffaa30dbfd60459a5e7b8e54[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""query"" : {
			        ""constant_score"" : {
			            ""filter"" : {
			                ""match"" : { ""type"" : ""hat"" }
			            }
			        }
			    },
			    ""aggs"" : {
			        ""hat_prices"" : { ""sum"" : { ""field"" : ""price"" } }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/sum-aggregation.asciidoc:51")]
		public void Line51()
		{
			// tag::4b5f2bd0db1a94614f4d2e46a5159bd2[]
			var response0 = new SearchResponse<object>();
			// end::4b5f2bd0db1a94614f4d2e46a5159bd2[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""query"" : {
			        ""constant_score"" : {
			            ""filter"" : {
			                ""match"" : { ""type"" : ""hat"" }
			            }
			        }
			    },
			    ""aggs"" : {
			        ""hat_prices"" : {
			            ""sum"" : {
			                ""script"" : {
			                   ""source"": ""doc.price.value""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/sum-aggregation.asciidoc:77")]
		public void Line77()
		{
			// tag::49a4032ac0cbc413b47660bcf998ef5f[]
			var response0 = new SearchResponse<object>();
			// end::49a4032ac0cbc413b47660bcf998ef5f[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""query"" : {
			        ""constant_score"" : {
			            ""filter"" : {
			                ""match"" : { ""type"" : ""hat"" }
			            }
			        }
			    },
			    ""aggs"" : {
			        ""hat_prices"" : {
			            ""sum"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"" : {
			                        ""field"" : ""price""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/sum-aggregation.asciidoc:109")]
		public void Line109()
		{
			// tag::82a2031f77972b713f75ed05c4bd9815[]
			var response0 = new SearchResponse<object>();
			// end::82a2031f77972b713f75ed05c4bd9815[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""query"" : {
			        ""constant_score"" : {
			            ""filter"" : {
			                ""match"" : { ""type"" : ""hat"" }
			            }
			        }
			    },
			    ""aggs"" : {
			        ""square_hats"" : {
			            ""sum"" : {
			                ""field"" : ""price"",
			                ""script"" : {
			                    ""source"": ""_value * _value""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/sum-aggregation.asciidoc:141")]
		public void Line141()
		{
			// tag::a78c3f4389502fe2dbd1cd10a017d1ed[]
			var response0 = new SearchResponse<object>();
			// end::a78c3f4389502fe2dbd1cd10a017d1ed[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""query"" : {
			        ""constant_score"" : {
			            ""filter"" : {
			                ""match"" : { ""type"" : ""hat"" }
			            }
			        }
			    },
			    ""aggs"" : {
			        ""hat_prices"" : {
			            ""sum"" : {
			                ""field"" : ""price"",
			                ""missing"": 100 \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/sum-aggregation.asciidoc:172")]
		public void Line172()
		{
			// tag::8db0606ceb96b90711c9e9a1665f3d06[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::8db0606ceb96b90711c9e9a1665f3d06[]

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
			        ""total_latency"" : { ""sum"" : { ""field"" : ""latency_histo"" } }
			    }
			}");
		}
	}
}
