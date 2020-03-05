using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class IndicesExistsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/indices-exists.asciidoc:12")]
		public void Line12()
		{
			// tag::22b6176a55b7884c247e30fb0899be5d[]
			var response0 = new SearchResponse<object>();
			// end::22b6176a55b7884c247e30fb0899be5d[]

			response0.MatchesExample(@"HEAD /twitter");
		}
	}
}