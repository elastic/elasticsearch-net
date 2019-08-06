using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class ChangePasswordPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line50()
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