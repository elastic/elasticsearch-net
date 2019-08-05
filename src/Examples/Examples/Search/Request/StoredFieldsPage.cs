using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Request
{
	public class StoredFieldsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line13()
		{
			// tag::2eeb3e55a7d3955e084bb369f1539009[]
			var response0 = new SearchResponse<object>();
			// end::2eeb3e55a7d3955e084bb369f1539009[]

			response0.MatchesExample(@"GET /_search
			{
			    ""stored_fields"" : [""user"", ""postDate""],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line30()
		{
			// tag::2af86a6ebbb834fbcf6fa7268f87a3a5[]
			var response0 = new SearchResponse<object>();
			// end::2af86a6ebbb834fbcf6fa7268f87a3a5[]

			response0.MatchesExample(@"GET /_search
			{
			    ""stored_fields"" : [],
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line62()
		{
			// tag::ccec437aed7a10d9111724ffd929fe00[]
			var response0 = new SearchResponse<object>();
			// end::ccec437aed7a10d9111724ffd929fe00[]

			response0.MatchesExample(@"GET /_search
			{
			    ""stored_fields"": ""_none_"",
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}
	}
}