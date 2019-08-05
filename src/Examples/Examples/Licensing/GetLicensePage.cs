using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Licensing
{
	public class GetLicensePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line48()
		{
			// tag::11c395d1649733bcab853fe31ec393b2[]
			var response0 = new SearchResponse<object>();
			// end::11c395d1649733bcab853fe31ec393b2[]

			response0.MatchesExample(@"GET /_license");
		}
	}
}