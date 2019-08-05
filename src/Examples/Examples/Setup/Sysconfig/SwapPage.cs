using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Setup.Sysconfig
{
	public class SwapPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line71()
		{
			// tag::ed250b74bc77c15bb794f55a12d762c3[]
			var response0 = new SearchResponse<object>();
			// end::ed250b74bc77c15bb794f55a12d762c3[]

			response0.MatchesExample(@"GET _nodes?filter_path=**.mlockall");
		}
	}
}