using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.IndexModules
{
	public class StorePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line26()
		{
			// tag::509322c2cfd2bcb2f4cbfd14666e1f43[]
			var response0 = new SearchResponse<object>();
			// end::509322c2cfd2bcb2f4cbfd14666e1f43[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""index.store.type"": ""niofs""
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line116()
		{
			// tag::9ba2e779fe3e9d12ed5fca1ba3f8be97[]
			var response0 = new SearchResponse<object>();
			// end::9ba2e779fe3e9d12ed5fca1ba3f8be97[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""index.store.preload"": [""nvd"", ""dvd""]
			  }
			}");
		}
	}
}