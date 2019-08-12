using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Bucket
{
	public class TermsAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line57()
		{
			// tag::9a8995fd31351045d99c78e40444c8ea[]
			var response0 = new SearchResponse<object>();
			// end::9a8995fd31351045d99c78e40444c8ea[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : { ""field"" : ""genre"" } \<1>
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line134()
		{
			// tag::d50a3835bf5795ac73e58906a3413544[]
			var response0 = new SearchResponse<object>();
			// end::d50a3835bf5795ac73e58906a3413544[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""products"" : {
			            ""terms"" : {
			                ""field"" : ""product"",
			                ""size"" : 5
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line264()
		{
			// tag::35e8da9410b8432cf4095f2541ad7b1d[]
			var response0 = new SearchResponse<object>();
			// end::35e8da9410b8432cf4095f2541ad7b1d[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""products"" : {
			            ""terms"" : {
			                ""field"" : ""product"",
			                ""size"" : 5,
			                ""show_term_doc_count_error"": true
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line342()
		{
			// tag::6a4679531e64c492fce16dc12de6dcb0[]
			var response0 = new SearchResponse<object>();
			// end::6a4679531e64c492fce16dc12de6dcb0[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : {
			                ""field"" : ""genre"",
			                ""order"" : { ""_count"" : ""asc"" }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line360()
		{
			// tag::93f1bdd72e79827dcf9a34efa02fd977[]
			var response0 = new SearchResponse<object>();
			// end::93f1bdd72e79827dcf9a34efa02fd977[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : {
			                ""field"" : ""genre"",
			                ""order"" : { ""_key"" : ""asc"" }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line380()
		{
			// tag::71b5b2ba9557d0f296ff2de91727d2f6[]
			var response0 = new SearchResponse<object>();
			// end::71b5b2ba9557d0f296ff2de91727d2f6[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : {
			                ""field"" : ""genre"",
			                ""order"" : { ""max_play_count"" : ""desc"" }
			            },
			            ""aggs"" : {
			                ""max_play_count"" : { ""max"" : { ""field"" : ""play_count"" } }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line401()
		{
			// tag::34efeade38445b2834749ced59782e25[]
			var response0 = new SearchResponse<object>();
			// end::34efeade38445b2834749ced59782e25[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : {
			                ""field"" : ""genre"",
			                ""order"" : { ""playback_stats.max"" : ""desc"" }
			            },
			            ""aggs"" : {
			                ""playback_stats"" : { ""stats"" : { ""field"" : ""play_count"" } }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line448()
		{
			// tag::dc15e2373e5ecbe09b4ea0858eb63d47[]
			var response0 = new SearchResponse<object>();
			// end::dc15e2373e5ecbe09b4ea0858eb63d47[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""countries"" : {
			            ""terms"" : {
			                ""field"" : ""artist.country"",
			                ""order"" : { ""rock>playback_stats.avg"" : ""desc"" }
			            },
			            ""aggs"" : {
			                ""rock"" : {
			                    ""filter"" : { ""term"" : { ""genre"" :  ""rock"" }},
			                    ""aggs"" : {
			                        ""playback_stats"" : { ""stats"" : { ""field"" : ""play_count"" }}
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line476()
		{
			// tag::028f6d6ac2594e20b78b8a8f8cbad49d[]
			var response0 = new SearchResponse<object>();
			// end::028f6d6ac2594e20b78b8a8f8cbad49d[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""countries"" : {
			            ""terms"" : {
			                ""field"" : ""artist.country"",
			                ""order"" : [ { ""rock>playback_stats.avg"" : ""desc"" }, { ""_count"" : ""desc"" } ]
			            },
			            ""aggs"" : {
			                ""rock"" : {
			                    ""filter"" : { ""term"" : { ""genre"" : ""rock"" }},
			                    ""aggs"" : {
			                        ""playback_stats"" : { ""stats"" : { ""field"" : ""play_count"" }}
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line510()
		{
			// tag::527324766814561b75aaee853ede49a7[]
			var response0 = new SearchResponse<object>();
			// end::527324766814561b75aaee853ede49a7[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""tags"" : {
			            ""terms"" : {
			                ""field"" : ""tags"",
			                ""min_doc_count"": 10
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line552()
		{
			// tag::033778305d52746f5ce0a2a922c8e521[]
			var response0 = new SearchResponse<object>();
			// end::033778305d52746f5ce0a2a922c8e521[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : {
			                ""script"" : {
			                    ""source"": ""doc['genre'].value"",
			                    ""lang"": ""painless""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line588()
		{
			// tag::4646764bf09911fee7d58630c72d3137[]
			var response0 = new SearchResponse<object>();
			// end::4646764bf09911fee7d58630c72d3137[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"": {
			                        ""field"": ""genre""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line611()
		{
			// tag::a49169b4622918992411fab4ec48191b[]
			var response0 = new SearchResponse<object>();
			// end::a49169b4622918992411fab4ec48191b[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : {
			                ""field"" : ""genre"",
			                ""script"" : {
			                    ""source"" : ""'Genre: ' +_value"",
			                    ""lang"" : ""painless""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line638()
		{
			// tag::0afaf1cad692e6201aa574c8feb6e622[]
			var response0 = new SearchResponse<object>();
			// end::0afaf1cad692e6201aa574c8feb6e622[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""tags"" : {
			            ""terms"" : {
			                ""field"" : ""tags"",
			                ""include"" : "".*sport.*"",
			                ""exclude"" : ""water_.*""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line667()
		{
			// tag::98b121bf47cebd85671a2cb519688d28[]
			var response0 = new SearchResponse<object>();
			// end::98b121bf47cebd85671a2cb519688d28[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""JapaneseCars"" : {
			             ""terms"" : {
			                 ""field"" : ""make"",
			                 ""include"" : [""mazda"", ""honda""]
			             }
			         },
			        ""ActiveCarManufacturers"" : {
			             ""terms"" : {
			                 ""field"" : ""make"",
			                 ""exclude"" : [""rover"", ""jensen""]
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line697()
		{
			// tag::5d9d7b84e2fec7ecd832145cbb951cf1[]
			var response0 = new SearchResponse<object>();
			// end::5d9d7b84e2fec7ecd832145cbb951cf1[]

			response0.MatchesExample(@"GET /_search
			{
			   ""size"": 0,
			   ""aggs"": {
			      ""expired_sessions"": {
			         ""terms"": {
			            ""field"": ""account_id"",
			            ""include"": {
			               ""partition"": 0,
			               ""num_partitions"": 20
			            },
			            ""size"": 10000,
			            ""order"": {
			               ""last_access"": ""asc""
			            }
			         },
			         ""aggs"": {
			            ""last_access"": {
			               ""max"": {
			                  ""field"": ""access_date""
			               }
			            }
			         }
			      }
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line790()
		{
			// tag::7f28f8ae8fcdbd807dadde0b5b007a6d[]
			var response0 = new SearchResponse<object>();
			// end::7f28f8ae8fcdbd807dadde0b5b007a6d[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""actors"" : {
			             ""terms"" : {
			                 ""field"" : ""actors"",
			                 ""size"" : 10
			             },
			            ""aggs"" : {
			                ""costars"" : {
			                     ""terms"" : {
			                         ""field"" : ""actors"",
			                         ""size"" : 5
			                     }
			                 }
			            }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line822()
		{
			// tag::cd5bc5bf7cd58d7b1492c9c298b345f6[]
			var response0 = new SearchResponse<object>();
			// end::cd5bc5bf7cd58d7b1492c9c298b345f6[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""actors"" : {
			             ""terms"" : {
			                 ""field"" : ""actors"",
			                 ""size"" : 10,
			                 ""collect_mode"" : ""breadth_first"" \<1>
			             },
			            ""aggs"" : {
			                ""costars"" : {
			                     ""terms"" : {
			                         ""field"" : ""actors"",
			                         ""size"" : 5
			                     }
			                 }
			            }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line874()
		{
			// tag::774d715155cd13713e6e327adf6ce328[]
			var response0 = new SearchResponse<object>();
			// end::774d715155cd13713e6e327adf6ce328[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""tags"" : {
			             ""terms"" : {
			                 ""field"" : ""tags"",
			                 ""execution_hint"": ""map"" \<1>
			             }
			         }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line900()
		{
			// tag::f085fb032dae56a3b104ab874eaea2ad[]
			var response0 = new SearchResponse<object>();
			// end::f085fb032dae56a3b104ab874eaea2ad[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""tags"" : {
			             ""terms"" : {
			                 ""field"" : ""tags"",
			                 ""missing"": ""N/A"" \<1>
			             }
			         }
			    }
			}");
		}
	}
}