using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Docs
{
	public class DeletePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line9()
		{
			// tag::c5e5873783246c7b1c01d8464fed72c4[]
			var response0 = new SearchResponse<object>();
			// end::c5e5873783246c7b1c01d8464fed72c4[]

			response0.MatchesExample(@"DELETE /twitter/_doc/1");
		}

		[U]
		[SkipExample]
		public void Line84()
		{
			// tag::47b5ff897f26e9c943cee5c06034181d[]
			var response0 = new SearchResponse<object>();
			// end::47b5ff897f26e9c943cee5c06034181d[]

			response0.MatchesExample(@"DELETE /twitter/_doc/1?routing=kimchy");
		}

		[U]
		[SkipExample]
		public void Line147()
		{
			// tag::d90a84a24a407731dfc1929ac8327746[]
			var response0 = new SearchResponse<object>();
			// end::d90a84a24a407731dfc1929ac8327746[]

			response0.MatchesExample(@"DELETE /twitter/_doc/1?timeout=5m");
		}
	}
}