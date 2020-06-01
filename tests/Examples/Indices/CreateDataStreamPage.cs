using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class CreateDataStreamPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/create-data-stream.asciidoc:30")]
		public void Line30()
		{
			// tag::e526bfa0fd5ee08891a1d0320e4a8040[]
			var response0 = new SearchResponse<object>();
			// end::e526bfa0fd5ee08891a1d0320e4a8040[]

			response0.MatchesExample(@"PUT _index_template/template
			{
			  ""index_patterns"": [""my-data-stream*""],
			  ""data_stream"": {
			    ""timestamp_field"": ""@timestamp""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/create-data-stream.asciidoc:42")]
		public void Line42()
		{
			// tag::7741a04e7e621c528cd72848d875776d[]
			var response0 = new SearchResponse<object>();
			// end::7741a04e7e621c528cd72848d875776d[]

			response0.MatchesExample(@"PUT _data_stream/my-data-stream");
		}
	}
}