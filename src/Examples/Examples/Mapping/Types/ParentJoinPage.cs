using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class ParentJoinPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line14()
		{
			// tag::59a6a91a43e92b9f7035eadae9e1b8b9[]
			var response0 = new SearchResponse<object>();
			// end::59a6a91a43e92b9f7035eadae9e1b8b9[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_join_field"": { \<1>
			        ""type"": ""join"",
			        ""relations"": {
			          ""question"": ""answer"" \<2>
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line38()
		{
			// tag::3a9297c0898dfe7b38da82635b7dc1ff[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::3a9297c0898dfe7b38da82635b7dc1ff[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""text"": ""This is a question"",
			  ""my_join_field"": {
			    ""name"": ""question"" \<1>
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""text"": ""This is another question"",
			  ""my_join_field"": {
			    ""name"": ""question""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line63()
		{
			// tag::fcfe9592f9c8a59fe2b2110246b9a462[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::fcfe9592f9c8a59fe2b2110246b9a462[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""text"": ""This is a question"",
			  ""my_join_field"": ""question"" \<1>
			}");

			response1.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""text"": ""This is another question"",
			  ""my_join_field"": ""question""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line89()
		{
			// tag::1d13c92896ed8a8bd273773481c90a3c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::1d13c92896ed8a8bd273773481c90a3c[]

			response0.MatchesExample(@"PUT my_index/_doc/3?routing=1&refresh \<1>
			{
			  ""text"": ""This is an answer"",
			  ""my_join_field"": {
			    ""name"": ""answer"", \<2>
			    ""parent"": ""1"" \<3>
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/4?routing=1&refresh
			{
			  ""text"": ""This is another answer"",
			  ""my_join_field"": {
			    ""name"": ""answer"",
			    ""parent"": ""1""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line156()
		{
			// tag::a5e3a4c6dbda1f1cd7f22720ef362de2[]
			var response0 = new SearchResponse<object>();
			// end::a5e3a4c6dbda1f1cd7f22720ef362de2[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match_all"": {}
			  },
			  ""sort"": [""_id""]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line259()
		{
			// tag::26fe7b3c9aeab972725b6d708cc6df22[]
			var response0 = new SearchResponse<object>();
			// end::26fe7b3c9aeab972725b6d708cc6df22[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""parent_id"": { \<1>
			      ""type"": ""answer"",
			      ""id"": ""1""
			    }
			  },
			  ""aggs"": {
			    ""parents"": {
			      ""terms"": {
			        ""field"": ""my_join_field#question"", \<2>
			        ""size"": 10
			      }
			    }
			  },
			  ""script_fields"": {
			    ""parent"": {
			      ""script"": {
			         ""source"": ""doc['my_join_field#question']"" \<3>
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line312()
		{
			// tag::e0b414b45460d424ab838b5136492fa1[]
			var response0 = new SearchResponse<object>();
			// end::e0b414b45460d424ab838b5136492fa1[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_join_field"": {
			        ""type"": ""join"",
			        ""relations"": {
			           ""question"": ""answer""
			        },
			        ""eager_global_ordinals"": false
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line333()
		{
			// tag::2c090fe7ec7b66b3f5c178d71c46323b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::2c090fe7ec7b66b3f5c178d71c46323b[]

			response0.MatchesExample(@"# Per-index");

			response1.MatchesExample(@"GET _stats/fielddata?human&fields=my_join_field#question");

			response2.MatchesExample(@"# Per-node per-index");

			response3.MatchesExample(@"GET _nodes/stats/indices/fielddata?human&fields=my_join_field#question");
		}

		[U(Skip = "Example not implemented")]
		public void Line347()
		{
			// tag::bc358cfd219faf9353cb65820981a0df[]
			var response0 = new SearchResponse<object>();
			// end::bc358cfd219faf9353cb65820981a0df[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_join_field"": {
			        ""type"": ""join"",
			        ""relations"": {
			          ""question"": [""answer"", ""comment""]  \<1>
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line374()
		{
			// tag::1cc03b9715d9a3f876f7b7bb7fe66394[]
			var response0 = new SearchResponse<object>();
			// end::1cc03b9715d9a3f876f7b7bb7fe66394[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_join_field"": {
			        ""type"": ""join"",
			        ""relations"": {
			          ""question"": [""answer"", ""comment""],  \<1>
			          ""answer"": ""vote"" \<2>
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line409()
		{
			// tag::6eecf0fbf95d132beb0f49b3181da419[]
			var response0 = new SearchResponse<object>();
			// end::6eecf0fbf95d132beb0f49b3181da419[]

			response0.MatchesExample(@"PUT my_index/_doc/3?routing=1&refresh \<1>
			{
			  ""text"": ""This is a vote"",
			  ""my_join_field"": {
			    ""name"": ""vote"",
			    ""parent"": ""2"" \<2>
			  }
			}");
		}
	}
}