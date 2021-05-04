// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class TermsAggregationPage : ExampleBase
	{
		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:57")]
		public void Line57()
		{
			// tag::9a8995fd31351045d99c78e40444c8ea[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("genres", t => t
						.Field("genre")
					)
				)
			);
			// end::9a8995fd31351045d99c78e40444c8ea[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : { ""field"" : ""genre"" } \<1>
			        }
			    }
			}");
		}

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:162")]
		public void Line162()
		{
			// tag::35e8da9410b8432cf4095f2541ad7b1d[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("products", t => t
						.Field("product")
						.Size(5)
						.ShowTermDocCountError()
					)
				)
			);
			// end::35e8da9410b8432cf4095f2541ad7b1d[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:207")]
		public void Line207()
		{
			// tag::6a4679531e64c492fce16dc12de6dcb0[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("genres", t => t
						.Field("genre")
						.Order(o => o
							.CountAscending()
						)
					)
				)
			);
			// end::6a4679531e64c492fce16dc12de6dcb0[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : {
			                ""field"" : ""genre"",
			                ""order"" : { ""_count"" : ""asc"" }
			            }
			        }
			    }
			}", e =>
			{
				e.ApplyBodyChanges(b => { b["aggs"]["genres"]["terms"]["order"].ToJArray(); });
			});
		}

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:224")]
		public void Line224()
		{
			// tag::93f1bdd72e79827dcf9a34efa02fd977[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("genres", t => t
						.Field("genre")
						.Order(o => o
							.KeyAscending()
						)
					)
				)
			);
			// end::93f1bdd72e79827dcf9a34efa02fd977[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""genres"" : {
			            ""terms"" : {
			                ""field"" : ""genre"",
			                ""order"" : { ""_key"" : ""asc"" }
			            }
			        }
			    }
			}", e =>
			{
				e.ApplyBodyChanges(b => { b["aggs"]["genres"]["terms"]["order"].ToJArray(); });
			});
		}

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:243")]
		public void Line243()
		{
			// tag::71b5b2ba9557d0f296ff2de91727d2f6[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("genres", t => t
						.Field("genre")
						.Order(o => o
							.Descending("max_play_count")
						)
						.Aggregations(aa => aa
							.Max("max_play_count", m => m
								.Field("play_count")
							)
						)
					)
				)
			);
			// end::71b5b2ba9557d0f296ff2de91727d2f6[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e =>
			{
				e.ApplyBodyChanges(b => { b["aggs"]["genres"]["terms"]["order"].ToJArray(); });
			});
		}

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:263")]
		public void Line263()
		{
			// tag::34efeade38445b2834749ced59782e25[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("genres", t => t
						.Field("genre")
						.Order(o => o
							.Descending("playback_stats.max")
						)
						.Aggregations(aa => aa
							.Stats("playback_stats", m => m
								.Field("play_count")
							)
						)
					)
				)
			);
			// end::34efeade38445b2834749ced59782e25[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e =>
			{
				e.ApplyBodyChanges(b => { b["aggs"]["genres"]["terms"]["order"].ToJArray(); });
			});
		}

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:309")]
		public void Line309()
		{
			// tag::dc15e2373e5ecbe09b4ea0858eb63d47[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("countries", t => t
						.Field("artist.country")
						.Order(o => o
							.Descending("rock>playback_stats.avg")
						)
						.Aggregations(aa => aa
							.Filter("rock", f => f
								.Filter(q => q
									.Term("genre", "rock")
								)
								.Aggregations(aaa => aaa
									.Stats("playback_stats", st => st
										.Field("play_count")
									)
								)
							)
						)
					)
				)
			);
			// end::dc15e2373e5ecbe09b4ea0858eb63d47[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e =>
			{
				e.ApplyBodyChanges(b =>
				{
					b["aggs"]["countries"]["terms"]["order"].ToJArray();
					b["aggs"]["countries"]["aggs"]["rock"]["filter"]["term"]["genre"].ToLongFormTermQuery();
				});
			});
		}

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:336")]
		public void Line336()
		{
			// tag::028f6d6ac2594e20b78b8a8f8cbad49d[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("countries", t => t
						.Field("artist.country")
						.Order(o => o
							.Descending("rock>playback_stats.avg")
							.CountDescending()
						)
						.Aggregations(aa => aa
							.Filter("rock", f => f
								.Filter(q => q
									.Term("genre", "rock")
								)
								.Aggregations(aaa => aaa
									.Stats("playback_stats", st => st
										.Field("play_count")
									)
								)
							)
						)
					)
				)
			);
			// end::028f6d6ac2594e20b78b8a8f8cbad49d[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e =>
			{
				e.ApplyBodyChanges(b => { b["aggs"]["countries"]["aggs"]["rock"]["filter"]["term"]["genre"].ToLongFormTermQuery(); });
			});
		}

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:369")]
		public void Line369()
		{
			// tag::527324766814561b75aaee853ede49a7[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("tags", t => t
						.Field("tags")
						.MinimumDocumentCount(10)
					)
				)
			);
			// end::527324766814561b75aaee853ede49a7[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:410")]
		public void Line410()
		{
			// tag::033778305d52746f5ce0a2a922c8e521[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("genres", t => t
						.Script(sc => sc
							.Source("doc['genre'].value")
							.Lang("painless")
						)
					)
				)
			);
			// end::033778305d52746f5ce0a2a922c8e521[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:444")]
		public void Line444()
		{
			// tag::4646764bf09911fee7d58630c72d3137[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("genres", t => t
						.Script(sc => sc
							.Id("my_script")
							.Params(p => p
								.Add("field", "genre")
							)
						)
					)
				)
			);
			// end::4646764bf09911fee7d58630c72d3137[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:466")]
		public void Line466()
		{
			// tag::a49169b4622918992411fab4ec48191b[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("genres", t => t
						.Field("genre")
						.Script(sc => sc
							.Source("'Genre: ' +_value")
							.Lang("painless")
						)
					)
				)
			);
			// end::a49169b4622918992411fab4ec48191b[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:492")]
		public void Line492()
		{
			// tag::0afaf1cad692e6201aa574c8feb6e622[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("tags", t => t
						.Field("tags")
						.Include(".*sport.*")
						.Exclude("water_.*")
					)
				)
			);
			// end::0afaf1cad692e6201aa574c8feb6e622[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:520")]
		public void Line520()
		{
			// tag::98b121bf47cebd85671a2cb519688d28[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("JapaneseCars", t => t
						.Field("make")
						.Include(new[] { "mazda", "honda" })
					)
					.Terms("ActiveCarManufacturers", t => t
						.Field("make")
						.Exclude(new[] { "rover", "jensen" })
					)
				)
			);
			// end::98b121bf47cebd85671a2cb519688d28[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:549")]
		public void Line549()
		{
			// tag::5d9d7b84e2fec7ecd832145cbb951cf1[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Size(0)
				.Aggregations(a => a
					.Terms("expired_sessions", t => t
						.Field("account_id")
						.Include(0, 20)
						.Size(10000)
						.Order(o => o
							.Ascending("last_access")
						)
						.Aggregations(aa => aa
							.Max("last_access", m => m
								.Field("access_date")
							)
						)
					)
				)
			);
			// end::5d9d7b84e2fec7ecd832145cbb951cf1[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", e =>
			{
				e.ApplyBodyChanges(b => { b["aggs"]["expired_sessions"]["terms"]["order"].ToJArray(); });
			});
		}

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:641")]
		public void Line641()
		{
			// tag::7f28f8ae8fcdbd807dadde0b5b007a6d[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("actors", t => t
						.Field("actors")
						.Size(10)
						.Aggregations(aa => aa
							.Terms("costars", tt => tt
								.Field("actors")
								.Size(5)
							)
						)
					)
				)
			);
			// end::7f28f8ae8fcdbd807dadde0b5b007a6d[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:672")]
		public void Line672()
		{
			// tag::cd5bc5bf7cd58d7b1492c9c298b345f6[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("actors", t => t
						.Field("actors")
						.Size(10)
						.CollectMode(TermsAggregationCollectMode.BreadthFirst)
						.Aggregations(aa => aa
							.Terms("costars", tt => tt
								.Field("actors")
								.Size(5)
							)
						)
					)
				)
			);
			// end::cd5bc5bf7cd58d7b1492c9c298b345f6[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:723")]
		public void Line723()
		{
			// tag::774d715155cd13713e6e327adf6ce328[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("tags", t => t
						.Field("tags")
						.ExecutionHint(TermsAggregationExecutionHint.Map)
					)
				)
			);
			// end::774d715155cd13713e6e327adf6ce328[]

			searchResponse.MatchesExample(@"GET /_search
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

		[U]
		[Description("aggregations/bucket/terms-aggregation.asciidoc:748")]
		public void Line748()
		{
			// tag::f085fb032dae56a3b104ab874eaea2ad[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Aggregations(a => a
					.Terms("tags", t => t
						.Field("tags")
						.Missing("N/A")
					)
				)
			);
			// end::f085fb032dae56a3b104ab874eaea2ad[]

			searchResponse.MatchesExample(@"GET /_search
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
