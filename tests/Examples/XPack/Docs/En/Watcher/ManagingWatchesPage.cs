// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.Watcher
{
	public class ManagingWatchesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/watcher/managing-watches.asciidoc:30")]
		public void Line30()
		{
			// tag::92a03184d36ce0367676952f8310771c[]
			var response0 = new SearchResponse<object>();
			// end::92a03184d36ce0367676952f8310771c[]

			response0.MatchesExample(@"GET .watches/_search
			{
			  ""size"" : 100
			}");
		}
	}
}
