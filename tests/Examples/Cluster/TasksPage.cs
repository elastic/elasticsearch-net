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
	public class TasksPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cluster/tasks.asciidoc:64")]
		public void Line64()
		{
			// tag::166bcfc6d5d39defec7ad6aa44d0914b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::166bcfc6d5d39defec7ad6aa44d0914b[]

			response0.MatchesExample(@"GET _tasks \<1>");

			response1.MatchesExample(@"GET _tasks?nodes=nodeId1,nodeId2 \<2>");

			response2.MatchesExample(@"GET _tasks?nodes=nodeId1,nodeId2&actions=cluster:* \<3>");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/tasks.asciidoc:118")]
		public void Line118()
		{
			// tag::33610800d9de3c3e6d6b3c611ace7330[]
			var response0 = new SearchResponse<object>();
			// end::33610800d9de3c3e6d6b3c611ace7330[]

			response0.MatchesExample(@"GET _tasks/oTUltX4IQMOUUVeiohTt8A:124");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/tasks.asciidoc:128")]
		public void Line128()
		{
			// tag::29824032d7d64512d17458fdd687b1f6[]
			var response0 = new SearchResponse<object>();
			// end::29824032d7d64512d17458fdd687b1f6[]

			response0.MatchesExample(@"GET _tasks?parent_task_id=oTUltX4IQMOUUVeiohTt8A:123");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/tasks.asciidoc:143")]
		public void Line143()
		{
			// tag::8f4a7f68f2ca3698abdf20026a2d8c5f[]
			var response0 = new SearchResponse<object>();
			// end::8f4a7f68f2ca3698abdf20026a2d8c5f[]

			response0.MatchesExample(@"GET _tasks?actions=*search&detailed");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/tasks.asciidoc:206")]
		public void Line206()
		{
			// tag::93fb59d3204f37af952198b331fb6bb7[]
			var response0 = new SearchResponse<object>();
			// end::93fb59d3204f37af952198b331fb6bb7[]

			response0.MatchesExample(@"GET _tasks/oTUltX4IQMOUUVeiohTt8A:12345?wait_for_completion=true&timeout=10s");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/tasks.asciidoc:215")]
		public void Line215()
		{
			// tag::77447e2966708e92f5e219d43ac3f00d[]
			var response0 = new SearchResponse<object>();
			// end::77447e2966708e92f5e219d43ac3f00d[]

			response0.MatchesExample(@"GET _tasks?actions=*reindex&wait_for_completion=true&timeout=10s");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/tasks.asciidoc:226")]
		public void Line226()
		{
			// tag::d89d36741d906a71eca6c144e8d83889[]
			var response0 = new SearchResponse<object>();
			// end::d89d36741d906a71eca6c144e8d83889[]

			response0.MatchesExample(@"POST _tasks/oTUltX4IQMOUUVeiohTt8A:12345/_cancel");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/tasks.asciidoc:241")]
		public void Line241()
		{
			// tag::612c2e975f833de9815651135735eae5[]
			var response0 = new SearchResponse<object>();
			// end::612c2e975f833de9815651135735eae5[]

			response0.MatchesExample(@"POST _tasks/_cancel?nodes=nodeId1,nodeId2&actions=*reindex");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/tasks.asciidoc:252")]
		public void Line252()
		{
			// tag::bd3d710ec50a151453e141691163af72[]
			var response0 = new SearchResponse<object>();
			// end::bd3d710ec50a151453e141691163af72[]

			response0.MatchesExample(@"GET _tasks?group_by=parents");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/tasks.asciidoc:259")]
		public void Line259()
		{
			// tag::a3ce0cfe2176f3d8a36959a5916995f0[]
			var response0 = new SearchResponse<object>();
			// end::a3ce0cfe2176f3d8a36959a5916995f0[]

			response0.MatchesExample(@"GET _tasks?group_by=none");
		}
	}
}
