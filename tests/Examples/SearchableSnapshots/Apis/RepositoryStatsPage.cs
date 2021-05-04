// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SearchableSnapshots.Apis
{
	public class RepositoryStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("searchable-snapshots/apis/repository-stats.asciidoc:70")]
		public void Line70()
		{
			// tag::ad607e070bd3191558e3e2cf2b5bc2ea[]
			var response0 = new SearchResponse<object>();
			// end::ad607e070bd3191558e3e2cf2b5bc2ea[]

			response0.MatchesExample(@"GET /_snapshot/my_repository/_stats");
		}
	}
}
