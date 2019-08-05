using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Apis
{
	public class DeletePipelinePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line29()
		{
			// tag::a05925031c1bfbb10c4ef6e5b678e20a[]
			var response0 = new SearchResponse<object>();
			// end::a05925031c1bfbb10c4ef6e5b678e20a[]

			response0.MatchesExample(@"DELETE _ingest/pipeline/my-pipeline-id");
		}

		[U]
		[SkipExample]
		public void Line64()
		{
			// tag::6ae6a398b979af8231cf6753a9a73f99[]
			var response0 = new SearchResponse<object>();
			// end::6ae6a398b979af8231cf6753a9a73f99[]

			response0.MatchesExample(@"DELETE _ingest/pipeline/*");
		}
	}
}