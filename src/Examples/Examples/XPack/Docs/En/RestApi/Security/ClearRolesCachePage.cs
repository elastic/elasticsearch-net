using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class ClearRolesCachePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line39()
		{
			// tag::ee577c4c7cc723e99569ea2d1137adba[]
			var response0 = new SearchResponse<object>();
			// end::ee577c4c7cc723e99569ea2d1137adba[]

			response0.MatchesExample(@"POST /_security/role/my_admin_role/_clear_cache");
		}
	}
}