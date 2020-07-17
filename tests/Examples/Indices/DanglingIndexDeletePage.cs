using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class DanglingIndexDeletePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/dangling-index-delete.asciidoc:13")]
		public void Line13()
		{
			// tag::31f4400716500149cccbc19aa06bff66[]
			var response0 = new SearchResponse<object>();
			// end::31f4400716500149cccbc19aa06bff66[]

			response0.MatchesExample(@"DELETE /_dangling/<index-uuid>?accept_data_loss=true");
		}
	}
}