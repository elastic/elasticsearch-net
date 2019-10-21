using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Licensing
{
	public class StartBasicPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line43()
		{
			// tag::8699d35269a47ba867fa8cc766287413[]
			var response0 = new SearchResponse<object>();
			// end::8699d35269a47ba867fa8cc766287413[]

			response0.MatchesExample(@"POST /_license/start_basic");
		}

		[U(Skip = "Example not implemented")]
		public void Line63()
		{
			// tag::f58fd031597e2c3df78bf0efd07206e3[]
			var response0 = new SearchResponse<object>();
			// end::f58fd031597e2c3df78bf0efd07206e3[]

			response0.MatchesExample(@"POST /_license/start_basic?acknowledge=true");
		}
	}
}