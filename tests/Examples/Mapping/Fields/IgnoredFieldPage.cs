using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Fields
{
	public class IgnoredFieldPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line18()
		{
			// tag::3fe0fb38f75d2a34fb1e6ac9bedbcdbc[]
			var response0 = new SearchResponse<object>();
			// end::3fe0fb38f75d2a34fb1e6ac9bedbcdbc[]

			response0.MatchesExample(@"GET _search
			{
			  ""query"": {
			    ""exists"": {
			      ""field"": ""_ignored""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line33()
		{
			// tag::cf47cd4a39cd62a3ecad919e54a67bca[]
			var response0 = new SearchResponse<object>();
			// end::cf47cd4a39cd62a3ecad919e54a67bca[]

			response0.MatchesExample(@"GET _search
			{
			  ""query"": {
			    ""term"": {
			      ""_ignored"": ""@timestamp""
			    }
			  }
			}");
		}
	}
}