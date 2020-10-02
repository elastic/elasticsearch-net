// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Examples.Models;
using Nest;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace Examples.Indices
{
	public class PutMappingPage : ExampleBase
	{
		[U]
		[Description("indices/put-mapping.asciidoc:13")]
		public void Line13()
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
		[Description("indices/put-mapping.asciidoc:95")]
		public void Line95()
		{
			// tag::12433d2b637d002e8d5c9a1adce69d3b[]
			var putMappingResponse = client.Indices.Create("publications");
			// end::12433d2b637d002e8d5c9a1adce69d3b[]

			putMappingResponse.MatchesExample(@"PUT /publications");
		}

		[U]
		[Description("indices/put-mapping.asciidoc:103")]
		public void Line103()
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
		[Description("indices/put-mapping.asciidoc:120")]
		public void Line120()
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
		[Description("indices/put-mapping.asciidoc:155")]
		public void Line155()
		{
			// tag::d9474f66970c6955e24b17c7447e7b5f[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
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
		[Description("indices/put-mapping.asciidoc:177")]
		public void Line177()
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
		[Description("indices/put-mapping.asciidoc:197")]
		public void Line197()
		{
			// tag::210cf5c76bff517f48e80fa1c2d63907[]
			var getMappingResponse = client.Indices.GetMapping<object>(r => r.Index("my_index"));
			// end::210cf5c76bff517f48e80fa1c2d63907[]

			getMappingResponse.MatchesExample(@"GET /my_index/_mapping");
		}

		[U]
		[Description("indices/put-mapping.asciidoc:245")]
		public void Line245()
		{
			// tag::c849c6c8f8659dbb93e1c14356f74e37[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
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
		[Description("indices/put-mapping.asciidoc:268")]
		public void Line268()
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
		[Description("indices/put-mapping.asciidoc:338")]
		public void Line338()
		{
			// tag::1f6fe6833686e38c3711c6f2aa00a078[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
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
		[Description("indices/put-mapping.asciidoc:357")]
		public void Line357()
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
		[Description("indices/put-mapping.asciidoc:423")]
		public void Line423()
		{
			// tag::bd5918ab903c0889bb1f09c8c2466e43[]
			var createIndexResponse = client.Indices.Create("users", c => c
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
		[Description("indices/put-mapping.asciidoc:441")]
		public void Line441()
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
		[Description("indices/put-mapping.asciidoc:460")]
		public void Line460()
		{
			// tag::734c2e2a1e45b84f1e4e65b51356fcd7[]
			var createIndexResponse = client.Indices.Create("new_users", c => c
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
		[Description("indices/put-mapping.asciidoc:479")]
		public void Line479()
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
		[Description("indices/put-mapping.asciidoc:533")]
		public void Line533()
		{
			// tag::6bf63f2ec6ba55fcaf1092f48212bf25[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
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
		[Description("indices/put-mapping.asciidoc:550")]
		public void Line550()
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
