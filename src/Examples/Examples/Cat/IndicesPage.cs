using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class IndicesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line8()
		{
			// tag::073539a7e38be3cdf13008330b6a536a[]
			var response0 = new SearchResponse<object>();
			// end::073539a7e38be3cdf13008330b6a536a[]

			response0.MatchesExample(@"GET /_cat/indices/twi*?v&s=index");
		}

		[U(Skip = "Example not implemented")]
		public void Line53()
		{
			// tag::bef41d7201051db3bc394b164c7130ae[]
			var response0 = new SearchResponse<object>();
			// end::bef41d7201051db3bc394b164c7130ae[]

			response0.MatchesExample(@"GET /_cat/indices?v&health=yellow");
		}

		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::79cba7619b42c0068b90ff72a3e45153[]
			var response0 = new SearchResponse<object>();
			// end::79cba7619b42c0068b90ff72a3e45153[]

			response0.MatchesExample(@"GET /_cat/indices?v&s=docs.count:desc");
		}

		[U(Skip = "Example not implemented")]
		public void Line92()
		{
			// tag::d5a849ddac8d7678d8460eef96e03c19[]
			var response0 = new SearchResponse<object>();
			// end::d5a849ddac8d7678d8460eef96e03c19[]

			response0.MatchesExample(@"GET /_cat/indices/twitter?pri&v&h=health,index,pri,rep,docs.count,mt");
		}

		[U(Skip = "Example not implemented")]
		public void Line110()
		{
			// tag::402de4d169bc4514e8d782bc06ac1c11[]
			var response0 = new SearchResponse<object>();
			// end::402de4d169bc4514e8d782bc06ac1c11[]

			response0.MatchesExample(@"GET /_cat/indices?v&h=i,tm&s=tm:desc");
		}
	}
}