// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class SignificanttextAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significanttext-aggregation.asciidoc:36")]
		public void Line36()
		{
			// tag::68f0c7c77b65bfdded348bbd397831b7[]
			var response0 = new SearchResponse<object>();
			// end::68f0c7c77b65bfdded348bbd397831b7[]

			response0.MatchesExample(@"GET news/_search
			{
			    ""query"" : {
			        ""match"" : {""content"" : ""Bird flu""}
			    },
			    ""aggregations"" : {
			        ""my_sample"" : {
			            ""sampler"" : {
			                ""shard_size"" : 100
			            },
			            ""aggregations"": {
			                ""keywords"" : {
			                    ""significant_text"" : { ""field"" : ""content"" }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significanttext-aggregation.asciidoc:150")]
		public void Line150()
		{
			// tag::d44ecc69090c0b2bc08a6cbc2e3467c5[]
			var response0 = new SearchResponse<object>();
			// end::d44ecc69090c0b2bc08a6cbc2e3467c5[]

			response0.MatchesExample(@"GET news/_search
			{
			  ""query"": {
			    ""simple_query_string"": {
			      ""query"": ""+elasticsearch  +pozmantier""
			    }
			  },
			  ""_source"": [
			    ""title"",
			    ""source""
			  ],
			  ""highlight"": {
			    ""fields"": {
			      ""content"": {}
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significanttext-aggregation.asciidoc:218")]
		public void Line218()
		{
			// tag::805f5550b90e75aa5cc82b90d8c6c242[]
			var response0 = new SearchResponse<object>();
			// end::805f5550b90e75aa5cc82b90d8c6c242[]

			response0.MatchesExample(@"GET news/_search
			{
			  ""query"": {
			    ""match"": {
			      ""content"": ""elasticsearch""
			    }
			  },
			  ""aggs"": {
			    ""sample"": {
			      ""sampler"": {
			        ""shard_size"": 100
			      },
			      ""aggs"": {
			        ""keywords"": {
			          ""significant_text"": {
			            ""field"": ""content"",
			            ""filter_duplicate_text"": true
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significanttext-aggregation.asciidoc:420")]
		public void Line420()
		{
			// tag::5f4cab20671ebac9233812f9e35d9c8b[]
			var response0 = new SearchResponse<object>();
			// end::5f4cab20671ebac9233812f9e35d9c8b[]

			response0.MatchesExample(@"GET news/_search
			{
			    ""query"" : {
			        ""match"" : {
			            ""content"" : ""madrid""
			        }
			    },
			    ""aggs"" : {
			        ""tags"" : {
			            ""significant_text"" : {
			                ""field"" : ""content"",
			                ""background_filter"": {
			                    ""term"" : { ""content"" : ""spain""}
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/significanttext-aggregation.asciidoc:458")]
		public void Line458()
		{
			// tag::b3e6d6f7f6d65d1efb60ca7503a20b16[]
			var response0 = new SearchResponse<object>();
			// end::b3e6d6f7f6d65d1efb60ca7503a20b16[]

			response0.MatchesExample(@"GET news/_search
			{
			    ""query"" : {
			        ""match"" : {
			            ""custom_all"" : ""elasticsearch""
			        }
			    },
			    ""aggs"" : {
			        ""tags"" : {
			            ""significant_text"" : {
			                ""field"" : ""custom_all"",
			                ""source_fields"": [""content"" , ""title""]
			            }
			        }
			    }
			}");
		}
	}
}
