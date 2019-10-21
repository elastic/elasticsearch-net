using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class HealthPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line32()
		{
			// tag::04f5dd677c777bcb15d7d5fa63275fc8[]
			var response0 = new SearchResponse<object>();
			// end::04f5dd677c777bcb15d7d5fa63275fc8[]

			response0.MatchesExample(@"GET /_cluster/health?wait_for_status=yellow&timeout=50s");
		}

		[U(Skip = "Example not implemented")]
		public void Line140()
		{
			// tag::b02e4907c9936c1adc16ccce9d49900d[]
			var response0 = new SearchResponse<object>();
			// end::b02e4907c9936c1adc16ccce9d49900d[]

			response0.MatchesExample(@"GET _cluster/health");
		}

		[U(Skip = "Example not implemented")]
		public void Line176()
		{
			// tag::c48264ec5d9b9679fddd72e5c44425b9[]
			var response0 = new SearchResponse<object>();
			// end::c48264ec5d9b9679fddd72e5c44425b9[]

			response0.MatchesExample(@"GET /_cluster/health/twitter?level=shards");
		}
	}
}