using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class RepositoriesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/repositories.asciidoc:38")]
		public void Line38()
		{
			// tag::6fa570aac5033e3b25d3071a6c9ea3dc[]
			var response0 = new SearchResponse<object>();
			// end::6fa570aac5033e3b25d3071a6c9ea3dc[]

			response0.MatchesExample(@"GET /_cat/repositories?v");
		}
	}
}