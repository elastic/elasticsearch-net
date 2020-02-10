using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Examples.Models;
using Nest;
using Newtonsoft.Json.Linq;

namespace Examples.Indices
{
	public class PutMappingPage : ExampleBase
	{
		[U]
		public void Line11()
		{
			// tag::5be23858b35043fcb7b50fe36b873e6e[]
			var putMappingResponse = client.Map<Tweet>(m => m
					.Index("twitter")
					.Properties(p =>
						p.Keyword(k => k.Name(t => t.Email))
					)
				);
			// end::5be23858b35043fcb7b50fe36b873e6e[]

			putMappingResponse.MatchesExample(@"PUT /twitter/_mapping
			{
			  ""properties"": {
			    ""email"": {
			      ""type"": ""keyword""
			    }
			  }
			}");
		}

		[U]
		public void Line90()
		{
			// tag::12433d2b637d002e8d5c9a1adce69d3b[]
			var putMappingResponse = client.Indices.Create("publications");
			// end::12433d2b637d002e8d5c9a1adce69d3b[]

			putMappingResponse.MatchesExample(@"PUT /publications");
		}

		[U]
		public void Line98()
		{
			// tag::e4be53736bcc02b03068fd72fdbfe271[]
			var putMappingResponse = client.Map<object>(m => m
				.Index("publications")
				.Properties(p =>
					p.Text(k => k.Name("title"))
				)
			);
			// end::e4be53736bcc02b03068fd72fdbfe271[]

			putMappingResponse.MatchesExample(@"PUT /publications/_mapping
			{
			  ""properties"": {
			    ""title"":  { ""type"": ""text""}
			  }
			}");
		}

		[U]
		public void Line115()
		{
			// tag::1da77e114459e0b77d78a3dcc8fae429[]
			var createIndex1Response = client.Indices.Create("twitter-1");

			var createIndex2Response = client.Indices.Create("twitter-2");

			var putMappingResponse = client.Map<Tweet>(m => m
				.Index("twitter-1,twitter-2")
				.Properties(p =>
					p.Text(k => k.Name(t => t.UserName))
				)
			);
			// end::1da77e114459e0b77d78a3dcc8fae429[]

			createIndex1Response.MatchesExample(@"PUT /twitter-1");

			createIndex2Response.MatchesExample(@"PUT /twitter-2");

			putMappingResponse.MatchesExample(@"PUT /twitter-1,twitter-2/_mapping <1>
			{
			  ""properties"": {
			    ""user_name"": {
			      ""type"": ""text""
			    }
			  }
			}");
		}

		[U]
		public void Line150()
		{
			// tag::d9474f66970c6955e24b17c7447e7b5f[]
			var createIndexResponse = client.Indices.Create("my_index", m => m
				.Map(m => m
					.Properties(pp => pp
						.Object<object>(o => o
							.Name("name")
							.Properties(p => p
								.Text(t => t.Name("first"))
							)
						)
					)
				)
			);
			// end::d9474f66970c6955e24b17c7447e7b5f[]

			createIndexResponse.MatchesExample(@"PUT /my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""name"": {
			        ""properties"": {
			          ""first"": {
			            ""type"": ""text""
			          }
			        }
			      }
			    }
			  }
			}", (e, body) =>
			{
				var value = body["mappings"]["properties"]["name"];
				var nameToken = JObject.Parse(value.ToString());
				nameToken.Add("type", "object");
				body["mappings"]["properties"]["name"] = nameToken;
			});
		}

		[U]
		public void Line172()
		{
			// tag::0bbd30b9be3e54ff3028b9f4459634d2[]
			var putMappingResponse = client.Map<object>(m => m
				.Index("my_index")
					.Properties(pp => pp
						.Object<object>(o => o
							.Name("name")
							.Properties(p => p
								.Text(t => t.Name("last"))
							)
						)
					)
			);
			// end::0bbd30b9be3e54ff3028b9f4459634d2[]

			putMappingResponse.MatchesExample(@"PUT /my_index/_mapping
			{
			  ""properties"": {
			    ""name"": {
			      ""properties"": {
			        ""last"": {
			          ""type"": ""text""
			        }
			      }
			    }
			  }
			}", (e, body) =>
			{
				var value = body["properties"]["name"];
				var nameToken = JObject.Parse(value.ToString());
				nameToken.Add("type", "object");
				body["properties"]["name"] = nameToken;
			});
		}

		[U]
		public void Line192()
		{
			// tag::210cf5c76bff517f48e80fa1c2d63907[]
			var getMappingResponse = client.Indices.GetMapping<object>(r => r.Index("my_index"));
			// end::210cf5c76bff517f48e80fa1c2d63907[]

			getMappingResponse.MatchesExample(@"GET /my_index/_mapping");
		}

		[U]
		public void Line240()
		{
			// tag::c849c6c8f8659dbb93e1c14356f74e37[]
			var createIndexResponse = client.Indices.Create("my_index", m => m
				.Map(m => m
					.Properties(pp => pp
						.Text(t => t.Name("city"))
					)
				)
			);
			// end::c849c6c8f8659dbb93e1c14356f74e37[]

			createIndexResponse.MatchesExample(@"PUT /my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""city"": {
			        ""type"": ""text""
			      }
			    }
			  }
			}");
		}

		[U]
		public void Line263()
		{
			// tag::5f3a3eefeefe6fa85ec49d499212d245[]
			var putMappingResponse = client.Map<object>(m => m
				.Index("my_index")
				.Properties(pp => pp
					.Text(t => t
						.Name("city")
						.Fields(f => f
							.Keyword(k => k.Name("raw"))
						)
					)
				)
			);
			// end::5f3a3eefeefe6fa85ec49d499212d245[]

			putMappingResponse.MatchesExample(@"PUT /my_index/_mapping
			{
			  ""properties"": {
			    ""city"": {
			      ""type"": ""text"",
			      ""fields"": {
			        ""raw"": {
			          ""type"": ""keyword""
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		public void Line333()
		{
			// tag::1f6fe6833686e38c3711c6f2aa00a078[]
			var createIndexResponse = client.Indices.Create("my_index", m => m
				.Map(m => m
					.Properties(pp => pp
						.Keyword(t => t
							.Name("user_id")
							.IgnoreAbove(20)
						)
					)
				)
			);
			// end::1f6fe6833686e38c3711c6f2aa00a078[]

			createIndexResponse.MatchesExample(@"PUT /my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""user_id"": {
			        ""type"": ""keyword"",
			        ""ignore_above"": 20
			      }
			    }
			  }
			}");
		}

		[U]
		public void Line352()
		{
			// tag::17de0020b228df961ad3c6b06233c948[]
			var putMappingResponse = client.Map<object>(m => m
				.Index("my_index")
				.Properties(pp => pp
					.Keyword(k => k
						.Name("user_id")
						.IgnoreAbove(100)
					)
				)
			);
			// end::17de0020b228df961ad3c6b06233c948[]

			putMappingResponse.MatchesExample(@"PUT /my_index/_mapping
			{
			  ""properties"": {
			    ""user_id"": {
			      ""type"": ""keyword"",
			      ""ignore_above"": 100
			    }
			  }
			}");
		}

		[U]
		public void Line415()
		{
			// tag::bd5918ab903c0889bb1f09c8c2466e43[]
			var createIndexResponse = client.Indices.Create("users", m => m
				.Map(m => m
					.Properties(pp => pp
						.Number(t => t
							.Name("user_id")
							.Type(NumberType.Long)
						)
					)
				)
			);
			// end::bd5918ab903c0889bb1f09c8c2466e43[]

			createIndexResponse.MatchesExample(@"PUT /users
			{
			  ""mappings"" : {
			    ""properties"": {
			      ""user_id"": {
			        ""type"": ""long""
			      }
			    }
			  }
			}");
		}

		[U]
		public void Line433()
		{
			// tag::0989cc65d8924f666ce3eb0820d2d244[]
			var indexResponse1 = client.Index<object>(new { user_id = 12345 }, r => r.Index("users").Refresh(Refresh.WaitFor));

			var indexResponse2 = client.Index<object>(new { user_id = 12346 }, r => r.Index("users").Refresh(Refresh.WaitFor));
			// end::0989cc65d8924f666ce3eb0820d2d244[]

			indexResponse1.MatchesExample(@"POST /users/_doc?refresh=wait_for
			{
			    ""user_id"" : 12345
			}");

			indexResponse2.MatchesExample(@"POST /users/_doc?refresh=wait_for
			{
			    ""user_id"" : 12346
			}");
		}

		[U]
		public void Line452()
		{
			// tag::734c2e2a1e45b84f1e4e65b51356fcd7[]
			var createIndexResponse = client.Indices.Create("new_users", m => m
				.Map(m => m
					.Properties(pp => pp
						.Keyword(t => t
							.Name("user_id")
						)
					)
				)
			);
			// end::734c2e2a1e45b84f1e4e65b51356fcd7[]

			createIndexResponse.MatchesExample(@"PUT /new_users
			{
			  ""mappings"" : {
			    ""properties"": {
			      ""user_id"": {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");
		}

		[U]
		public void Line471()
		{
			// tag::53d938c754f36a912fcbe6473abb463f[]
			var reindexOnServerResponse = client.ReindexOnServer(r => r
				.Source(s => s.Index("users"))
				.Destination(d => d.Index("new_users"))
			);
			// end::53d938c754f36a912fcbe6473abb463f[]

			reindexOnServerResponse.MatchesExample(@"POST /_reindex
			{
			  ""source"": {
			    ""index"": ""users""
			  },
			  ""dest"": {
			    ""index"": ""new_users""
			  }
			}");
		}

		[U]
		public void Line525()
		{
			// tag::6bf63f2ec6ba55fcaf1092f48212bf25[]
			var createIndexResponse = client.Indices.Create("my_index", m => m
				.Map(m => m
					.Properties(pp => pp
						.Keyword(t => t
							.Name("user_identifier")
						)
					)
				)
			);
			// end::6bf63f2ec6ba55fcaf1092f48212bf25[]

			createIndexResponse.MatchesExample(@"PUT /my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""user_identifier"": {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");
		}

		[U]
		public void Line542()
		{
			// tag::afc29b61c532cf683f749baf013e7bfe[]
			var putMappingResponse = client.Map<object>(m => m
				.Index("my_index")
				.Properties(p => p
					.FieldAlias(k => k
						.Name("user_id")
						.Path("user_identifier")
					)
				)
			);
			// end::afc29b61c532cf683f749baf013e7bfe[]

			putMappingResponse.MatchesExample(@"PUT /my_index/_mapping
			{
			  ""properties"": {
			    ""user_id"": {
			      ""type"": ""alias"",
			      ""path"": ""user_identifier""
			    }
			  }
			}");
		}
	}
}
