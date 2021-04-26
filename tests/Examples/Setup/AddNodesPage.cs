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
using System.ComponentModel;
using Nest;

namespace Examples.Setup
{
	public class AddNodesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("setup/add-nodes.asciidoc:111")]
		public void Line111()
		{
			// tag::4e1f02928ef243bf07fd425754b7642b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::4e1f02928ef243bf07fd425754b7642b[]

			response0.MatchesExample(@"POST /_cluster/voting_config_exclusions?node_names=node_name");

			response1.MatchesExample(@"POST /_cluster/voting_config_exclusions?node_names=node_name&timeout=1m");
		}

		[U(Skip = "Example not implemented")]
		[Description("setup/add-nodes.asciidoc:154")]
		public void Line154()
		{
			// tag::92f073762634a4b2274f71002494192e[]
			var response0 = new SearchResponse<object>();
			// end::92f073762634a4b2274f71002494192e[]

			response0.MatchesExample(@"GET /_cluster/state?filter_path=metadata.cluster_coordination.voting_config_exclusions");
		}

		[U(Skip = "Example not implemented")]
		[Description("setup/add-nodes.asciidoc:172")]
		public void Line172()
		{
			// tag::ead4d875877d618594d0cdbdd9b7998b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::ead4d875877d618594d0cdbdd9b7998b[]

			response0.MatchesExample(@"DELETE /_cluster/voting_config_exclusions");

			response1.MatchesExample(@"DELETE /_cluster/voting_config_exclusions?wait_for_removal=false");
		}
	}
}
