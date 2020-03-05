using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Metrics
{
	public class TophitsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/tophits-aggregation.asciidoc:36")]
		public void Line36()
		{
			// tag::12b4b34f9958ed157ac2d812d612cda6[]
			var response0 = new SearchResponse<object>();
			// end::12b4b34f9958ed157ac2d812d612cda6[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"": {
			        ""top_tags"": {
			            ""terms"": {
			                ""field"": ""type"",
			                ""size"": 3
			            },
			            ""aggs"": {
			                ""top_sales_hits"": {
			                    ""top_hits"": {
			                        ""sort"": [
			                            {
			                                ""date"": {
			                                    ""order"": ""desc""
			                                }
			                            }
			                        ],
			                        ""_source"": {
			                            ""includes"": [ ""date"", ""price"" ]
			                        },
			                        ""size"" : 1
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/tophits-aggregation.asciidoc:185")]
		public void Line185()
		{
			// tag::30db2702dd0071c72a090b8311d0db09[]
			var response0 = new SearchResponse<object>();
			// end::30db2702dd0071c72a090b8311d0db09[]

			response0.MatchesExample(@"POST /sales/_search
			{
			  ""query"": {
			    ""match"": {
			      ""body"": ""elections""
			    }
			  },
			  ""aggs"": {
			    ""top_sites"": {
			      ""terms"": {
			        ""field"": ""domain"",
			        ""order"": {
			          ""top_hit"": ""desc""
			        }
			      },
			      ""aggs"": {
			        ""top_tags_hits"": {
			          ""top_hits"": {}
			        },
			        ""top_hit"" : {
			          ""max"": {
			            ""script"": {
			              ""source"": ""_score""
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/tophits-aggregation.asciidoc:238")]
		public void Line238()
		{
			// tag::2720c5e463876c415419c426697d15e4[]
			var response0 = new SearchResponse<object>();
			// end::2720c5e463876c415419c426697d15e4[]

			response0.MatchesExample(@"PUT /sales
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""tags"" : { ""type"" : ""keyword"" },
			            ""comments"" : { \<1>
			                ""type"" : ""nested"",
			                ""properties"" : {
			                    ""username"" : { ""type"" : ""keyword"" },
			                    ""comment"" : { ""type"" : ""text"" }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/tophits-aggregation.asciidoc:261")]
		public void Line261()
		{
			// tag::6ac67f7e30219d85fcc68b99459a39a4[]
			var response0 = new SearchResponse<object>();
			// end::6ac67f7e30219d85fcc68b99459a39a4[]

			response0.MatchesExample(@"PUT /sales/_doc/1?refresh
			{
			    ""tags"": [""car"", ""auto""],
			    ""comments"": [
			        {""username"": ""baddriver007"", ""comment"": ""This car could have better brakes""},
			        {""username"": ""dr_who"", ""comment"": ""Where's the autopilot? Can't find it""},
			        {""username"": ""ilovemotorbikes"", ""comment"": ""This car has two extra wheels""}
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/metrics/tophits-aggregation.asciidoc:277")]
		public void Line277()
		{
			// tag::f1b8612151a660264fb62dc6c74b19be[]
			var response0 = new SearchResponse<object>();
			// end::f1b8612151a660264fb62dc6c74b19be[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""query"": {
			        ""term"": { ""tags"": ""car"" }
			    },
			    ""aggs"": {
			        ""by_sale"": {
			            ""nested"" : {
			                ""path"" : ""comments""
			            },
			            ""aggs"": {
			                ""by_user"": {
			                    ""terms"": {
			                        ""field"": ""comments.username"",
			                        ""size"": 1
			                    },
			                    ""aggs"": {
			                        ""by_nested"": {
			                            ""top_hits"":{}
			                        }
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}