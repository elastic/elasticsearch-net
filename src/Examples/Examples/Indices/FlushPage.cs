using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class FlushPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line13()
		{
			// tag::7ef5a1dfd0c9db876c0dd03d8f0fe3a7[]
			var response0 = new SearchResponse<object>();
			// end::7ef5a1dfd0c9db876c0dd03d8f0fe3a7[]

			response0.MatchesExample(@"POST twitter/_flush");
		}

		[U(Skip = "Example not implemented")]
		public void Line42()
		{
			// tag::191c0396ef10ca408b41bbb4c7645ee7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::191c0396ef10ca408b41bbb4c7645ee7[]

			response0.MatchesExample(@"POST kimchy,elasticsearch/_flush");

			response1.MatchesExample(@"POST _flush");
		}

		[U(Skip = "Example not implemented")]
		public void Line75()
		{
			// tag::94819e06e05de52c23b285346205ddaf[]
			var response0 = new SearchResponse<object>();
			// end::94819e06e05de52c23b285346205ddaf[]

			response0.MatchesExample(@"GET twitter/_stats?filter_path=**.commit&level=shards \<1>");
		}

		[U(Skip = "Example not implemented")]
		public void Line143()
		{
			// tag::da2658cc33e1a75c4b0fe96eb62740a7[]
			var response0 = new SearchResponse<object>();
			// end::da2658cc33e1a75c4b0fe96eb62740a7[]

			response0.MatchesExample(@"POST twitter/_flush/synced");
		}

		[U(Skip = "Example not implemented")]
		public void Line239()
		{
			// tag::fc079cd6d867c5d65b7a28de197292a4[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::fc079cd6d867c5d65b7a28de197292a4[]

			response0.MatchesExample(@"POST kimchy,elasticsearch/_flush/synced");

			response1.MatchesExample(@"POST _flush/synced");
		}
	}
}