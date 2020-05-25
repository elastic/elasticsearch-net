// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.Watcher
{
	public class TroubleshootingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/troubleshooting.asciidoc:18")]
		public void Line18()
		{
			// tag::2856a5ceff1861aa9a78099f1c517fe7[]
			var response0 = new SearchResponse<object>();
			// end::2856a5ceff1861aa9a78099f1c517fe7[]

			response0.MatchesExample(@"GET .watches/_mapping");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/troubleshooting.asciidoc:33")]
		public void Line33()
		{
			// tag::e905543b281e9c41395304da76ed2ea3[]
			var response0 = new SearchResponse<object>();
			// end::e905543b281e9c41395304da76ed2ea3[]

			response0.MatchesExample(@"DELETE .watches");
		}
	}
}
