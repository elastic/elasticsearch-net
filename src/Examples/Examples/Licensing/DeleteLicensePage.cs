using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Licensing
{
	public class DeleteLicensePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line35()
		{
			// tag::4f8a4ad49e2bca6784c88ede18a1a709[]
			var response0 = new SearchResponse<object>();
			// end::4f8a4ad49e2bca6784c88ede18a1a709[]

			response0.MatchesExample(@"DELETE /_license");
		}
	}
}