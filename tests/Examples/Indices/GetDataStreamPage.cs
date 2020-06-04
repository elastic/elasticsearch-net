using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class GetDataStreamPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/get-data-stream.asciidoc:35")]
		public void Line35()
		{
			// tag::05ba0fdd0215e313ecea8a2f8f5a43b4[]
			var response0 = new SearchResponse<object>();
			// end::05ba0fdd0215e313ecea8a2f8f5a43b4[]

			response0.MatchesExample(@"GET _data_stream/my-data-stream");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-data-stream.asciidoc:63")]
		public void Line63()
		{
			// tag::200f6d4cc7b9c300b8962a119e03873f[]
			var response0 = new SearchResponse<object>();
			// end::200f6d4cc7b9c300b8962a119e03873f[]

			response0.MatchesExample(@"GET _data_stream/my-data-stream*");
		}
	}
}