using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Eql
{
	public class DeleteAsyncEqlSearchApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("eql/delete-async-eql-search-api.asciidoc:17")]
		public void Line17()
		{
			// tag::26d3ab748a855eb383e992eb1ff79662[]
			var response0 = new SearchResponse<object>();
			// end::26d3ab748a855eb383e992eb1ff79662[]

			response0.MatchesExample(@"DELETE /_eql/search/FkpMRkJGS1gzVDRlM3g4ZzMyRGlLbkEaTXlJZHdNT09TU2VTZVBoNDM3cFZMUToxMDM=");
		}
	}
}