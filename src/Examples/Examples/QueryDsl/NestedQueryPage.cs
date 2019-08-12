using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class NestedQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line23()
		{
			// tag::ae57d3aa9075aaab34bda7655cdafabb[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::ae57d3aa9075aaab34bda7655cdafabb[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""obj1"" : {
			                ""type"" : ""nested""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"");
		}

		[U(Skip = "Example not implemented")]
		public void Line43()
		{
			// tag::f9abf6c518e9ec793218c3696f5f2f8f[]
			var response0 = new SearchResponse<object>();
			// end::f9abf6c518e9ec793218c3696f5f2f8f[]

			response0.MatchesExample(@"GET /my_index/_search
			{
			    ""query"": {
			        ""nested"" : {
			            ""path"" : ""obj1"",
			            ""query"" : {
			                ""bool"" : {
			                    ""must"" : [
			                    { ""match"" : {""obj1.name"" : ""blue""} },
			                    { ""range"" : {""obj1.count"" : {""gt"" : 5}} }
			                    ]
			                }
			            },
			            ""score_mode"" : ""avg""
			        }
			    }
			}");
		}
	}
}