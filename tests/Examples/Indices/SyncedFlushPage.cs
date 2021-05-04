// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class SyncedFlushPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::40183a2d3b3e476fa9087cdef4eb7d1e[]
			var response0 = new SearchResponse<object>();
			// end::40183a2d3b3e476fa9087cdef4eb7d1e[]

			response0.MatchesExample(@"POST /twitter/_flush/synced");
		}

		[U(Skip = "Example not implemented")]
		public void Line80()
		{
			// tag::067ffd3c175b635d3a925afab1a5d547[]
			var response0 = new SearchResponse<object>();
			// end::067ffd3c175b635d3a925afab1a5d547[]

			response0.MatchesExample(@"GET /twitter/_stats?filter_path=**.commit&level=shards <1>");
		}

		[U(Skip = "Example not implemented")]
		public void Line172()
		{
			// tag::cefde3553fdbd516813e73a603c72c24[]
			var response0 = new SearchResponse<object>();
			// end::cefde3553fdbd516813e73a603c72c24[]

			response0.MatchesExample(@"POST /kimchy/_flush");
		}

		[U(Skip = "Example not implemented")]
		public void Line182()
		{
			// tag::859c6797075782162be1f82cfdcb6330[]
			var response0 = new SearchResponse<object>();
			// end::859c6797075782162be1f82cfdcb6330[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_flush/synced");
		}

		[U(Skip = "Example not implemented")]
		public void Line193()
		{
			// tag::5184908cdfe9c27762b3a53151ede483[]
			var response0 = new SearchResponse<object>();
			// end::5184908cdfe9c27762b3a53151ede483[]

			response0.MatchesExample(@"POST /_flush/synced");
		}
	}
}
