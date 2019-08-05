using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Monitoring
{
	public class IndicesPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line12()
		{
			// tag::83dfd0852101eca3ba8174c9c38b4e73[]
			var response0 = new SearchResponse<object>();
			// end::83dfd0852101eca3ba8174c9c38b4e73[]

			response0.MatchesExample(@"GET /_template/.monitoring-*");
		}

		[U]
		[SkipExample]
		public void Line30()
		{
			// tag::a63906c63a8681c72d53ee0fcf2ffd35[]
			var response0 = new SearchResponse<object>();
			// end::a63906c63a8681c72d53ee0fcf2ffd35[]

			response0.MatchesExample(@"PUT /_template/custom_monitoring
			{
			    ""index_patterns"": "".monitoring-*"",
			    ""order"": 1,
			    ""settings"": {
			        ""number_of_shards"": 5,
			        ""number_of_replicas"": 2
			    }
			}");
		}
	}
}