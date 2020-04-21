using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Cat
{
	public class TransformsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/transforms.asciidoc:168")]
		public void Line168()
		{
			// tag::da8c3c635ea1101d6a1a5eb1db2ffebd[]
			var response0 = new SearchResponse<object>();
			// end::da8c3c635ea1101d6a1a5eb1db2ffebd[]

			response0.MatchesExample(@"GET /_cat/transforms?v&format=json");
		}
	}
}