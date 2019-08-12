using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class CreateIndexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::a9f021477e6c3d78a7907fbd96e16b5f[]
			var response0 = new SearchResponse<object>();
			// end::a9f021477e6c3d78a7907fbd96e16b5f[]

			response0.MatchesExample(@"PUT twitter");
		}

		[U(Skip = "Example not implemented")]
		public void Line39()
		{
			// tag::be844338bc330b6d3939bac6ee57bbba[]
			var response0 = new SearchResponse<object>();
			// end::be844338bc330b6d3939bac6ee57bbba[]

			response0.MatchesExample(@"PUT twitter
			{
			    ""settings"" : {
			        ""index"" : {
			            ""number_of_shards"" : 3, \<1>
			            ""number_of_replicas"" : 2 \<2>
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line57()
		{
			// tag::15377f76164fd88309f58097c7125ff2[]
			var response0 = new SearchResponse<object>();
			// end::15377f76164fd88309f58097c7125ff2[]

			response0.MatchesExample(@"PUT twitter
			{
			    ""settings"" : {
			        ""number_of_shards"" : 3,
			        ""number_of_replicas"" : 2
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line84()
		{
			// tag::eaa809dc19ac4e9a4166ed46c6450c36[]
			var response0 = new SearchResponse<object>();
			// end::eaa809dc19ac4e9a4166ed46c6450c36[]

			response0.MatchesExample(@"PUT test
			{
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""mappings"" : {
			        ""properties"" : {
			            ""field1"" : { ""type"" : ""text"" }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line110()
		{
			// tag::ab8a4d5bd020a6923446a9bd9e402d16[]
			var response0 = new SearchResponse<object>();
			// end::ab8a4d5bd020a6923446a9bd9e402d16[]

			response0.MatchesExample(@"PUT test
			{
			    ""aliases"" : {
			        ""alias_1"" : {},
			        ""alias_2"" : {
			            ""filter"" : {
			                ""term"" : {""user"" : ""kimchy"" }
			            },
			            ""routing"" : ""kimchy""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line160()
		{
			// tag::f887b972ee522e0497f4b5289d33f764[]
			var response0 = new SearchResponse<object>();
			// end::f887b972ee522e0497f4b5289d33f764[]

			response0.MatchesExample(@"PUT test
			{
			    ""settings"": {
			        ""index.write.wait_for_active_shards"": ""2""
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line174()
		{
			// tag::ef3fb50903876e4497249165ec493bb5[]
			var response0 = new SearchResponse<object>();
			// end::ef3fb50903876e4497249165ec493bb5[]

			response0.MatchesExample(@"PUT test?wait_for_active_shards=2");
		}
	}
}