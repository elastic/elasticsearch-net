using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.Security.SecuringCommunications
{
	public class TutorialTlsAddnodesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line156()
		{
			// tag::b02e4907c9936c1adc16ccce9d49900d[]
			var response0 = new SearchResponse<object>();
			// end::b02e4907c9936c1adc16ccce9d49900d[]

			response0.MatchesExample(@"GET _cluster/health");
		}

		[U(Skip = "Example not implemented")]
		public void Line166()
		{
			// tag::9296d687ad779f8c57896edff2791c0d[]
			var response0 = new SearchResponse<object>();
			// end::9296d687ad779f8c57896edff2791c0d[]

			response0.MatchesExample(@"GET _cat/nodes?v");
		}
	}
}