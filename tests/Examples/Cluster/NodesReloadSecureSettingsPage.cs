/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
