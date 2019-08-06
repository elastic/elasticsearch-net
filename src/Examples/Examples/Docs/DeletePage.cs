using Elastic.Xunit.XunitPlumbing;
using Examples.Models;
using Nest;

namespace Examples.Docs
{
	public class DeletePage : ExampleBase
	{
		[U]
		public void Line9()
		{
			// tag::c5e5873783246c7b1c01d8464fed72c4[]
			var deleteResponse = client.Delete<Tweet>(1, d => d.Index("twitter"));
			// end::c5e5873783246c7b1c01d8464fed72c4[]

			deleteResponse.MatchesExample(@"DELETE /twitter/_doc/1");
		}

		[U]
		public void Line84()
		{
			// tag::47b5ff897f26e9c943cee5c06034181d[]
			var deleteResponse = client.Delete<Tweet>(1, d => d
				.Index("twitter")
				.Routing("kimchy")
			);
			// end::47b5ff897f26e9c943cee5c06034181d[]

			deleteResponse.MatchesExample(@"DELETE /twitter/_doc/1?routing=kimchy");
		}

		[U]
		public void Line147()
		{
			// tag::d90a84a24a407731dfc1929ac8327746[]
			var deleteResponse = client.Delete<Tweet>(1, d => d
				.Index("twitter")
				.Timeout("5m")
			);
			// end::d90a84a24a407731dfc1929ac8327746[]

			deleteResponse.MatchesExample(@"DELETE /twitter/_doc/1?timeout=5m");
		}
	}
}
