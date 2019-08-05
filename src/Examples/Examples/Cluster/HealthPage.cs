using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class HealthPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line9()
		{
			// tag::b02e4907c9936c1adc16ccce9d49900d[]
			var response0 = new SearchResponse<object>();
			// end::b02e4907c9936c1adc16ccce9d49900d[]

			response0.MatchesExample(@"GET _cluster/health");
		}

		[U]
		[SkipExample]
		public void Line46()
		{
			// tag::1dc474520122c524016c14a4418934b6[]
			var response0 = new SearchResponse<object>();
			// end::1dc474520122c524016c14a4418934b6[]

			response0.MatchesExample(@"GET /_cluster/health/test1,test2");
		}

		[U]
		[SkipExample]
		public void Line66()
		{
			// tag::04f5dd677c777bcb15d7d5fa63275fc8[]
			var response0 = new SearchResponse<object>();
			// end::04f5dd677c777bcb15d7d5fa63275fc8[]

			response0.MatchesExample(@"GET /_cluster/health?wait_for_status=yellow&timeout=50s");
		}

		[U]
		[SkipExample]
		public void Line129()
		{
			// tag::c48264ec5d9b9679fddd72e5c44425b9[]
			var response0 = new SearchResponse<object>();
			// end::c48264ec5d9b9679fddd72e5c44425b9[]

			response0.MatchesExample(@"GET /_cluster/health/twitter?level=shards");
		}
	}
}