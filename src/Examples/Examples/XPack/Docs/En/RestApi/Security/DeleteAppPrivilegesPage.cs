using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class DeleteAppPrivilegesPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line42()
		{
			// tag::ebd76a45e153c4656c5871e23b7b5508[]
			var response0 = new SearchResponse<object>();
			// end::ebd76a45e153c4656c5871e23b7b5508[]

			response0.MatchesExample(@"DELETE /_security/privilege/myapp/read");
		}
	}
}