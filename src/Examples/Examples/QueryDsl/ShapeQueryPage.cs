using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class ShapeQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line28()
		{
			// tag::32afa78603025e0917a2bdd7e2e39ac8[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::32afa78603025e0917a2bdd7e2e39ac8[]

			response0.MatchesExample(@"PUT /example
			{
			    ""mappings"": {
			        ""properties"": {
			            ""geometry"": {
			                ""type"": ""shape""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"POST /example/_doc?refresh
			{
			    ""name"": ""Lucky Landing"",
			    ""location"": {
			        ""type"": ""point"",
			        ""coordinates"": [1355.400544, 5255.530286]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line56()
		{
			// tag::f7bbcd762afef5a562768a46fe192b56[]
			var response0 = new SearchResponse<object>();
			// end::f7bbcd762afef5a562768a46fe192b56[]

			response0.MatchesExample(@"GET /example/_search
			{
			    ""query"":{
			        ""shape"": {
			            ""geometry"": {
			                ""shape"": {
			                    ""type"": ""envelope"",
			                    ""coordinates"" : [[1355.0, 5355.0], [1400.0, 5200.0]]
			                },
			                ""relation"": ""within""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line94()
		{
			// tag::86388922e307dd94c0f5f93890f13832[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::86388922e307dd94c0f5f93890f13832[]

			response0.MatchesExample(@"PUT /shapes
			{
			    ""mappings"": {
			        ""properties"": {
			            ""geometry"": {
			                ""type"": ""shape""
			            }
			        }
			    }
			}");

			response1.MatchesExample(@"PUT /shapes/_doc/footprint
			{
			    ""geometry"": {
			        ""type"": ""envelope"",
			        ""coordinates"" : [[1355.0, 5355.0], [1400.0, 5200.0]]
			    }
			}");

			response2.MatchesExample(@"GET /example/_search
			{
			    ""query"": {
			        ""shape"": {
			            ""geometry"": {
			                ""indexed_shape"": {
			                    ""index"": ""shapes"",
			                    ""id"": ""footprint"",
			                    ""path"": ""geometry""
			                }
			            }
			        }
			    }
			}");
		}
	}
}