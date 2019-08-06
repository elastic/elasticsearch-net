using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class HasParentQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line27()
		{
			// tag::6515e74b150bbdae570e0fd3ef5ac2e5[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::6515e74b150bbdae570e0fd3ef5ac2e5[]

			response0.MatchesExample(@"PUT /my-index
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""my-join-field"" : {
			                ""type"" : ""join"",
			                ""relations"": {
			                    ""parent"": ""child""
			                }
			            },
			            ""tag"" : {
			                ""type"" : ""keyword""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"");
		}

		[U(Skip = "Example not implemented")]
		public void Line53()
		{
			// tag::e539bfb5c73771c73acdf22fe77dde04[]
			var response0 = new SearchResponse<object>();
			// end::e539bfb5c73771c73acdf22fe77dde04[]

			response0.MatchesExample(@"GET /my-index/_search
			{
			    ""query"": {
			        ""has_parent"" : {
			            ""parent_type"" : ""parent"",
			            ""query"" : {
			                ""term"" : {
			                    ""tag"" : {
			                        ""value"" : ""Elasticsearch""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line124()
		{
			// tag::85d88b08243afbef45d4dcea72c9a41c[]
			var response0 = new SearchResponse<object>();
			// end::85d88b08243afbef45d4dcea72c9a41c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""has_parent"" : {
			            ""parent_type"" : ""parent"",
			            ""score"" : true,
			            ""query"" : {
			                ""function_score"" : {
			                    ""script_score"": {
			                        ""script"": ""_score * doc['view_count'].value""
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}