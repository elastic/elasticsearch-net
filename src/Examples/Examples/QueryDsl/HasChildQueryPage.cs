using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class HasChildQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line31()
		{
			// tag::10239a59784c3069e0d9399d3f9a7008[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::10239a59784c3069e0d9399d3f9a7008[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""my-join-field"" : {
			                ""type"" : ""join"",
			                ""relations"": {
			                    ""parent"": ""child""
			                }
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"");
		}

		[U(Skip = "Example not implemented")]
		public void Line54()
		{
			// tag::a204ff3396082b32175371c7ed8b9394[]
			var response0 = new SearchResponse<object>();
			// end::a204ff3396082b32175371c7ed8b9394[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""has_child"" : {
			            ""type"" : ""child"",
			            ""query"" : {
			                ""match_all"" : {}
			            },
			            ""max_children"": 10,
			            ""min_children"": 2,
			            ""score_mode"" : ""min""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line143()
		{
			// tag::d7b459941dc32d790ade80a0f5712560[]
			var response0 = new SearchResponse<object>();
			// end::d7b459941dc32d790ade80a0f5712560[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""has_child"" : {
			            ""type"" : ""child"",
			            ""query"" : {
			                ""function_score"" : {
			                    ""script_score"": {
			                        ""script"": ""_score * doc['click_count'].value""
			                    }
			                }
			            },
			            ""score_mode"" : ""max""
			        }
			    }
			}");
		}
	}
}