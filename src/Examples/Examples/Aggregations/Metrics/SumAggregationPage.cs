using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Metrics
{
	public class SumAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
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
		public void Line50()
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
		public void Line110()
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
		public void Line143()
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
	}
}