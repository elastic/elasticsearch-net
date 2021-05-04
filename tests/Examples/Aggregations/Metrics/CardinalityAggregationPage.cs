// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class CardinalityAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/cardinality-aggregation.asciidoc:11")]
		public void Line11()
		{
			// tag::826140cdd3d5fe9a728239605c6dc71a[]
			var response0 = new SearchResponse<object>();
			// end::826140cdd3d5fe9a728239605c6dc71a[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""type_count"" : {
			            ""cardinality"" : {
			                ""field"" : ""type""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/cardinality-aggregation.asciidoc:45")]
		public void Line45()
		{
			// tag::edbd54e71e56f3a5617aa012b100aa0f[]
			var response0 = new SearchResponse<object>();
			// end::edbd54e71e56f3a5617aa012b100aa0f[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""type_count"" : {
			            ""cardinality"" : {
			                ""field"" : ""type"",
			                ""precision_threshold"": 100 \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/cardinality-aggregation.asciidoc:185")]
		public void Line185()
		{
			// tag::ef3a3e292e9e74d42703555178ed5fb6[]
			var response0 = new SearchResponse<object>();
			// end::ef3a3e292e9e74d42703555178ed5fb6[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""type_promoted_count"" : {
			            ""cardinality"" : {
			                ""script"": {
			                    ""lang"": ""painless"",
			                    ""source"": ""doc['type'].value + ' ' + doc['promoted'].value""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/cardinality-aggregation.asciidoc:205")]
		public void Line205()
		{
			// tag::6969b29883eefa552475ae1837dc5f96[]
			var response0 = new SearchResponse<object>();
			// end::6969b29883eefa552475ae1837dc5f96[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""type_promoted_count"" : {
			            ""cardinality"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"": {
			                        ""type_field"": ""type"",
			                        ""promoted_field"": ""promoted""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/cardinality-aggregation.asciidoc:232")]
		public void Line232()
		{
			// tag::7d86ff090cbd87f144edb72e949470b3[]
			var response0 = new SearchResponse<object>();
			// end::7d86ff090cbd87f144edb72e949470b3[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""tag_cardinality"" : {
			            ""cardinality"" : {
			                ""field"" : ""tag"",
			                ""missing"": ""N/A"" \<1>
			            }
			        }
			    }
			}");
		}
	}
}
