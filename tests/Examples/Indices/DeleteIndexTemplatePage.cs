using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class DeleteIndexTemplatePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/delete-index-template.asciidoc:24")]
		public void Line24()
		{
			// tag::0f0fba0061d26602cd5f401ca4a19be3[]
			var response0 = new SearchResponse<object>();
			// end::0f0fba0061d26602cd5f401ca4a19be3[]

			response0.MatchesExample(@"DELETE /_template/template_1");
		}
	}
}