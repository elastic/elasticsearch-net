using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping
{
	public class DynamicMappingPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::61c49cee90c6aa0eafbdd5cc03936e7d[]
			var response0 = new SearchResponse<object>();
			// end::61c49cee90c6aa0eafbdd5cc03936e7d[]

			response0.MatchesExample(@"PUT data/_doc/1 \<1>
			{ ""count"": 5 }");
		}
	}
}