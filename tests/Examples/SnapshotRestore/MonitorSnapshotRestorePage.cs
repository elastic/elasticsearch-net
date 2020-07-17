// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.SnapshotRestore
{
	public class MonitorSnapshotRestorePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/monitor-snapshot-restore.asciidoc:32")]
		public void Line32()
		{
			// tag::020c56e520ff6556ebfaf98efaef56aa[]
			var response0 = new SearchResponse<object>();
			// end::020c56e520ff6556ebfaf98efaef56aa[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/monitor-snapshot-restore.asciidoc:43")]
		public void Line43()
		{
			// tag::e566ca0098be82a2847c17069711a822[]
			var response0 = new SearchResponse<object>();
			// end::e566ca0098be82a2847c17069711a822[]

			response0.MatchesExample(@"GET /_snapshot/my_backup/snapshot_1/_status");
		}

		[U(Skip = "Example not implemented")]
		[Description("snapshot-restore/monitor-snapshot-restore.asciidoc:73")]
		public void Line73()
		{
			// tag::86c723fc6212d34166661e7dac223491[]
			var response0 = new SearchResponse<object>();
			// end::86c723fc6212d34166661e7dac223491[]

			response0.MatchesExample(@"DELETE /_snapshot/my_backup/snapshot_1");
		}
	}
}
