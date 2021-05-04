// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SnapshotRestore.Apis
{
	public class DeleteRepoApiPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/apis/delete-repo-api.asciidoc:27")]
		public void Line27()
		{
			// tag::ff56ded50c65998c70f3c5691ddc6f86[]
			var response0 = new SearchResponse<object>();
			// end::ff56ded50c65998c70f3c5691ddc6f86[]

			response0.MatchesExample(@"DELETE /_snapshot/my_repository");
		}
	}
}