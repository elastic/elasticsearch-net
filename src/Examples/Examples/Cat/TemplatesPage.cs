using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class TemplatesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line7()
		{
			// tag::289e6033c96f931844770114113cad6a[]
			var response0 = new SearchResponse<object>();
			// end::289e6033c96f931844770114113cad6a[]

			response0.MatchesExample(@"GET /_cat/templates?v&s=name");
		}
	}
}