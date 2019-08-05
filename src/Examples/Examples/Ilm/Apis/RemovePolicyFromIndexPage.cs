using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class RemovePolicyFromIndexPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line78()
		{
			// tag::8bec5a437f4aea6f3f897c9df2ce2442[]
			var response0 = new SearchResponse<object>();
			// end::8bec5a437f4aea6f3f897c9df2ce2442[]

			response0.MatchesExample(@"POST my_index/_ilm/remove");
		}

		[U]
		[SkipExample]
		public void Line87()
		{
			// tag::7464040de4facd0800a50d9488d41808[]
			var response0 = new SearchResponse<object>();
			// end::7464040de4facd0800a50d9488d41808[]

			response0.MatchesExample(@"{
			  ""has_failures"" : false,
			  ""failed_indexes"" : []
			}");
		}
	}
}