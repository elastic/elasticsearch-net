using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Docs
{
	public class MultiGetPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line15()
		{
			// tag::b02ea386a43df53f5b925ae64ff4bf96[]
			var response0 = new SearchResponse<object>();
			// end::b02ea386a43df53f5b925ae64ff4bf96[]

			response0.MatchesExample(@"GET /_mget
			{
			    ""docs"" : [
			        {
			            ""_index"" : ""test"",
			            ""_type"" : ""_doc"",
			            ""_id"" : ""1""
			        },
			        {
			            ""_index"" : ""test"",
			            ""_type"" : ""_doc"",
			            ""_id"" : ""2""
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line38()
		{
			// tag::60be49d20e42e165467817dcce53fcf7[]
			var response0 = new SearchResponse<object>();
			// end::60be49d20e42e165467817dcce53fcf7[]

			response0.MatchesExample(@"GET /test/_mget
			{
			    ""docs"" : [
			        {
			            ""_type"" : ""_doc"",
			            ""_id"" : ""1""
			        },
			        {
			            ""_type"" : ""_doc"",
			            ""_id"" : ""2""
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line58()
		{
			// tag::80229b2c753cfce1d1554e03cbdbfa29[]
			var response0 = new SearchResponse<object>();
			// end::80229b2c753cfce1d1554e03cbdbfa29[]

			response0.MatchesExample(@"GET /test/_doc/_mget
			{
			    ""docs"" : [
			        {
			            ""_id"" : ""1""
			        },
			        {
			            ""_id"" : ""2""
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line77()
		{
			// tag::104404dad47a4b52637fb88df6160a08[]
			var response0 = new SearchResponse<object>();
			// end::104404dad47a4b52637fb88df6160a08[]

			response0.MatchesExample(@"GET /test/_doc/_mget
			{
			    ""ids"" : [""1"", ""2""]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line98()
		{
			// tag::562535f3e70cc1a85ee6eb03588f96a6[]
			var response0 = new SearchResponse<object>();
			// end::562535f3e70cc1a85ee6eb03588f96a6[]

			response0.MatchesExample(@"GET /_mget
			{
			    ""docs"" : [
			        {
			            ""_index"" : ""test"",
			            ""_type"" : ""_doc"",
			            ""_id"" : ""1"",
			            ""_source"" : false
			        },
			        {
			            ""_index"" : ""test"",
			            ""_type"" : ""_doc"",
			            ""_id"" : ""2"",
			            ""_source"" : [""field3"", ""field4""]
			        },
			        {
			            ""_index"" : ""test"",
			            ""_type"" : ""_doc"",
			            ""_id"" : ""3"",
			            ""_source"" : {
			                ""include"": [""user""],
			                ""exclude"": [""user.location""]
			            }
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line137()
		{
			// tag::1b1e75613163308e4d40073a6c0918ce[]
			var response0 = new SearchResponse<object>();
			// end::1b1e75613163308e4d40073a6c0918ce[]

			response0.MatchesExample(@"GET /_mget
			{
			    ""docs"" : [
			        {
			            ""_index"" : ""test"",
			            ""_type"" : ""_doc"",
			            ""_id"" : ""1"",
			            ""stored_fields"" : [""field1"", ""field2""]
			        },
			        {
			            ""_index"" : ""test"",
			            ""_type"" : ""_doc"",
			            ""_id"" : ""2"",
			            ""stored_fields"" : [""field3"", ""field4""]
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line162()
		{
			// tag::8c7bf48aeb7d0919a1b0fd8685a1e480[]
			var response0 = new SearchResponse<object>();
			// end::8c7bf48aeb7d0919a1b0fd8685a1e480[]

			response0.MatchesExample(@"GET /test/_doc/_mget?stored_fields=field1,field2
			{
			    ""docs"" : [
			        {
			            ""_id"" : ""1"" \<1>
			        },
			        {
			            ""_id"" : ""2"",
			            ""stored_fields"" : [""field3"", ""field4""] \<2>
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line187()
		{
			// tag::1a6750042ee402bc92d644824f5cdc1f[]
			var response0 = new SearchResponse<object>();
			// end::1a6750042ee402bc92d644824f5cdc1f[]

			response0.MatchesExample(@"GET /_mget?routing=key1
			{
			    ""docs"" : [
			        {
			            ""_index"" : ""test"",
			            ""_type"" : ""_doc"",
			            ""_id"" : ""1"",
			            ""routing"" : ""key2""
			        },
			        {
			            ""_index"" : ""test"",
			            ""_type"" : ""_doc"",
			            ""_id"" : ""2""
			        }
			    ]
			}");
		}
	}
}