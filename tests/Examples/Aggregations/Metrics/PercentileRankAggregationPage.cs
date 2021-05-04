// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class PercentileRankAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-rank-aggregation.asciidoc:26")]
		public void Line26()
		{
			// tag::daaa9e0df859d764ca0a4a4ebcfbdb26[]
			var response0 = new SearchResponse<object>();
			// end::daaa9e0df859d764ca0a4a4ebcfbdb26[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_ranks"" : {
			            ""percentile_ranks"" : {
			                ""field"" : ""load_time"", \<1>
			                ""values"" : [500, 600]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-rank-aggregation.asciidoc:73")]
		public void Line73()
		{
			// tag::156dd311073c8c825e608becf63ae7fe[]
			var response0 = new SearchResponse<object>();
			// end::156dd311073c8c825e608becf63ae7fe[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""load_time_ranks"": {
			            ""percentile_ranks"": {
			                ""field"": ""load_time"",
			                ""values"": [500, 600],
			                ""keyed"": false
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-rank-aggregation.asciidoc:125")]
		public void Line125()
		{
			// tag::c9ea558335446fc64006724cb72684e1[]
			var response0 = new SearchResponse<object>();
			// end::c9ea558335446fc64006724cb72684e1[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_ranks"" : {
			            ""percentile_ranks"" : {
			                ""values"" : [500, 600],
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
		[Description("aggregations/metrics/percentile-rank-aggregation.asciidoc:154")]
		public void Line154()
		{
			// tag::59bcc5d1ed0aac1aa949f84d80a4fa1d[]
			var response0 = new SearchResponse<object>();
			// end::59bcc5d1ed0aac1aa949f84d80a4fa1d[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_ranks"" : {
			            ""percentile_ranks"" : {
			                ""values"" : [500, 600],
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
		[Description("aggregations/metrics/percentile-rank-aggregation.asciidoc:189")]
		public void Line189()
		{
			// tag::214d704d18485ab75ef53aa9c0524590[]
			var response0 = new SearchResponse<object>();
			// end::214d704d18485ab75ef53aa9c0524590[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_ranks"" : {
			            ""percentile_ranks"" : {
			                ""field"" : ""load_time"",
			                ""values"" : [500, 600],
			                ""hdr"": { \<1>
			                  ""number_of_significant_value_digits"" : 3 \<2>
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/percentile-rank-aggregation.asciidoc:221")]
		public void Line221()
		{
			// tag::77f575b0cc37dd7a2415cbf6417d3148[]
			var response0 = new SearchResponse<object>();
			// end::77f575b0cc37dd7a2415cbf6417d3148[]

			response0.MatchesExample(@"GET latency/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""load_time_ranks"" : {
			            ""percentile_ranks"" : {
			                ""field"" : ""load_time"",
			                ""values"" : [500, 600],
			                ""missing"": 10 \<1>
			            }
			        }
			    }
			}");
		}
	}
}
