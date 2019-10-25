using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class MatchAllQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::09d617863a103c82fb4101e6165ea7fe[]
			var response0 = new SearchResponse<object>();
			// end::09d617863a103c82fb4101e6165ea7fe[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_all"": {}
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line23()
		{
			// tag::75330ec1305d2beb0e2f34d2195464e2[]
			var response0 = new SearchResponse<object>();
			// end::75330ec1305d2beb0e2f34d2195464e2[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_all"": { ""boost"" : 1.2 }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line39()
		{
			// tag::81c9aa2678d6166a9662ddf2c011a6a5[]
			var response0 = new SearchResponse<object>();
			// end::81c9aa2678d6166a9662ddf2c011a6a5[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""match_none"": {}
			    }
			}");
		}
	}
}