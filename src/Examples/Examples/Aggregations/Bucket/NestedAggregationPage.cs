using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Bucket
{
	public class NestedAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::53e6007f451ddf30074b3e26a4afdaad[]
			var response0 = new SearchResponse<object>();
			// end::53e6007f451ddf30074b3e26a4afdaad[]

			response0.MatchesExample(@"PUT /index
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""resellers"" : { \<1>
			                ""type"" : ""nested"",
			                ""properties"" : {
			                    ""name"" : { ""type"" : ""text"" },
			                    ""price"" : { ""type"" : ""double"" }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line33()
		{
			// tag::e3d2300ad78b2d20c3a501a73db6bcac[]
			var response0 = new SearchResponse<object>();
			// end::e3d2300ad78b2d20c3a501a73db6bcac[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match"" : { ""name"" : ""led tv"" }
			    },
			    ""aggs"" : {
			        ""resellers"" : {
			            ""nested"" : {
			                ""path"" : ""resellers""
			            },
			            ""aggs"" : {
			                ""min_price"" : { ""min"" : { ""field"" : ""resellers.price"" } }
			            }
			        }
			    }
			}");
		}
	}
}