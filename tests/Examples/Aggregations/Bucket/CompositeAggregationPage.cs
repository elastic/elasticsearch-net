using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Bucket
{
	public class CompositeAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line117()
		{
			// tag::b0d7068cff901f2b91f8387628e3c2c0[]
			var response0 = new SearchResponse<object>();
			// end::b0d7068cff901f2b91f8387628e3c2c0[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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
		public void Line136()
		{
			// tag::47f1e01d131fd50304dd35f1c459d222[]
			var response0 = new SearchResponse<object>();
			// end::47f1e01d131fd50304dd35f1c459d222[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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
		public void Line172()
		{
			// tag::426eb07a1fc499df8ea30d8593a0d989[]
			var response0 = new SearchResponse<object>();
			// end::426eb07a1fc499df8ea30d8593a0d989[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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
		public void Line191()
		{
			// tag::d4c5e4123e53daa39775def537365376[]
			var response0 = new SearchResponse<object>();
			// end::d4c5e4123e53daa39775def537365376[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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
		public void Line224()
		{
			// tag::4997db4f41283835e5a6250c454bec92[]
			var response0 = new SearchResponse<object>();
			// end::4997db4f41283835e5a6250c454bec92[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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
		public void Line254()
		{
			// tag::1c42bc684745178a587494809ab6ae57[]
			var response0 = new SearchResponse<object>();
			// end::1c42bc684745178a587494809ab6ae57[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    {
			                        ""date"": {
			                            ""date_histogram"" : {
			                                ""field"": ""timestamp"",
			                                ""calendar_interval"": ""1d"",
			                                ""format"": ""yyyy-MM-dd"" <1>
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
		public void Line295()
		{
			// tag::038bef10c90916a9addab866fc73dcca[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::038bef10c90916a9addab866fc73dcca[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""date"": ""2015-10-01T05:30:00Z""
			}");

			response1.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""date"": ""2015-10-01T06:30:00Z""
			}");

			response2.MatchesExample(@"GET my_index/_search?size=0
			{
			  ""aggs"": {
			    ""my_buckets"": {
			      ""composite"" : {
			        ""sources"" : [
			          {
			            ""date"": {
			              ""date_histogram"" : {
			                ""field"": ""date"",
			                ""calendar_interval"": ""day"",
			                ""offset"": ""+6h"",
			                ""format"": ""iso8601""
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
		public void Line363()
		{
			// tag::7df600a962ddb9e75462cab1017ab710[]
			var response0 = new SearchResponse<object>();
			// end::7df600a962ddb9e75462cab1017ab710[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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
		public void Line386()
		{
			// tag::1559d896ef715c8997e773e8f26ded49[]
			var response0 = new SearchResponse<object>();
			// end::1559d896ef715c8997e773e8f26ded49[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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
		public void Line416()
		{
			// tag::1111d70f0ae3044b0a86c82b2ded5f74[]
			var response0 = new SearchResponse<object>();
			// end::1111d70f0ae3044b0a86c82b2ded5f74[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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
		public void Line443()
		{
			// tag::441e1052c59a3d9182fd608c08e11169[]
			var response0 = new SearchResponse<object>();
			// end::441e1052c59a3d9182fd608c08e11169[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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
		public void Line483()
		{
			// tag::a84493b3b31741c9e1f998b59b40db82[]
			var response0 = new SearchResponse<object>();
			// end::a84493b3b31741c9e1f998b59b40db82[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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
		public void Line541()
		{
			// tag::eac8d98e2bd0eb75e8428212e9f4e4a7[]
			var response0 = new SearchResponse<object>();
			// end::eac8d98e2bd0eb75e8428212e9f4e4a7[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""size"": 2,
			                 ""sources"" : [
			                    { ""date"": { ""date_histogram"": { ""field"": ""timestamp"", ""calendar_interval"": ""1d"", ""order"": ""desc"" } } },
			                    { ""product"": { ""terms"": {""field"": ""product"", ""order"": ""asc"" } } }
			                ],
			                ""after"": { ""date"": 1494288000000, ""product"": ""mad max"" } <1>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line573()
		{
			// tag::1d1186dc28cb5b11c19a8341ec1c0558[]
			var response0 = new SearchResponse<object>();
			// end::1d1186dc28cb5b11c19a8341ec1c0558[]

			response0.MatchesExample(@"PUT twitter
			{
			    ""settings"" : {
			        ""index"" : {
			            ""sort.field"" : [""username"", ""timestamp""],   <1>
			            ""sort.order"" : [""asc"", ""desc""]              <2>
			        }
			    },
			    ""mappings"": {
			        ""properties"": {
			            ""username"": {
			                ""type"": ""keyword"",
			                ""doc_values"": true
			            },
			            ""timestamp"": {
			                ""type"": ""date""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line602()
		{
			// tag::ca3c86d8bb26a8a9422b4b628de03dc4[]
			var response0 = new SearchResponse<object>();
			// end::ca3c86d8bb26a8a9422b4b628de03dc4[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""user_name"": { ""terms"" : { ""field"": ""user_name"" } } }     <1>
			                ]
			            }
			        }
			     }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line621()
		{
			// tag::c98edce2074791ebad716b9a5b03215f[]
			var response0 = new SearchResponse<object>();
			// end::c98edce2074791ebad716b9a5b03215f[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""user_name"": { ""terms"" : { ""field"": ""user_name"" } } }, <1>
			                    { ""date"": { ""date_histogram"": { ""field"": ""timestamp"", ""calendar_interval"": ""1d"", ""order"": ""desc"" } } } <2>
			                ]
			            }
			        }
			     }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line646()
		{
			// tag::b2d0d2f519a37b93b93889be7979ee5d[]
			var response0 = new SearchResponse<object>();
			// end::b2d0d2f519a37b93b93889be7979ee5d[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
			    ""track_total_hits"": false,
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""user_name"": { ""terms"" : { ""field"": ""user_name"" } } },
			                    { ""date"": { ""date_histogram"": { ""field"": ""timestamp"", ""calendar_interval"": ""1d"", ""order"": ""desc"" } } }
			                ]
			            }
			        }
			     }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line685()
		{
			// tag::4a37d7d228d9cba63ebe7b9870dce531[]
			var response0 = new SearchResponse<object>();
			// end::4a37d7d228d9cba63ebe7b9870dce531[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
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