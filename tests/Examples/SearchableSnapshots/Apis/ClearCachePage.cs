// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SearchableSnapshots.Apis
{
	public class ClearCachePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("searchable-snapshots/apis/clear-cache.asciidoc:73")]
		public void Line73()
		{
			// tag::69487bde55a1fc688a8cc5acf40b1555[]
			var response0 = new SearchResponse<object>();
			// end::69487bde55a1fc688a8cc5acf40b1555[]

			response0.MatchesExample(@"POST /docs/_searchable_snapshots/cache/clear");
		}
	}
}
