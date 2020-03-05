using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Transform.Apis
{
	public class StopTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/apis/stop-transform.asciidoc:91")]
		public void Line91()
		{
			// tag::654882f545eca8d7047695f867c63072[]
			var response0 = new SearchResponse<object>();
			// end::654882f545eca8d7047695f867c63072[]

			response0.MatchesExample(@"POST _transform/ecommerce_transform/_stop");
		}
	}
}