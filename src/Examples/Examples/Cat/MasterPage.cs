using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class MasterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line8()
		{
			// tag::45bde49f35ffae3f3dabc77a592241b4[]
			var response0 = new SearchResponse<object>();
			// end::45bde49f35ffae3f3dabc77a592241b4[]

			response0.MatchesExample(@"GET /_cat/master?v");
		}
	}
}