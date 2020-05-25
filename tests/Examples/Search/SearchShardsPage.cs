// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search
{
	public class SearchShardsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/search-shards.asciidoc:7")]
		public void Line7()
		{
			// tag::49b137a1c0016face219bac3faf41996[]
			var response0 = new SearchResponse<object>();
			// end::49b137a1c0016face219bac3faf41996[]

			response0.MatchesExample(@"GET /twitter/_search_shards");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/search-shards.asciidoc:144")]
		public void Line144()
		{
			// tag::a44b7da0091ac75e5571475a4e99bb16[]
			var response0 = new SearchResponse<object>();
			// end::a44b7da0091ac75e5571475a4e99bb16[]

			response0.MatchesExample(@"GET /twitter/_search_shards?routing=foo,bar");
		}
	}
}
