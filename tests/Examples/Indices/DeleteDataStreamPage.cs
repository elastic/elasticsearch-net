using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class DeleteDataStreamPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/delete-data-stream.asciidoc:26")]
		public void Line26()
		{
			// tag::2b59b014349d45bf894aca90b2b1fbe0[]
			var response0 = new SearchResponse<object>();
			// end::2b59b014349d45bf894aca90b2b1fbe0[]

			response0.MatchesExample(@"DELETE _data_stream/my-data-stream");
		}
	}
}