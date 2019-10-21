using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class PinnedQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line14()
		{
			// tag::f36a6f32ef72b326f13317bd34c6353f[]
			var response0 = new SearchResponse<object>();
			// end::f36a6f32ef72b326f13317bd34c6353f[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""pinned"" : {
			            ""ids"" : [""1"", ""4"", ""100""],
			            ""organic"" : {
			            	""match"":{
			            		""description"": ""iphone""
			            	}
			            }
			        }
			    }
			}");
		}
	}
}