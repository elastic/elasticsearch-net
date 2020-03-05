using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ilm.Apis
{
	public class StartPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ilm/apis/start.asciidoc:76")]
		public void Line76()
		{
			// tag::72ae3851160fcf02b8e2cdfd4e57d238[]
			var response0 = new SearchResponse<object>();
			// end::72ae3851160fcf02b8e2cdfd4e57d238[]

			response0.MatchesExample(@"POST _ilm/start");
		}
	}
}