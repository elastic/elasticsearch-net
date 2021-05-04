// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class SnapshotsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/snapshots.asciidoc:115")]
		public void Line115()
		{
			// tag::706fc4b9e4df1f6ee3fe34194492c20e[]
			var response0 = new SearchResponse<object>();
			// end::706fc4b9e4df1f6ee3fe34194492c20e[]

			response0.MatchesExample(@"GET /_cat/snapshots/repo1?v&s=id");
		}
	}
}
