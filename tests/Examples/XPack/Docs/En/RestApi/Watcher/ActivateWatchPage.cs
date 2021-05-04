// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class ActivateWatchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/activate-watch.asciidoc:50")]
		public void Line50()
		{
			// tag::e827a9040e137410d62d10bb3b3cbb71[]
			var response0 = new SearchResponse<object>();
			// end::e827a9040e137410d62d10bb3b3cbb71[]

			response0.MatchesExample(@"GET _watcher/watch/my_watch");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/activate-watch.asciidoc:82")]
		public void Line82()
		{
			// tag::3477a89d869b1f7f72d50c2ca86c4679[]
			var response0 = new SearchResponse<object>();
			// end::3477a89d869b1f7f72d50c2ca86c4679[]

			response0.MatchesExample(@"PUT _watcher/watch/my_watch/_activate");
		}
	}
}
