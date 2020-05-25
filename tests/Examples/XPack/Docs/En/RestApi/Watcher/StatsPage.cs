// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class StatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/stats.asciidoc:84")]
		public void Line84()
		{
			// tag::17266cee5eaaddf08e5534bf580a1910[]
			var response0 = new SearchResponse<object>();
			// end::17266cee5eaaddf08e5534bf580a1910[]

			response0.MatchesExample(@"GET _watcher/stats");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/stats.asciidoc:112")]
		public void Line112()
		{
			// tag::3ed79871d956bfb2d6d2721d7272520c[]
			var response0 = new SearchResponse<object>();
			// end::3ed79871d956bfb2d6d2721d7272520c[]

			response0.MatchesExample(@"GET _watcher/stats?metric=current_watches");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/stats.asciidoc:119")]
		public void Line119()
		{
			// tag::56b6b50b174a935d368301ebd717231d[]
			var response0 = new SearchResponse<object>();
			// end::56b6b50b174a935d368301ebd717231d[]

			response0.MatchesExample(@"GET _watcher/stats/current_watches");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/stats.asciidoc:163")]
		public void Line163()
		{
			// tag::6244204213f60edf2f23295f9059f2c9[]
			var response0 = new SearchResponse<object>();
			// end::6244204213f60edf2f23295f9059f2c9[]

			response0.MatchesExample(@"GET _watcher/stats/queued_watches");
		}
	}
}
