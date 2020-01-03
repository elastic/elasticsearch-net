using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Bucket
{
	public class CompositeAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line116()
		{
			// tag::118c81b8561fd9a9ead388d7971fccd9[]
			var response0 = new SearchResponse<object>();
			// end::118c81b8561fd9a9ead388d7971fccd9[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""product"": { ""terms"" : { ""field"": ""product"" } } }
			                ]
			            }
			        }
			     }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line134()
		{
			// tag::d4d4cb1e761f72aa7cd408655dbcbeac[]
			var response0 = new SearchResponse<object>();
			// end::d4d4cb1e761f72aa7cd408655dbcbeac[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    {
			                        ""product"": {
			                            ""terms"" : {
			                                ""script"" : {
			                                    ""source"": ""doc['product'].value"",
			                                    ""lang"": ""painless""
			                                }
			                            }
			                        }
			                    }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line168()
		{
			// tag::59d377892d4d912b216defa48e7befce[]
			var response0 = new SearchResponse<object>();
			// end::59d377892d4d912b216defa48e7befce[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""histo"": { ""histogram"" : { ""field"": ""price"", ""interval"": 5 } } }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line186()
		{
			// tag::a7ad889b26defd508889b288e076f05f[]
			var response0 = new SearchResponse<object>();
			// end::a7ad889b26defd508889b288e076f05f[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    {
			                        ""histo"": {
			                            ""histogram"" : {
			                                ""interval"": 5,
			                                ""script"" : {
			                                    ""source"": ""doc['price'].value"",
			                                    ""lang"": ""painless""
			                                }
			                            }
			                        }
			                    }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line218()
		{
			// tag::9361db99de15d1f18233a555777c2e1f[]
			var response0 = new SearchResponse<object>();
			// end::9361db99de15d1f18233a555777c2e1f[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""date"": { ""date_histogram"" : { ""field"": ""timestamp"", ""calendar_interval"": ""1d"" } } }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line247()
		{
			// tag::2fb60a596d3d996c1329fb4c50955b89[]
			var response0 = new SearchResponse<object>();
			// end::2fb60a596d3d996c1329fb4c50955b89[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    {
			                        ""date"": {
			                            ""date_histogram"" : {
			                                ""field"": ""timestamp"",
			                                ""calendar_interval"": ""1d"",
			                                ""format"": ""yyyy-MM-dd"" \<1>
			                            }
			                        }
			                    }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line289()
		{
			// tag::5d9aef8cd8d324049e34bf96e38814ee[]
			var response0 = new SearchResponse<object>();
			// end::5d9aef8cd8d324049e34bf96e38814ee[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""date"": { ""date_histogram"": { ""field"": ""timestamp"", ""calendar_interval"": ""1d"" } } },
			                    { ""product"": { ""terms"": {""field"": ""product"" } } }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line311()
		{
			// tag::ce182c31ce9ffb336dd26ee9899da3e7[]
			var response0 = new SearchResponse<object>();
			// end::ce182c31ce9ffb336dd26ee9899da3e7[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""shop"": { ""terms"": {""field"": ""shop"" } } },
			                    { ""product"": { ""terms"": { ""field"": ""product"" } } },
			                    { ""date"": { ""date_histogram"": { ""field"": ""timestamp"", ""calendar_interval"": ""1d"" } } }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line340()
		{
			// tag::9837cab0afe4bae8d11e42411cb812ad[]
			var response0 = new SearchResponse<object>();
			// end::9837cab0afe4bae8d11e42411cb812ad[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""date"": { ""date_histogram"": { ""field"": ""timestamp"", ""calendar_interval"": ""1d"", ""order"": ""desc"" } } },
			                    { ""product"": { ""terms"": {""field"": ""product"", ""order"": ""asc"" } } }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line366()
		{
			// tag::af056fa2f099bbf339d07b6d11a46210[]
			var response0 = new SearchResponse<object>();
			// end::af056fa2f099bbf339d07b6d11a46210[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""product_name"": { ""terms"" : { ""field"": ""product"", ""missing_bucket"": true } } }
			                ]
			            }
			        }
			     }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line405()
		{
			// tag::b29c0503d688299dd1eb87ff0fe69415[]
			var response0 = new SearchResponse<object>();
			// end::b29c0503d688299dd1eb87ff0fe69415[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""size"": 2,
			                ""sources"" : [
			                    { ""date"": { ""date_histogram"": { ""field"": ""timestamp"", ""calendar_interval"": ""1d"" } } },
			                    { ""product"": { ""terms"": {""field"": ""product"" } } }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line470()
		{
			// tag::b6dc7bb2713d7fe2eb6e480dee2e458d[]
			var response0 = new SearchResponse<object>();
			// end::b6dc7bb2713d7fe2eb6e480dee2e458d[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""size"": 2,
			                 ""sources"" : [
			                    { ""date"": { ""date_histogram"": { ""field"": ""timestamp"", ""calendar_interval"": ""1d"", ""order"": ""desc"" } } },
			                    { ""product"": { ""terms"": {""field"": ""product"", ""order"": ""asc"" } } }
			                ],
			                ""after"": { ""date"": 1494288000000, ""product"": ""mad max"" } \<1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line499()
		{
			// tag::e4979ca30ac53864edb4871a23ad73b3[]
			var response0 = new SearchResponse<object>();
			// end::e4979ca30ac53864edb4871a23ad73b3[]

			response0.MatchesExample(@"GET /_search
			{
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                 ""sources"" : [
			                    { ""date"": { ""date_histogram"": { ""field"": ""timestamp"", ""calendar_interval"": ""1d"", ""order"": ""desc"" } } },
			                    { ""product"": { ""terms"": {""field"": ""product"" } } }
			                ]
			            },
			            ""aggregations"": {
			                ""the_avg"": {
			                    ""avg"": { ""field"": ""price"" }
			                }
			            }
			        }
			    }
			}");
		}
	}
}