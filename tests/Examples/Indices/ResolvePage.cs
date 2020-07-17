using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class ResolvePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/resolve.asciidoc:50")]
		public void Line50()
		{
			// tag::b73ffaecb5532f9ab0136137e899c205[]
			var response0 = new SearchResponse<object>();
			// end::b73ffaecb5532f9ab0136137e899c205[]

			response0.MatchesExample(@"GET /_resolve/index/twitter*");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/resolve.asciidoc:83")]
		public void Line83()
		{
			// tag::bd57976bc93ca64b2d3e001df9f06c82[]
			var response0 = new SearchResponse<object>();
			// end::bd57976bc93ca64b2d3e001df9f06c82[]

			response0.MatchesExample(@"GET /_resolve/index/f*,remoteCluster1:bar*?expand_wildcards=all");
		}
	}
}