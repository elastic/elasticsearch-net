using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search
{
	public class SearchPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line17()
		{
			// tag::be49260e1b3496c4feac38c56ebb0669[]
			var response0 = new SearchResponse<object>();
			// end::be49260e1b3496c4feac38c56ebb0669[]

			response0.MatchesExample(@"GET /twitter/_search?q=user:kimchy");
		}

		[U]
		[SkipExample]
		public void Line27()
		{
			// tag::269071bbf812125f0b250676251c5936[]
			var response0 = new SearchResponse<object>();
			// end::269071bbf812125f0b250676251c5936[]

			response0.MatchesExample(@"GET /kimchy,elasticsearch/_search?q=tag:wow");
		}

		[U]
		[SkipExample]
		public void Line36()
		{
			// tag::4b2b9e7600f9d1eecf82de070a1bf2f4[]
			var response0 = new SearchResponse<object>();
			// end::4b2b9e7600f9d1eecf82de070a1bf2f4[]

			response0.MatchesExample(@"GET /_all/_search?q=tag:wow");
		}
	}
}