using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class CreateDataStreamPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/create-data-stream.asciidoc:25")]
		public void Line25()
		{
			// tag::e51a86b666f447cda5f634547a8e1a4a[]
			var response0 = new SearchResponse<object>();
			// end::e51a86b666f447cda5f634547a8e1a4a[]

			response0.MatchesExample(@"PUT /_data_stream/my-data-stream");
		}
	}
}