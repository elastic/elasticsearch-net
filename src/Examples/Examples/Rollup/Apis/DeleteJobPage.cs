using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Rollup.Apis
{
	public class DeleteJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line80()
		{
			// tag::94246f45025ed394cd6415ed8d7a0588[]
			var response0 = new SearchResponse<object>();
			// end::94246f45025ed394cd6415ed8d7a0588[]

			response0.MatchesExample(@"DELETE _rollup/job/sensor");
		}
	}
}