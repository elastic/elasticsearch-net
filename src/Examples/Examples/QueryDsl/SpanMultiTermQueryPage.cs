using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class SpanMultiTermQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line12()
		{
			// tag::a22f79d01a4a625840072024feb60b46[]
			var response0 = new SearchResponse<object>();
			// end::a22f79d01a4a625840072024feb60b46[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_multi"":{
			            ""match"":{
			                ""prefix"" : { ""user"" :  { ""value"" : ""ki"" } }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line29()
		{
			// tag::87ffa93d8de41fd0c3ea2f52378dab9c[]
			var response0 = new SearchResponse<object>();
			// end::87ffa93d8de41fd0c3ea2f52378dab9c[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_multi"":{
			            ""match"":{
			                ""prefix"" : { ""user"" :  { ""value"" : ""ki"", ""boost"" : 1.08 } }
			            }
			        }
			    }
			}");
		}
	}
}