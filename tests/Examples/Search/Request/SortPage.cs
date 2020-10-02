// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using Elasticsearch.Net;
using Examples.Models;
using Newtonsoft.Json.Linq;

namespace Examples.Search.Request
{
	public class SortPage : ExampleBase
	{
		[U]
		[Description("search/request/sort.asciidoc:11")]
		public void Line11()
		{
			// tag::d1b3b7d2bb2ab90d15fd10318abd24db[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map<Tweet>(m => m
					.Properties(p => p
						.Date(d => d
							.Name(n => n.PostDate)
						)
						.Keyword(k => k
							.Name(n => n.User)
						)
						.Keyword(k => k
							.Name(n => n.Name)
						)
						.Number(n => n
							.Name(nn => nn.Age)
							.Type(NumberType.Integer)
						)
					)
				)
			);
			// end::d1b3b7d2bb2ab90d15fd10318abd24db[]

			createIndexResponse.MatchesExample(@"PUT /my_index
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

		[U]
		[Description("search/request/sort.asciidoc:30")]
		public void Line30()
		{
			// tag::ae9b5fbd42af2386ffbf56ad4a697e51[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("my_index")
				.Sort(so => so
					.Ascending(f => f.PostDate)
					.Field(f => f.Field("user"))
					.Descending(f => f.Name)
					.Descending(f => f.Age)
					.Field(f => f.Field("_score"))
				)
				.Query(q => q
					.Term(t => t
						.Field(f => f.User)
						.Value("kimchy")
					)
				)
			);
			// end::ae9b5fbd42af2386ffbf56ad4a697e51[]

			searchResponse.MatchesExample(@"GET /my_index/_search
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
			}", (e, body) =>
			{
				body["query"]["term"]["user"].ToLongFormTermQuery();

				// the client doesn't support many of the sort short forms
				body["sort"][1] = new JObject { { "user", new JObject() } };
				body["sort"][2] = new JObject { { "name", new JObject { { "order", "desc" } } } };
				body["sort"][3] = new JObject { { "age", new JObject { { "order", "desc" } } } };
				body["sort"][4] = new JObject { { "_score", new JObject() } };
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:94")]
		public void Line94()
		{
			// tag::b997885974522ef439d5e345924cc5ba[]
			var indexResponse = client.Index(new
			{
				product = "chocolate",
				price = new[] { 20, 4 }
			}, i => i
				.Index("my_index")
				.Id(1)
				.Refresh(Refresh.True)
			);

			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Term(t => t
						.Field("product")
						.Value("chocolate")
					)
				)
				.Sort(so => so
					.Field(f => f
						.Field("price")
						.Order(SortOrder.Ascending)
						.Mode(SortMode.Average)
					)
				)
			);
			// end::b997885974522ef439d5e345924cc5ba[]

			indexResponse.MatchesExample(@"PUT /my_index/_doc/1?refresh
			{
			   ""product"": ""chocolate"",
			   ""price"": [20, 4]
			}");

			searchResponse.MatchesExample(@"POST /_search
			{
			   ""query"" : {
			      ""term"" : { ""product"" : ""chocolate"" }
			   },
			   ""sort"" : [
			      {""price"" : {""order"" : ""asc"", ""mode"" : ""avg""}}
			   ]
			}", (e, body) =>
			{
				body["query"]["term"]["product"].ToLongFormTermQuery();
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:122")]
		public void Line122()
		{
			// tag::abf329ebefaf58acd4ee30e685731499[]
			var createIndexResponse = client.Indices.Create("index_double", c => c
				.Map(m => m
					.Properties(p => p
						.Number(n => n
							.Name("field")
							.Type(NumberType.Double)
						)
					)
				)
			);
			// end::abf329ebefaf58acd4ee30e685731499[]

			createIndexResponse.MatchesExample(@"PUT /index_double
			{
			    ""mappings"": {
			        ""properties"": {
			            ""field"": { ""type"": ""double"" }
			        }
			    }
			}");
		}

		[U]
		[Description("search/request/sort.asciidoc:134")]
		public void Line134()
		{
			// tag::f6b5032bf27c2445d28845be0d413970[]
			var createIndexResponse = client.Indices.Create("index_long", c => c
				.Map(m => m
					.Properties(p => p
						.Number(n => n
							.Name("field")
							.Type(NumberType.Long)
						)
					)
				)
			);
			// end::f6b5032bf27c2445d28845be0d413970[]

			createIndexResponse.MatchesExample(@"PUT /index_long
			{
			    ""mappings"": {
			        ""properties"": {
			            ""field"": { ""type"": ""long"" }
			        }
			    }
			}");
		}

		[U]
		[Description("search/request/sort.asciidoc:153")]
		public void Line153()
		{
			// tag::2891aa10ee9d474780adf94d5607f2db[]
			var searchResponse = client.Search<object>(s => s
				.Index(new[] { "index_long", "index_double" })
				.Sort(so => so
					.Field(f => f
						.Field("field")
						.NumericType(NumericType.Double)
					)
				)
			);
			// end::2891aa10ee9d474780adf94d5607f2db[]

			searchResponse.MatchesExample(@"POST /index_long,index_double/_search
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

		[U]
		[Description("search/request/sort.asciidoc:180")]
		public void Line180()
		{
			// tag::f4a1008b3f9baa67bb03ce9ef5ab4cb4[]
			var createIndexResponse = client.Indices.Create("index_double", c => c
				.Map(m => m
					.Properties(p => p
						.Date(n => n
							.Name("field")
						)
					)
				)
			);
			// end::f4a1008b3f9baa67bb03ce9ef5ab4cb4[]

			createIndexResponse.MatchesExample(@"PUT /index_double
			{
			    ""mappings"": {
			        ""properties"": {
			            ""field"": { ""type"": ""date"" }
			        }
			    }
			}");
		}

		[U]
		[Description("search/request/sort.asciidoc:192")]
		public void Line192()
		{
			// tag::7477671958734843dd67cf0b8e6c7515[]
			var createIndexResponse = client.Indices.Create("index_long", c => c
				.Map(m => m
					.Properties(p => p
						.DateNanos(n => n
							.Name("field")
						)
					)
				)
			);
			// end::7477671958734843dd67cf0b8e6c7515[]

			createIndexResponse.MatchesExample(@"PUT /index_long
			{
			    ""mappings"": {
			        ""properties"": {
			            ""field"": { ""type"": ""date_nanos"" }
			        }
			    }
			}");
		}

		[U]
		[Description("search/request/sort.asciidoc:211")]
		public void Line211()
		{
			// tag::5f3549ac7fee94682ca0d7439eebdd2a[]
			var searchResponse = client.Search<object>(s => s
				.Index(new[] { "index_long", "index_double" })
				.Sort(so => so
					.Field(f => f
						.Field("field")
						.NumericType(NumericType.DateNanos)
					)
				)
			);
			// end::5f3549ac7fee94682ca0d7439eebdd2a[]

			searchResponse.MatchesExample(@"POST /index_long,index_double/_search
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

		[U]
		[Description("search/request/sort.asciidoc:262")]
		public void Line262()
		{
			// tag::de139866a220124360e5e27d1a736ea4[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Term(t => t
						.Field("product")
						.Value("chocolate")
					)
				)
				.Sort(so => so
					.Field(f => f
						.Field("offer.price")
						.Mode(SortMode.Average)
						.Order(SortOrder.Ascending)
						.Nested(ns => ns
							.Path("offer")
							.Filter(nf => nf
								.Term(tf => tf
									.Field("offer.color")
									.Value("blue")
								)
							)
						)
					)
				)
			);
			// end::de139866a220124360e5e27d1a736ea4[]

			searchResponse.MatchesExample(@"POST /_search
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
			}", (e, body) =>
			{
				body["query"]["term"]["product"].ToLongFormTermQuery();
				body["sort"][0]["offer.price"]["nested"]["filter"]["term"]["offer.color"].ToLongFormTermQuery();
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:289")]
		public void Line289()
		{
			// tag::22334f4b24bb8977d3e1bf2ffdc29d3f[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Nested(n => n
						.Path("parent")
						.Query(nq => nq
							.LongRange(r => r
								.Field("parent.age")
								.GreaterThanOrEquals(21)
							) && +nq
							.Nested(nn => nn
								.Path("parent.child")
								.Query(nnq => nnq
									.Match(m => m
										.Field("parent.child.name")
										.Query("matt")
									)
								)
							)
						)
					)
				)
				.Sort(so => so
					.Field(f => f
						.Field("parent.child.age")
						.Mode(SortMode.Min)
						.Order(SortOrder.Ascending)
						.Nested(ns => ns
							.Path("parent")
							.Filter(nf => nf
								.LongRange(tf => tf
									.Field("parent.age")
									.GreaterThanOrEquals(21)
								)
							)
							.Nested(nns => nns
								.Path("parent.child")
								.Filter(nnf => nnf
									.Match(m => m
										.Field("parent.child.name")
										.Query("matt")
									)
								)
							)
						)
					)
				)
			);
			// end::22334f4b24bb8977d3e1bf2ffdc29d3f[]

			searchResponse.MatchesExample(@"POST /_search
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
			}", (e, body) =>
			{
				body["query"]["nested"]["query"]["bool"].ToLongFormBoolQuery(b =>
				{
					b["filter"][0]["nested"]["query"]["match"]["parent.child.name"].ToLongFormQuery();
				});
				body["sort"][0]["parent.child.age"]["nested"]["nested"]["filter"]["match"]["parent.child.name"].ToLongFormQuery();
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:345")]
		public void Line345()
		{
			// tag::ef0f4fa4272c47ff62fb7b422cf975e7[]
			var response0 = client.Search<object>(s => s
				.AllIndices()
				.Sort(so => so
					.Field(f => f
						.Field("price")
						.MissingLast()
					)
				)
				.Query(q => q
					.Term(t => t
						.Field("product")
						.Value("chocolate")
					)
				)
			);
			// end::ef0f4fa4272c47ff62fb7b422cf975e7[]

			response0.MatchesExample(@"GET /_search
			{
			    ""sort"" : [
			        { ""price"" : {""missing"" : ""_last""} }
			    ],
			    ""query"" : {
			        ""term"" : { ""product"" : ""chocolate"" }
			    }
			}", (e, body) =>
			{
				body["query"]["term"]["product"].ToLongFormTermQuery();
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:369")]
		public void Line369()
		{
			// tag::899eef71a67a1b2aa11a2166ec7f48f1[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Sort(so => so
					.Field(f => f
						.Field("price")
						.UnmappedType(FieldType.Long)
					)
				)
				.Query(q => q
					.Term("product", "chocolate")
				)
			);
			// end::899eef71a67a1b2aa11a2166ec7f48f1[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""sort"" : [
			        { ""price"" : {""unmapped_type"" : ""long""} }
			    ],
			    ""query"" : {
			        ""term"" : { ""product"" : ""chocolate"" }
			    }
			}", (e, body) =>
			{
				body["query"]["term"]["product"].ToLongFormTermQuery();
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:391")]
		public void Line391()
		{
			// tag::d17269bb80fb63ec0bf37d219e003dcb[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Sort(so => so
					.GeoDistance(g => g
						.Field("pin.location")
						.Points(new GeoCoordinate(40, -70))
						.Order(SortOrder.Ascending)
						.Unit(DistanceUnit.Kilometers)
						.Mode(SortMode.Min)
						.DistanceType(GeoDistanceType.Arc)
						.IgnoreUnmapped()
					)
				)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::d17269bb80fb63ec0bf37d219e003dcb[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", (e, body) =>
			{
				var current = body["sort"][0]["_geo_distance"]["pin.location"];
				body["sort"][0]["_geo_distance"]["pin.location"] = new JArray(new JObject {
					{ "lat", current[1].Value<double>() },
					{ "lon", current[0].Value<double>() },
				});
				body["query"]["term"]["user"].ToLongFormTermQuery();
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:444")]
		public void Line444()
		{
			// tag::979d25dff2d8987119410291ad47b0d1[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Sort(so => so
					.GeoDistance(g => g
						.Field("pin.location")
						.Points(new GeoLocation(40, -70))
						.Order(SortOrder.Ascending)
						.Unit(DistanceUnit.Kilometers)
					)
				)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::979d25dff2d8987119410291ad47b0d1[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", (e, body) =>
			{
				body["sort"][0]["_geo_distance"]["pin.location"]["lat"] = 40d;
				body["sort"][0]["_geo_distance"]["pin.location"]["lon"] = -70d;
				body["sort"][0]["_geo_distance"]["pin.location"].ToJArray();
				body["query"]["term"]["user"].ToLongFormTermQuery();
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:470")]
		public void Line470()
		{
			// tag::d50a3c64890f88af32c6d4ef4899d82a[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Sort(so => so
					.GeoDistance(g => g
						.Field("pin.location")
						.Points(new GeoCoordinate(40, -70))
						.Order(SortOrder.Ascending)
						.Unit(DistanceUnit.Kilometers)
					)
				)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::d50a3c64890f88af32c6d4ef4899d82a[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", (e, body) =>
			{
				body["sort"][0]["_geo_distance"]["pin.location"] = new JArray(new JObject { { "lon", -70d }, { "lat", 40d } });
				body["query"]["term"]["user"].ToLongFormTermQuery();
			});
		}

		[U(Skip = "No representable with the client, which does not support geohashes")]
		[Description("search/request/sort.asciidoc:491")]
		public void Line491()
		{
			// tag::a1db5c822745fe167e9ef854dca3d129[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Sort(so => so
					.GeoDistance(g => g
						.Field("pin.location")
						.Points(new GeoCoordinate(40, -70))
						.Order(SortOrder.Ascending)
						.Unit(DistanceUnit.Kilometers)
					)
				)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::a1db5c822745fe167e9ef854dca3d129[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", (e, body) =>
			{
				body["sort"][0]["_geo_distance"]["pin.location"] = new JArray(new JObject { { "lat", -70d }, { "lon", 40d } });
				body["query"]["term"]["user"].ToLongFormTermQuery();
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:515")]
		public void Line515()
		{
			// tag::15dad5338065baaaa7d475abe85f4c22[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Sort(so => so
					.GeoDistance(g => g
						.Field("pin.location")
						.Points(new GeoCoordinate(40, -70))
						.Order(SortOrder.Ascending)
						.Unit(DistanceUnit.Kilometers)
					)
				)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::15dad5338065baaaa7d475abe85f4c22[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", (e, body) =>
			{
				body["query"]["term"]["user"].ToLongFormTermQuery();
				body["sort"][0]["_geo_distance"]["pin.location"] = new JArray(new JObject { { "lon", -70d }, { "lat", 40d } });
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:539")]
		public void Line539()
		{
			// tag::77243bbf92f2a55e0fca6c2a349a1c15[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Sort(so => so
					.GeoDistance(g => g
						.Field("pin.location")
						.Points(
							new GeoCoordinate(40, -70),
							new GeoCoordinate(42, -71)
						)
						.Order(SortOrder.Ascending)
						.Unit(DistanceUnit.Kilometers)
					)
				)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::77243bbf92f2a55e0fca6c2a349a1c15[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", (e, body) =>
			{
				body["query"]["term"]["user"].ToLongFormTermQuery();
				body["sort"][0]["_geo_distance"]["pin.location"] = new JArray(
					new JObject { { "lon", -70d }, { "lat", 40d } },
					new JObject { { "lon", -71d }, { "lat", 42d } }
				);
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:568")]
		public void Line568()
		{
			// tag::04fe1e3a0047b0cdb10987b79fc3f3f3[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Sort(so => so
					.Script(ss => ss
						.Type("number")
						.Script(sc => sc
							.Source("doc['field_name'].value * params.factor")
							.Lang("painless")
							.Params(p => p
								.Add("factor", 1.1)
							)
						)
						.Order(SortOrder.Ascending)
					)
				)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::04fe1e3a0047b0cdb10987b79fc3f3f3[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", (e, body) =>
			{
				body["query"]["term"]["user"].ToLongFormTermQuery();
				body["sort"].ToJArray();
			});
		}

		[U]
		[Description("search/request/sort.asciidoc:597")]
		public void Line597()
		{
			// tag::e8e451bc8c45bcf16df43804c4fc8329[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.TrackScores()
				.Sort(so => so
					.Descending("post_date")
					.Descending("name")
					.Descending("age")
				)
				.Query(q => q
					.Term("user", "kimchy")
				)
			);
			// end::e8e451bc8c45bcf16df43804c4fc8329[]

			searchResponse.MatchesExample(@"GET /_search
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
			}", (e, body) =>
			{
				body["query"]["term"]["user"].ToLongFormTermQuery();
				body["sort"][1]["name"] = new JObject { { "order", "desc" } };
				body["sort"][2]["age"] = new JObject { { "order", "desc" } };
			});
		}
	}
}
