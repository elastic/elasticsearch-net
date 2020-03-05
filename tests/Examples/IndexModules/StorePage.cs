using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.IndexModules
{
	public class StorePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line30()
		{
			// tag::fba99da14d4323c91794703438979912[]
			var response0 = new SearchResponse<object>();
			// end::fba99da14d4323c91794703438979912[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""index.store.type"": ""hybridfs""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line120()
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