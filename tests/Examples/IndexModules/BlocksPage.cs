using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.IndexModules
{
	public class BlocksPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("index-modules/blocks.asciidoc:76")]
		public void Line76()
		{
			// tag::fcdc59a15a8f6da6e8f30905cae0525c[]
			var response0 = new SearchResponse<object>();
			// end::fcdc59a15a8f6da6e8f30905cae0525c[]

			response0.MatchesExample(@"PUT /twitter/_block/write");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/blocks.asciidoc:143")]
		public void Line143()
		{
			// tag::9bc4ea0bd452bade2feb275883f45861[]
			var response0 = new SearchResponse<object>();
			// end::9bc4ea0bd452bade2feb275883f45861[]

			response0.MatchesExample(@"PUT /my_index/_block/write");
		}
	}
}