// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Root
{
	public class FrozenIndicesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("frozen-indices.asciidoc:66")]
		public void Line66()
		{
			// tag::f9018c483fb6b810d8a921668addfc71[]
			var response0 = new SearchResponse<object>();
			// end::f9018c483fb6b810d8a921668addfc71[]

			response0.MatchesExample(@"POST /twitter/_forcemerge?max_num_segments=1");
		}

		[U(Skip = "Example not implemented")]
		[Description("frozen-indices.asciidoc:83")]
		public void Line83()
		{
			// tag::0652fc9f77639fce67a87dc2e33cef51[]
			var response0 = new SearchResponse<object>();
			// end::0652fc9f77639fce67a87dc2e33cef51[]

			response0.MatchesExample(@"GET /twitter/_search?q=user:kimchy&ignore_throttled=false");
		}

		[U(Skip = "Example not implemented")]
		[Description("frozen-indices.asciidoc:97")]
		public void Line97()
		{
			// tag::9ff10591660890ba9d00eb14168c3b67[]
			var response0 = new SearchResponse<object>();
			// end::9ff10591660890ba9d00eb14168c3b67[]

			response0.MatchesExample(@"GET /_cat/indices/twitter?v&h=i,sth");
		}
	}
}
