// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Migration.Migrate80
{
	public class SnapshotsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("migration/migrate_8_0/snapshots.asciidoc:22")]
		public void Line22()
		{
			// tag::6458a2377155ecbdd2d3ebd0e1529201[]
			var response0 = new SearchResponse<object>();
			// end::6458a2377155ecbdd2d3ebd0e1529201[]

			response0.MatchesExample(@"GET _snapshot/repo1/snap1");
		}
	}
}
