using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetAppPrivilegesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line54()
		{
			// tag::cd8006165ac64f1ef99af48e5a35a25b[]
			var response0 = new SearchResponse<object>();
			// end::cd8006165ac64f1ef99af48e5a35a25b[]

			response0.MatchesExample(@"GET /_security/privilege/myapp/read");
		}

		[U(Skip = "Example not implemented")]
		public void Line86()
		{
			// tag::3b18e9de638ff0b1c7a1f1f6bf1c24f3[]
			var response0 = new SearchResponse<object>();
			// end::3b18e9de638ff0b1c7a1f1f6bf1c24f3[]

			response0.MatchesExample(@"GET /_security/privilege/myapp/");
		}

		[U(Skip = "Example not implemented")]
		public void Line94()
		{
			// tag::0ddf705317d9c5095b4a1419a2e3bace[]
			var response0 = new SearchResponse<object>();
			// end::0ddf705317d9c5095b4a1419a2e3bace[]

			response0.MatchesExample(@"GET /_security/privilege/");
		}
	}
}