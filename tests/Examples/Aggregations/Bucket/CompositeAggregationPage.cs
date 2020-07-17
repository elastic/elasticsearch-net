// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Aggregations.Bucket
{
	public class CompositeAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/composite-aggregation.asciidoc:117")]
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:136")]
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:172")]
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:191")]
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:224")]
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:254")]
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:295")]
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:367")]
		public void Line367()
		{
			// tag::488f4a4da2d2d8e0b35cba9c0a11ffef[]
			var response0 = new SearchResponse<object>();
			// end::488f4a4da2d2d8e0b35cba9c0a11ffef[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    { ""tile"": { ""geotile_grid"" : { ""field"": ""location"", ""precision"": 8 } } }
			                ]
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("aggregations/bucket/composite-aggregation.asciidoc:400")]
		public void Line400()
		{
			// tag::5bc48d211cc95cb8962250f894da34a4[]
			var response0 = new SearchResponse<object>();
			// end::5bc48d211cc95cb8962250f894da34a4[]

			response0.MatchesExample(@"GET /_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""my_buckets"": {
			            ""composite"" : {
			                ""sources"" : [
			                    {
			                        ""tile"": {
			                            ""geotile_grid"" : {
			                                ""field"" : ""location"",
			                                ""precision"" : 22,
			                                ""bounds"": {
			                                    ""top_left"" : ""52.4, 4.9"",
			                                    ""bottom_right"" : ""52.3, 5.0""
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:434")]
		public void Line434()
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:457")]
		public void Line457()
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:487")]
		public void Line487()
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:514")]
		public void Line514()
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:554")]
		public void Line554()
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:612")]
		public void Line612()
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:644")]
		public void Line644()
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:673")]
		public void Line673()
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:692")]
		public void Line692()
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:717")]
		public void Line717()
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
		[Description("aggregations/bucket/composite-aggregation.asciidoc:756")]
		public void Line756()
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
