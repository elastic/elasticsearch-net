// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping.Types
{
	public class ParentJoinPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/parent-join.asciidoc:14")]
		public void Line14()
		{
			// tag::adbb85423739e45e6d072fd6bebb140e[]
			var response0 = new SearchResponse<object>();
			// end::adbb85423739e45e6d072fd6bebb140e[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_id"": {
			        ""type"": ""keyword""
			      },
			      ""my_join_field"": { <1>
			        ""type"": ""join"",
			        ""relations"": {
			          ""question"": ""answer"" <2>
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/parent-join.asciidoc:41")]
		public void Line41()
		{
			// tag::ce09baf41be8157b688e19e36b6050c9[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::ce09baf41be8157b688e19e36b6050c9[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""my_id"": ""1"",
			  ""text"": ""This is a question"",
			  ""my_join_field"": {
			    ""name"": ""question"" <1>
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""my_id"": ""2"",
			  ""text"": ""This is another question"",
			  ""my_join_field"": {
			    ""name"": ""question""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/parent-join.asciidoc:68")]
		public void Line68()
		{
			// tag::34a90fc67bf423c562cfbc91ca1016cf[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::34a90fc67bf423c562cfbc91ca1016cf[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""my_id"": ""1"",
			  ""text"": ""This is a question"",
			  ""my_join_field"": ""question"" <1>
			}");

			response1.MatchesExample(@"PUT my_index/_doc/2?refresh
			{
			  ""my_id"": ""2"",
			  ""text"": ""This is another question"",
			  ""my_join_field"": ""question""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/parent-join.asciidoc:96")]
		public void Line96()
		{
			// tag::f2b074b37e37cc12abf1b5c795965912[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::f2b074b37e37cc12abf1b5c795965912[]

			response0.MatchesExample(@"PUT my_index/_doc/3?routing=1&refresh <1>
			{
			  ""my_id"": ""3"",
			  ""text"": ""This is an answer"",
			  ""my_join_field"": {
			    ""name"": ""answer"", <2>
			    ""parent"": ""1"" <3>
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/4?routing=1&refresh
			{
			  ""my_id"": ""4"",
			  ""text"": ""This is another answer"",
			  ""my_join_field"": {
			    ""name"": ""answer"",
			    ""parent"": ""1""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/parent-join.asciidoc:165")]
		public void Line165()
		{
			// tag::275353b0245fde574d0b11f2aba2836e[]
			var response0 = new SearchResponse<object>();
			// end::275353b0245fde574d0b11f2aba2836e[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match_all"": {}
			  },
			  ""sort"": [""my_id""]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/parent-join.asciidoc:272")]
		public void Line272()
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
		[Description("mapping/types/parent-join.asciidoc:325")]
		public void Line325()
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
		[Description("mapping/types/parent-join.asciidoc:346")]
		public void Line346()
		{
			// tag::2c090fe7ec7b66b3f5c178d71c46323b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::2c090fe7ec7b66b3f5c178d71c46323b[]

			response0.MatchesExample(@"GET _stats/fielddata?human&fields=my_join_field#question");

			response1.MatchesExample(@"GET _nodes/stats/indices/fielddata?human&fields=my_join_field#question");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/parent-join.asciidoc:360")]
		public void Line360()
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
		[Description("mapping/types/parent-join.asciidoc:387")]
		public void Line387()
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
		[Description("mapping/types/parent-join.asciidoc:422")]
		public void Line422()
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
