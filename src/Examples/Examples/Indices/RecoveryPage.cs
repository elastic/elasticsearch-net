using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class RecoveryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line10()
		{
			// tag::13ebcb01ebf1b5d2b5c52739db47e30c[]
			var response0 = new SearchResponse<object>();
			// end::13ebcb01ebf1b5d2b5c52739db47e30c[]

			response0.MatchesExample(@"GET index1,index2/_recovery?human");
		}

		[U]
		[SkipExample]
		public void Line67()
		{
			// tag::5dfb23f6e36ef484f1d3271bae76a8d1[]
			var response0 = new SearchResponse<object>();
			// end::5dfb23f6e36ef484f1d3271bae76a8d1[]

			response0.MatchesExample(@"GET /_recovery?human");
		}

		[U]
		[SkipExample]
		public void Line159()
		{
			// tag::5619103306878d58a058bce87c5bd82b[]
			var response0 = new SearchResponse<object>();
			// end::5619103306878d58a058bce87c5bd82b[]

			response0.MatchesExample(@"GET _recovery?human&detailed=true");
		}
	}
}