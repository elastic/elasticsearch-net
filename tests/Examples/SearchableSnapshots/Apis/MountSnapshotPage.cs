// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SearchableSnapshots.Apis
{
	public class MountSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("searchable-snapshots/apis/mount-snapshot.asciidoc:110")]
		public void Line110()
		{
			// tag::098820242e25a05d6a4d962ad4132d1b[]
			var response0 = new SearchResponse<object>();
			// end::098820242e25a05d6a4d962ad4132d1b[]

			response0.MatchesExample(@"POST /_snapshot/my_repository/my_snapshot/_mount?wait_for_completion=true
			{
			  ""index"": ""my_docs"", <1>
			  ""renamed_index"": ""docs"", <2>
			  ""index_settings"": { <3>
			    ""index.number_of_replicas"": 0
			  },
			  ""ignored_index_settings"": [ ""index.refresh_interval"" ] <4>
			}");
		}
	}
}
