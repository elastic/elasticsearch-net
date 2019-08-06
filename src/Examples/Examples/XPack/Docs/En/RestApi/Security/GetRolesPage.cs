using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class GetRolesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line42()
		{
			// tag::115529722ba30b0b0d51a7ff87e59198[]
			var response0 = new SearchResponse<object>();
			// end::115529722ba30b0b0d51a7ff87e59198[]

			response0.MatchesExample(@"GET /_security/role/my_admin_role");
		}

		[U(Skip = "Example not implemented")]
		public void Line81()
		{
			// tag::128283698535116931dca9d16a16dca2[]
			var response0 = new SearchResponse<object>();
			// end::128283698535116931dca9d16a16dca2[]

			response0.MatchesExample(@"GET /_security/role");
		}
	}
}