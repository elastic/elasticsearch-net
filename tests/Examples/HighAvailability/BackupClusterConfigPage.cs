// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.HighAvailability
{
	public class BackupClusterConfigPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("high-availability/backup-cluster-config.asciidoc:41")]
		public void Line41()
		{
			// tag::ee79edafcbc80dfda496e3a26506dcbc[]
			var response0 = new SearchResponse<object>();
			// end::ee79edafcbc80dfda496e3a26506dcbc[]

			response0.MatchesExample(@"GET _cluster/settings?pretty&flat_settings&filter_path=persistent");
		}
	}
}
