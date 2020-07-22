using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Eql
{
	public class GetAsyncEqlSearchApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("eql/get-async-eql-search-api.asciidoc:17")]
		public void Line17()
		{
			// tag::d5ead6aacbfbedc8396f87bb34acc880[]
			var response0 = new SearchResponse<object>();
			// end::d5ead6aacbfbedc8396f87bb34acc880[]

			response0.MatchesExample(@"GET /_eql/search/FkpMRkJGS1gzVDRlM3g4ZzMyRGlLbkEaTXlJZHdNT09TU2VTZVBoNDM3cFZMUToxMDM=");
		}
	}
}