// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cluster
{
	public class NodesReloadSecureSettingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-reload-secure-settings.asciidoc:57")]
		public void Line57()
		{
			// tag::a28811aa25e10cfc38fe593c1615e1a1[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::a28811aa25e10cfc38fe593c1615e1a1[]

			response0.MatchesExample(@"POST _nodes/reload_secure_settings
			{
			  ""secure_settings_password"":""s3cr3t""
			}");

			response1.MatchesExample(@"POST _nodes/nodeId1,nodeId2/reload_secure_settings
			{
			  ""secure_settings_password"":""s3cr3t""
			}");
		}
	}
}
