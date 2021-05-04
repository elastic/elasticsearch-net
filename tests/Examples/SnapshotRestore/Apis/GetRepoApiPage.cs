// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SnapshotRestore.Apis
{
	public class GetRepoApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/get-repo-api.asciidoc:25")]
		public void Line25()
		{
			// tag::218b9009f120e8ad33f710e019179562[]
			var response0 = new SearchResponse<object>();
			// end::218b9009f120e8ad33f710e019179562[]

			response0.MatchesExample(@"GET /_snapshot/my_repository");
		}
	}
}