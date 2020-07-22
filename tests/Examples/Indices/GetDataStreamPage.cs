using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class GetDataStreamPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/get-data-stream.asciidoc:66")]
		public void Line66()
		{
			// tag::3337c817ebd438254505a31e91c91724[]
			var response0 = new SearchResponse<object>();
			// end::3337c817ebd438254505a31e91c91724[]

			response0.MatchesExample(@"GET /_data_stream/my-data-stream");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-data-stream.asciidoc:185")]
		public void Line185()
		{
			// tag::200f6d4cc7b9c300b8962a119e03873f[]
			var response0 = new SearchResponse<object>();
			// end::200f6d4cc7b9c300b8962a119e03873f[]

			response0.MatchesExample(@"GET _data_stream/my-data-stream*");
		}
	}
}