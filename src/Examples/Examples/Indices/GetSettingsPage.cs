using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class GetSettingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line7()
		{
			// tag::20bdfd960e8d76c4329269e237792eb7[]
			var response0 = new SearchResponse<object>();
			// end::20bdfd960e8d76c4329269e237792eb7[]

			response0.MatchesExample(@"GET /twitter/_settings");
		}

		[U(Skip = "Example not implemented")]
		public void Line24()
		{
			// tag::c538fc182f433e7141aee9d75c3e42d2[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::c538fc182f433e7141aee9d75c3e42d2[]

			response0.MatchesExample(@"GET /twitter,kimchy/_settings");

			response1.MatchesExample(@"GET /_all/_settings");

			response2.MatchesExample(@"GET /log_2013_*/_settings");
		}

		[U(Skip = "Example not implemented")]
		public void Line42()
		{
			// tag::9748682dcfb24b7d4893f534f7040370[]
			var response0 = new SearchResponse<object>();
			// end::9748682dcfb24b7d4893f534f7040370[]

			response0.MatchesExample(@"GET /log_2013_-*/_settings/index.number_*");
		}
	}
}