using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class DeleteRolesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/delete-roles.asciidoc:40")]
		public void Line40()
		{
			// tag::cffce059425d3d21e7f9571500d63524[]
			var response0 = new SearchResponse<object>();
			// end::cffce059425d3d21e7f9571500d63524[]

			response0.MatchesExample(@"DELETE /_security/role/my_admin_role");
		}
	}
}