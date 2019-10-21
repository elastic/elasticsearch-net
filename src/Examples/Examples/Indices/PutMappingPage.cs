using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class PutMappingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::5be23858b35043fcb7b50fe36b873e6e[]
			var response0 = new SearchResponse<object>();
			// end::5be23858b35043fcb7b50fe36b873e6e[]

			response0.MatchesExample(@"PUT /twitter/_mapping
			{
			  ""properties"": {
			    ""email"": {
			      ""type"": ""keyword""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line90()
		{
			// tag::12433d2b637d002e8d5c9a1adce69d3b[]
			var response0 = new SearchResponse<object>();
			// end::12433d2b637d002e8d5c9a1adce69d3b[]

			response0.MatchesExample(@"PUT /publications");
		}

		[U(Skip = "Example not implemented")]
		public void Line98()
		{
			// tag::e4be53736bcc02b03068fd72fdbfe271[]
			var response0 = new SearchResponse<object>();
			// end::e4be53736bcc02b03068fd72fdbfe271[]

			response0.MatchesExample(@"PUT /publications/_mapping
			{
			  ""properties"": {
			    ""title"":  { ""type"": ""text""}
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line115()
		{
			// tag::1da77e114459e0b77d78a3dcc8fae429[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();
			// end::1da77e114459e0b77d78a3dcc8fae429[]

			response0.MatchesExample(@"# Create the two indices");

			response1.MatchesExample(@"PUT /twitter-1");

			response2.MatchesExample(@"PUT /twitter-2");

			response3.MatchesExample(@"# Update both mappings");

			response4.MatchesExample(@"PUT /twitter-1,twitter-2/_mapping <1>
			{
			  ""properties"": {
			    ""user_name"": {
			      ""type"": ""text""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line150()
		{
			// tag::d9474f66970c6955e24b17c7447e7b5f[]
			var response0 = new SearchResponse<object>();
			// end::d9474f66970c6955e24b17c7447e7b5f[]

			response0.MatchesExample(@"PUT /my_index
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
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line172()
		{
			// tag::0bbd30b9be3e54ff3028b9f4459634d2[]
			var response0 = new SearchResponse<object>();
			// end::0bbd30b9be3e54ff3028b9f4459634d2[]

			response0.MatchesExample(@"PUT /my_index/_mapping
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
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line192()
		{
			// tag::210cf5c76bff517f48e80fa1c2d63907[]
			var response0 = new SearchResponse<object>();
			// end::210cf5c76bff517f48e80fa1c2d63907[]

			response0.MatchesExample(@"GET /my_index/_mapping");
		}

		[U(Skip = "Example not implemented")]
		public void Line240()
		{
			// tag::c849c6c8f8659dbb93e1c14356f74e37[]
			var response0 = new SearchResponse<object>();
			// end::c849c6c8f8659dbb93e1c14356f74e37[]

			response0.MatchesExample(@"PUT /my_index
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

		[U(Skip = "Example not implemented")]
		public void Line263()
		{
			// tag::5f3a3eefeefe6fa85ec49d499212d245[]
			var response0 = new SearchResponse<object>();
			// end::5f3a3eefeefe6fa85ec49d499212d245[]

			response0.MatchesExample(@"PUT /my_index/_mapping
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

		[U(Skip = "Example not implemented")]
		public void Line333()
		{
			// tag::1f6fe6833686e38c3711c6f2aa00a078[]
			var response0 = new SearchResponse<object>();
			// end::1f6fe6833686e38c3711c6f2aa00a078[]

			response0.MatchesExample(@"PUT /my_index
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

		[U(Skip = "Example not implemented")]
		public void Line352()
		{
			// tag::17de0020b228df961ad3c6b06233c948[]
			var response0 = new SearchResponse<object>();
			// end::17de0020b228df961ad3c6b06233c948[]

			response0.MatchesExample(@"PUT /my_index/_mapping
			{
			  ""properties"": {
			    ""user_id"": {
			      ""type"": ""keyword"",
			      ""ignore_above"": 100
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line415()
		{
			// tag::bd5918ab903c0889bb1f09c8c2466e43[]
			var response0 = new SearchResponse<object>();
			// end::bd5918ab903c0889bb1f09c8c2466e43[]

			response0.MatchesExample(@"PUT /users
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

		[U(Skip = "Example not implemented")]
		public void Line433()
		{
			// tag::0989cc65d8924f666ce3eb0820d2d244[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::0989cc65d8924f666ce3eb0820d2d244[]

			response0.MatchesExample(@"POST /users/_doc?refresh=wait_for
			{
			    ""user_id"" : 12345
			}");

			response1.MatchesExample(@"POST /users/_doc?refresh=wait_for
			{
			    ""user_id"" : 12346
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line452()
		{
			// tag::734c2e2a1e45b84f1e4e65b51356fcd7[]
			var response0 = new SearchResponse<object>();
			// end::734c2e2a1e45b84f1e4e65b51356fcd7[]

			response0.MatchesExample(@"PUT /new_users
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

		[U(Skip = "Example not implemented")]
		public void Line471()
		{
			// tag::53d938c754f36a912fcbe6473abb463f[]
			var response0 = new SearchResponse<object>();
			// end::53d938c754f36a912fcbe6473abb463f[]

			response0.MatchesExample(@"POST /_reindex
			{
			  ""source"": {
			    ""index"": ""users""
			  },
			  ""dest"": {
			    ""index"": ""new_users""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line525()
		{
			// tag::6bf63f2ec6ba55fcaf1092f48212bf25[]
			var response0 = new SearchResponse<object>();
			// end::6bf63f2ec6ba55fcaf1092f48212bf25[]

			response0.MatchesExample(@"PUT /my_index
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

		[U(Skip = "Example not implemented")]
		public void Line542()
		{
			// tag::afc29b61c532cf683f749baf013e7bfe[]
			var response0 = new SearchResponse<object>();
			// end::afc29b61c532cf683f749baf013e7bfe[]

			response0.MatchesExample(@"PUT /my_index/_mapping
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