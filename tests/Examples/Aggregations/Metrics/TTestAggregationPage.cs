// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Aggregations.Metrics
{
	public class TTestAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/t-test-aggregation.asciidoc:30")]
		public void Line30()
		{
			// tag::9c071245643d02289b05d8163f207f74[]
			var response0 = new SearchResponse<object>();
			// end::9c071245643d02289b05d8163f207f74[]

			response0.MatchesExample(@"GET node_upgrade/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""startup_time_ttest"" : {
			            ""t_test"" : {
			                ""a"" : {""field"": ""startup_time_before""}, <1>
			                ""b"" : {""field"": ""startup_time_after""}, <2>
			                ""type"": ""paired"" <3>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/t-test-aggregation.asciidoc:84")]
		public void Line84()
		{
			// tag::a170b4aa968b32e40e288a97088cfc50[]
			var response0 = new SearchResponse<object>();
			// end::a170b4aa968b32e40e288a97088cfc50[]

			response0.MatchesExample(@"GET node_upgrade/_search
			{
			    ""size"" : 0,
			    ""aggs"" : {
			        ""startup_time_ttest"" : {
			            ""t_test"" : {
			                ""a"" : {
			                    ""field"" : ""startup_time_before"", <1>
			                    ""filter"" : {
			                        ""term"" : {
			                            ""group"" : ""A""  <2>
			                        }
			                    }
			                },
			                ""b"" : {
			                    ""field"" : ""startup_time_before"", <3>
			                    ""filter"" : {
			                        ""term"" : {
			                            ""group"" : ""B"" <4>
			                        }
			                    }
			                },
			                ""type"" : ""heteroscedastic"" <5>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/t-test-aggregation.asciidoc:146")]
		public void Line146()
		{
			// tag::f114ff40e67771a24f796f458ab10838[]
			var response0 = new SearchResponse<object>();
			// end::f114ff40e67771a24f796f458ab10838[]

			response0.MatchesExample(@"GET node_upgrade/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""startup_time_ttest"" : {
			            ""t_test"" : {
			                ""a"": {
			                    ""script"" : {
			                        ""lang"": ""painless"",
			                        ""source"": ""doc['startup_time_before'].value - params.adjustment"", <1>
			                        ""params"" : {
			                            ""adjustment"" : 10   <2>
			                        }
			                    }
			                },
			                ""b"": {
			                    ""field"": ""startup_time_after"" <3>
			                },
			                ""type"": ""paired""
			            }
			        }
			    }
			}");
		}
	}
}
