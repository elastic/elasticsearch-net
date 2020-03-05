using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class ChangePasswordPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/change-password.asciidoc:55")]
		public void Line55()
		{
			// tag::a2d14f8f1ea3efe970887f7892fdb268[]
			var response0 = new SearchResponse<object>();
			// end::a2d14f8f1ea3efe970887f7892fdb268[]

			response0.MatchesExample(@"POST /_security/user/jacknich/_password
			{
			  ""password"" : ""s3cr3t""
			}");
		}
	}
}