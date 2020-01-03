using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class SpanTermQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::086b2bbc4c3bfc2310c22d10db42cb82[]
			var response0 = new SearchResponse<object>();
			// end::086b2bbc4c3bfc2310c22d10db42cb82[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line23()
		{
			// tag::5add42087c83b7e498f8f43e91f343d4[]
			var response0 = new SearchResponse<object>();
			// end::5add42087c83b7e498f8f43e91f343d4[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			       ""span_term"" : { ""user"" : { ""value"" : ""kimchy"", ""boost"" : 2.0 } }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line35()
		{
			// tag::2a07d189553602066fefdb6b7cbdf542[]
			var response0 = new SearchResponse<object>();
			// end::2a07d189553602066fefdb6b7cbdf542[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""span_term"" : { ""user"" : { ""term"" : ""kimchy"", ""boost"" : 2.0 } }
			    }
			}");
		}
	}
}