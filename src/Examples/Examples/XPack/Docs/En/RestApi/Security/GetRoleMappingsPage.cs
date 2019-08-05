using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetRoleMappingsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line53()
		{
			// tag::8b3a94495127efd9d56b2cd7f3eecdca[]
			var response0 = new SearchResponse<object>();
			// end::8b3a94495127efd9d56b2cd7f3eecdca[]

			response0.MatchesExample(@"GET /_security/role_mapping/mapping1");
		}
	}
}