using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Request
{
	public class SortPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::d1b3b7d2bb2ab90d15fd10318abd24db[]
			var response0 = new SearchResponse<object>();
			// end::d1b3b7d2bb2ab90d15fd10318abd24db[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""mappings"": {
			        ""properties"": {
			            ""post_date"": { ""type"": ""date"" },
			            ""user"": {
			                ""type"": ""keyword""
			            },
			            ""name"": {
			                ""type"": ""keyword""
			            },
			            ""age"": { ""type"": ""integer"" }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line30()
		{
			// tag::ae9b5fbd42af2386ffbf56ad4a697e51[]
			var response0 = new SearchResponse<object>();
			// end::ae9b5fbd42af2386ffbf56ad4a697e51[]

			response0.MatchesExample(@"GET /my_index/_search
			{
			    ""sort"" : [
			        { ""post_date"" : {""order"" : ""asc""}},
			        ""user"",
			        { ""name"" : ""desc"" },
			        { ""age"" : ""desc"" },
			        ""_score""
			    ],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line94()
		{
			// tag::b997885974522ef439d5e345924cc5ba[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::b997885974522ef439d5e345924cc5ba[]

			response0.MatchesExample(@"PUT /my_index/_doc/1?refresh
			{
			   ""product"": ""chocolate"",
			   ""price"": [20, 4]
			}");

			response1.MatchesExample(@"POST /_search
			{
			   ""query"" : {
			      ""term"" : { ""product"" : ""chocolate"" }
			   },
			   ""sort"" : [
			      {""price"" : {""order"" : ""asc"", ""mode"" : ""avg""}}
			   ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line123()
		{
			// tag::abf329ebefaf58acd4ee30e685731499[]
			var response0 = new SearchResponse<object>();
			// end::abf329ebefaf58acd4ee30e685731499[]

			response0.MatchesExample(@"PUT /index_double
			{
			    ""mappings"": {
			        ""properties"": {
			            ""field"": { ""type"": ""double"" }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line135()
		{
			// tag::f6b5032bf27c2445d28845be0d413970[]
			var response0 = new SearchResponse<object>();
			// end::f6b5032bf27c2445d28845be0d413970[]

			response0.MatchesExample(@"PUT /index_long
			{
			    ""mappings"": {
			        ""properties"": {
			            ""field"": { ""type"": ""long"" }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line154()
		{
			// tag::2891aa10ee9d474780adf94d5607f2db[]
			var response0 = new SearchResponse<object>();
			// end::2891aa10ee9d474780adf94d5607f2db[]

			response0.MatchesExample(@"POST /index_long,index_double/_search
			{
			   ""sort"" : [
			      {
			        ""field"" : {
			            ""numeric_type"" : ""double""
			        }
			      }
			   ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line181()
		{
			// tag::f4a1008b3f9baa67bb03ce9ef5ab4cb4[]
			var response0 = new SearchResponse<object>();
			// end::f4a1008b3f9baa67bb03ce9ef5ab4cb4[]

			response0.MatchesExample(@"PUT /index_double
			{
			    ""mappings"": {
			        ""properties"": {
			            ""field"": { ""type"": ""date"" }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line193()
		{
			// tag::7477671958734843dd67cf0b8e6c7515[]
			var response0 = new SearchResponse<object>();
			// end::7477671958734843dd67cf0b8e6c7515[]

			response0.MatchesExample(@"PUT /index_long
			{
			    ""mappings"": {
			        ""properties"": {
			            ""field"": { ""type"": ""date_nanos"" }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line212()
		{
			// tag::5f3549ac7fee94682ca0d7439eebdd2a[]
			var response0 = new SearchResponse<object>();
			// end::5f3549ac7fee94682ca0d7439eebdd2a[]

			response0.MatchesExample(@"POST /index_long,index_double/_search
			{
			   ""sort"" : [
			      {
			        ""field"" : {
			            ""numeric_type"" : ""date_nanos""
			        }
			      }
			   ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line263()
		{
			// tag::de139866a220124360e5e27d1a736ea4[]
			var response0 = new SearchResponse<object>();
			// end::de139866a220124360e5e27d1a736ea4[]

			response0.MatchesExample(@"POST /_search
			{
			   ""query"" : {
			      ""term"" : { ""product"" : ""chocolate"" }
			   },
			   ""sort"" : [
			       {
			          ""offer.price"" : {
			             ""mode"" :  ""avg"",
			             ""order"" : ""asc"",
			             ""nested"": {
			                ""path"": ""offer"",
			                ""filter"": {
			                   ""term"" : { ""offer.color"" : ""blue"" }
			                }
			             }
			          }
			       }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line290()
		{
			// tag::22334f4b24bb8977d3e1bf2ffdc29d3f[]
			var response0 = new SearchResponse<object>();
			// end::22334f4b24bb8977d3e1bf2ffdc29d3f[]

			response0.MatchesExample(@"POST /_search
			{
			   ""query"": {
			      ""nested"": {
			         ""path"": ""parent"",
			         ""query"": {
			            ""bool"": {
			                ""must"": {""range"": {""parent.age"": {""gte"": 21}}},
			                ""filter"": {
			                    ""nested"": {
			                        ""path"": ""parent.child"",
			                        ""query"": {""match"": {""parent.child.name"": ""matt""}}
			                    }
			                }
			            }
			         }
			      }
			   },
			   ""sort"" : [
			      {
			         ""parent.child.age"" : {
			            ""mode"" :  ""min"",
			            ""order"" : ""asc"",
			            ""nested"": {
			               ""path"": ""parent"",
			               ""filter"": {
			                  ""range"": {""parent.age"": {""gte"": 21}}
			               },
			               ""nested"": {
			                  ""path"": ""parent.child"",
			                  ""filter"": {
			                     ""match"": {""parent.child.name"": ""matt""}
			                  }
			               }
			            }
			         }
			      }
			   ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line346()
		{
			// tag::ef0f4fa4272c47ff62fb7b422cf975e7[]
			var response0 = new SearchResponse<object>();
			// end::ef0f4fa4272c47ff62fb7b422cf975e7[]

			response0.MatchesExample(@"GET /_search
			{
			    ""sort"" : [
			        { ""price"" : {""missing"" : ""_last""} }
			    ],
			    ""query"" : {
			        ""term"" : { ""product"" : ""chocolate"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line370()
		{
			// tag::899eef71a67a1b2aa11a2166ec7f48f1[]
			var response0 = new SearchResponse<object>();
			// end::899eef71a67a1b2aa11a2166ec7f48f1[]

			response0.MatchesExample(@"GET /_search
			{
			    ""sort"" : [
			        { ""price"" : {""unmapped_type"" : ""long""} }
			    ],
			    ""query"" : {
			        ""term"" : { ""product"" : ""chocolate"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line392()
		{
			// tag::d17269bb80fb63ec0bf37d219e003dcb[]
			var response0 = new SearchResponse<object>();
			// end::d17269bb80fb63ec0bf37d219e003dcb[]

			response0.MatchesExample(@"GET /_search
			{
			    ""sort"" : [
			        {
			            ""_geo_distance"" : {
			                ""pin.location"" : [-70, 40],
			                ""order"" : ""asc"",
			                ""unit"" : ""km"",
			                ""mode"" : ""min"",
			                ""distance_type"" : ""arc"",
			                ""ignore_unmapped"": true
			            }
			        }
			    ],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line445()
		{
			// tag::979d25dff2d8987119410291ad47b0d1[]
			var response0 = new SearchResponse<object>();
			// end::979d25dff2d8987119410291ad47b0d1[]

			response0.MatchesExample(@"GET /_search
			{
			    ""sort"" : [
			        {
			            ""_geo_distance"" : {
			                ""pin.location"" : {
			                    ""lat"" : 40,
			                    ""lon"" : -70
			                },
			                ""order"" : ""asc"",
			                ""unit"" : ""km""
			            }
			        }
			    ],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line471()
		{
			// tag::d50a3c64890f88af32c6d4ef4899d82a[]
			var response0 = new SearchResponse<object>();
			// end::d50a3c64890f88af32c6d4ef4899d82a[]

			response0.MatchesExample(@"GET /_search
			{
			    ""sort"" : [
			        {
			            ""_geo_distance"" : {
			                ""pin.location"" : ""40,-70"",
			                ""order"" : ""asc"",
			                ""unit"" : ""km""
			            }
			        }
			    ],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line492()
		{
			// tag::a1db5c822745fe167e9ef854dca3d129[]
			var response0 = new SearchResponse<object>();
			// end::a1db5c822745fe167e9ef854dca3d129[]

			response0.MatchesExample(@"GET /_search
			{
			    ""sort"" : [
			        {
			            ""_geo_distance"" : {
			                ""pin.location"" : ""drm3btev3e86"",
			                ""order"" : ""asc"",
			                ""unit"" : ""km""
			            }
			        }
			    ],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line516()
		{
			// tag::15dad5338065baaaa7d475abe85f4c22[]
			var response0 = new SearchResponse<object>();
			// end::15dad5338065baaaa7d475abe85f4c22[]

			response0.MatchesExample(@"GET /_search
			{
			    ""sort"" : [
			        {
			            ""_geo_distance"" : {
			                ""pin.location"" : [-70, 40],
			                ""order"" : ""asc"",
			                ""unit"" : ""km""
			            }
			        }
			    ],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line540()
		{
			// tag::77243bbf92f2a55e0fca6c2a349a1c15[]
			var response0 = new SearchResponse<object>();
			// end::77243bbf92f2a55e0fca6c2a349a1c15[]

			response0.MatchesExample(@"GET /_search
			{
			    ""sort"" : [
			        {
			            ""_geo_distance"" : {
			                ""pin.location"" : [[-70, 40], [-71, 42]],
			                ""order"" : ""asc"",
			                ""unit"" : ""km""
			            }
			        }
			    ],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line569()
		{
			// tag::04fe1e3a0047b0cdb10987b79fc3f3f3[]
			var response0 = new SearchResponse<object>();
			// end::04fe1e3a0047b0cdb10987b79fc3f3f3[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    },
			    ""sort"" : {
			        ""_script"" : {
			            ""type"" : ""number"",
			            ""script"" : {
			                ""lang"": ""painless"",
			                ""source"": ""doc['field_name'].value * params.factor"",
			                ""params"" : {
			                    ""factor"" : 1.1
			                }
			            },
			            ""order"" : ""asc""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line598()
		{
			// tag::e8e451bc8c45bcf16df43804c4fc8329[]
			var response0 = new SearchResponse<object>();
			// end::e8e451bc8c45bcf16df43804c4fc8329[]

			response0.MatchesExample(@"GET /_search
			{
			    ""track_scores"": true,
			    ""sort"" : [
			        { ""post_date"" : {""order"" : ""desc""} },
			        { ""name"" : ""desc"" },
			        { ""age"" : ""desc"" }
			    ],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}