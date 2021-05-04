// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class StringStatsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/string-stats-aggregation.asciidoc:22")]
		public void Line22()
		{
			// tag::5507b01f713674781996a07718785444[]
			var response0 = new SearchResponse<object>();
			// end::5507b01f713674781996a07718785444[]

			response0.MatchesExample(@"POST /twitter/_search?size=0
			{
			    ""aggs"" : {
			        ""message_stats"" : { ""string_stats"" : { ""field"" : ""message.keyword"" } }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/string-stats-aggregation.asciidoc:63")]
		public void Line63()
		{
			// tag::a1d02ab1549dbd3f1874f6a2fd48ec48[]
			var response0 = new SearchResponse<object>();
			// end::a1d02ab1549dbd3f1874f6a2fd48ec48[]

			response0.MatchesExample(@"POST /twitter/_search?size=0
			{
			    ""aggs"" : {
			        ""message_stats"" : {
			            ""string_stats"" : {
			                ""field"" : ""message.keyword"",
			                ""show_distribution"": true  <1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/string-stats-aggregation.asciidoc:130")]
		public void Line130()
		{
			// tag::24b22f8b83e9b199964036a170e4299e[]
			var response0 = new SearchResponse<object>();
			// end::24b22f8b83e9b199964036a170e4299e[]

			response0.MatchesExample(@"POST /twitter/_search?size=0
			{
			    ""aggs"" : {
			        ""message_stats"" : {
			             ""string_stats"" : {
			                 ""script"" : {
			                     ""lang"": ""painless"",
			                     ""source"": ""doc['message.keyword'].value""
			                 }
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/string-stats-aggregation.asciidoc:151")]
		public void Line151()
		{
			// tag::a89a22d4f41ee3538bbfec8ac9e7ac74[]
			var response0 = new SearchResponse<object>();
			// end::a89a22d4f41ee3538bbfec8ac9e7ac74[]

			response0.MatchesExample(@"POST /twitter/_search?size=0
			{
			    ""aggs"" : {
			        ""message_stats"" : {
			            ""string_stats"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"" : {
			                        ""field"" : ""message.keyword""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/string-stats-aggregation.asciidoc:175")]
		public void Line175()
		{
			// tag::46078bde3789b5e7e83ab458857bc9c9[]
			var response0 = new SearchResponse<object>();
			// end::46078bde3789b5e7e83ab458857bc9c9[]

			response0.MatchesExample(@"POST /twitter/_search?size=0
			{
			    ""aggs"" : {
			        ""message_stats"" : {
			            ""string_stats"" : {
			                ""field"" : ""message.keyword"",
			                ""script"" : {
			                    ""lang"": ""painless"",
			                    ""source"": ""params.prefix + _value"",
			                    ""params"" : {
			                        ""prefix"" : ""Message: ""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/string-stats-aggregation.asciidoc:202")]
		public void Line202()
		{
			// tag::437f4009a93d46b6b76e7db366b34ce6[]
			var response0 = new SearchResponse<object>();
			// end::437f4009a93d46b6b76e7db366b34ce6[]

			response0.MatchesExample(@"POST /twitter/_search?size=0
			{
			    ""aggs"" : {
			        ""message_stats"" : {
			            ""string_stats"" : {
			                ""field"" : ""message.keyword"",
			                ""missing"": ""[empty message]"" <1>
			            }
			        }
			    }
			}");
		}
	}
}
