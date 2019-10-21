using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class TemplateExistsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line12()
		{
			// tag::a6be6c1cb4a556866fdccb0dee2f1dea[]
			var response0 = new SearchResponse<object>();
			// end::a6be6c1cb4a556866fdccb0dee2f1dea[]

			response0.MatchesExample(@"HEAD /_template/template_1");
		}
	}
}