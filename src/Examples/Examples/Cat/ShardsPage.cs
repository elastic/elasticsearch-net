using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class ShardsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::7e126e2751311db60cfcbb22c9c41caa[]
			var response0 = new SearchResponse<object>();
			// end::7e126e2751311db60cfcbb22c9c41caa[]

			response0.MatchesExample(@"GET _cat/shards");
		}

		[U]
		[SkipExample]
		public void Line37()
		{
			// tag::e42e92050dd1c20262ce9e38f4b42ba0[]
			var response0 = new SearchResponse<object>();
			// end::e42e92050dd1c20262ce9e38f4b42ba0[]

			response0.MatchesExample(@"GET _cat/shards/twitt*");
		}

		[U]
		[SkipExample]
		public void Line107()
		{
			// tag::25c0e66a433a0cd596e0641b752ff6d7[]
			var response0 = new SearchResponse<object>();
			// end::25c0e66a433a0cd596e0641b752ff6d7[]

			response0.MatchesExample(@"GET _cat/shards?h=index,shard,prirep,state,unassigned.reason");
		}
	}
}