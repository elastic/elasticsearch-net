using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class ExistsQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line20()
		{
			// tag::3342c69b2c2303247217532956fcce85[]
			var response0 = new SearchResponse<object>();
			// end::3342c69b2c2303247217532956fcce85[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""exists"": {
			            ""field"": ""user""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line57()
		{
			// tag::43af86de5e49aa06070092fffc138208[]
			var response0 = new SearchResponse<object>();
			// end::43af86de5e49aa06070092fffc138208[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"": {
			            ""must_not"": {
			                ""exists"": {
			                    ""field"": ""user""
			                }
			            }
			        }
			    }
			}");
		}
	}
}