// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class StatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/stats.asciidoc:11")]
		public void Line11()
		{
			// tag::fce5d68a9ac1b34b59d5308b65688e59[]
			var response0 = new SearchResponse<object>();
			// end::fce5d68a9ac1b34b59d5308b65688e59[]

			response0.MatchesExample(@"GET /twitter/_stats");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/stats.asciidoc:102")]
		public void Line102()
		{
			// tag::e0b2f56c34e33ff52f8f9658be2f7ca1[]
			var response0 = new SearchResponse<object>();
			// end::e0b2f56c34e33ff52f8f9658be2f7ca1[]

			response0.MatchesExample(@"GET /index1,index2/_stats");
		}
		[U(Skip = "Example not implemented")]
		[Description("indices/stats.asciidoc:112")]
		public void Line112()
		{
			// tag::78c4035e4fbf6851140660f6ed2a1fa5[]
			var response0 = new SearchResponse<object>();
			// end::78c4035e4fbf6851140660f6ed2a1fa5[]

			response0.MatchesExample(@"GET /_stats");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/stats.asciidoc:126")]
		public void Line126()
		{
			// tag::a861a89f52008610e813b9f073951c58[]
			var response0 = new SearchResponse<object>();
			// end::a861a89f52008610e813b9f073951c58[]

			response0.MatchesExample(@"GET /_stats/merge,refresh");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/stats.asciidoc:140")]
		public void Line140()
		{
			// tag::7a8de5606f283f4ef171b015eef6befa[]
			var response0 = new SearchResponse<object>();
			// end::7a8de5606f283f4ef171b015eef6befa[]

			response0.MatchesExample(@"GET /_stats/search?groups=group1,group2");
		}
	}
}
