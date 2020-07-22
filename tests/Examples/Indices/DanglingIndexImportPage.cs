using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class DanglingIndexImportPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/dangling-index-import.asciidoc:13")]
		public void Line13()
		{
			// tag::a3d943ac9d45b4eff4aa0c679b4eceb3[]
			var response0 = new SearchResponse<object>();
			// end::a3d943ac9d45b4eff4aa0c679b4eceb3[]

			response0.MatchesExample(@"POST /_dangling/<index-uuid>?accept_data_loss=true");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/dangling-index-import.asciidoc:53")]
		public void Line53()
		{
			// tag::ca98afbd6a90f63e02f62239d225313b[]
			var response0 = new SearchResponse<object>();
			// end::ca98afbd6a90f63e02f62239d225313b[]

			response0.MatchesExample(@"POST /_dangling/zmM4e0JtBkeUjiHD-MihPQ?accept_data_loss=true");
		}
	}
}