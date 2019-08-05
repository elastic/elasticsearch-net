using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Suggesters
{
	public class MiscPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line10()
		{
			// tag::e194e9cbe3eb2305f4f7cdda0cf529bd[]
			var response0 = new SearchResponse<object>();
			// end::e194e9cbe3eb2305f4f7cdda0cf529bd[]

			response0.MatchesExample(@"POST _search?typed_keys
			{
			  ""suggest"": {
			    ""text"" : ""some test mssage"",
			    ""my-first-suggester"" : {
			      ""term"" : {
			        ""field"" : ""message""
			      }
			    },
			    ""my-second-suggester"" : {
			      ""phrase"" : {
			        ""field"" : ""message""
			      }
			    }
			  }
			}");
		}
	}
}