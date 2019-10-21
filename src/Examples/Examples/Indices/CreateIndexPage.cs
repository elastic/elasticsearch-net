using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class CreateIndexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::1c23507edd7a3c18538b68223378e4ab[]
			var response0 = new SearchResponse<object>();
			// end::1c23507edd7a3c18538b68223378e4ab[]

			response0.MatchesExample(@"PUT /twitter");
		}

		[U(Skip = "Example not implemented")]
		public void Line82()
		{
			// tag::e5d2172b524332196cac0f031c043659[]
			var response0 = new SearchResponse<object>();
			// end::e5d2172b524332196cac0f031c043659[]

			response0.MatchesExample(@"PUT /twitter
			{
			    ""settings"" : {
			        ""index"" : {
			            ""number_of_shards"" : 3, <1>
			            ""number_of_replicas"" : 2 <2>
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line100()
		{
			// tag::b9c5d7ca6ca9c6f747201f45337a4abf[]
			var response0 = new SearchResponse<object>();
			// end::b9c5d7ca6ca9c6f747201f45337a4abf[]

			response0.MatchesExample(@"PUT /twitter
			{
			    ""settings"" : {
			        ""number_of_shards"" : 3,
			        ""number_of_replicas"" : 2
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line124()
		{
			// tag::dfef545b1e2c247bafd1347e8e807ac1[]
			var response0 = new SearchResponse<object>();
			// end::dfef545b1e2c247bafd1347e8e807ac1[]

			response0.MatchesExample(@"PUT /test
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
		public void Line148()
		{
			// tag::4d56b179242fed59e3d6476f817b6055[]
			var response0 = new SearchResponse<object>();
			// end::4d56b179242fed59e3d6476f817b6055[]

			response0.MatchesExample(@"PUT /test
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
		public void Line195()
		{
			// tag::4d46dbb96125b27f46299547de9d8709[]
			var response0 = new SearchResponse<object>();
			// end::4d46dbb96125b27f46299547de9d8709[]

			response0.MatchesExample(@"PUT /test
			{
			    ""settings"": {
			        ""index.write.wait_for_active_shards"": ""2""
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line208()
		{
			// tag::fabe14480624a99e8ee42c7338672058[]
			var response0 = new SearchResponse<object>();
			// end::fabe14480624a99e8ee42c7338672058[]

			response0.MatchesExample(@"PUT /test?wait_for_active_shards=2");
		}
	}
}