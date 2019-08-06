using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class TermQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line28()
		{
			// tag::d0a8a938a2fa913b6fdbc871079a59dd[]
			var response0 = new SearchResponse<object>();
			// end::d0a8a938a2fa913b6fdbc871079a59dd[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""term"": {
			            ""user"": {
			                ""value"": ""Kimchy"",
			                ""boost"": 1.0
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line95()
		{
			// tag::2a1de18774f9c68cafa169847832b2bc[]
			var response0 = new SearchResponse<object>();
			// end::2a1de18774f9c68cafa169847832b2bc[]

			response0.MatchesExample(@"PUT my_index
			{
			    ""mappings"" : {
			        ""properties"" : {
			            ""full_text"" : { ""type"" : ""text"" }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line115()
		{
			// tag::d4b4cefba4318caeba7480187faf2b13[]
			var response0 = new SearchResponse<object>();
			// end::d4b4cefba4318caeba7480187faf2b13[]

			response0.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""full_text"":   ""Quick Brown Foxes!""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line135()
		{
			// tag::cdedd5f33f7e5f7acde561e97bff61de[]
			var response0 = new SearchResponse<object>();
			// end::cdedd5f33f7e5f7acde561e97bff61de[]

			response0.MatchesExample(@"GET my_index/_search?pretty
			{
			  ""query"": {
			    ""term"": {
			      ""full_text"": ""Quick Brown Foxes!""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line170()
		{
			// tag::a80f5db4357bb25b8704d374c18318ed[]
			var response0 = new SearchResponse<object>();
			// end::a80f5db4357bb25b8704d374c18318ed[]

			response0.MatchesExample(@"GET my_index/_search?pretty
			{
			  ""query"": {
			    ""match"": {
			      ""full_text"": ""Quick Brown Foxes!""
			    }
			  }
			}");
		}
	}
}