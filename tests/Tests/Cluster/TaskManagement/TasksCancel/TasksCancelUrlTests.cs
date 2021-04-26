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

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cluster.TaskManagement.TasksCancel
{
	public class TasksCancelUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_tasks/_cancel")
					.Fluent(c => c.Tasks.Cancel())
					.Request(c => c.Tasks.Cancel(new CancelTasksRequest()))
					.FluentAsync(c => c.Tasks.CancelAsync())
					.RequestAsync(c => c.Tasks.CancelAsync(new CancelTasksRequest()))
				;

			var taskId = "node:4";
			await POST($"/_tasks/node%3A4/_cancel")
					.Fluent(c => c.Tasks.Cancel(t => t.TaskId(taskId)))
					.Request(c => c.Tasks.Cancel(new CancelTasksRequest(taskId)))
					.FluentAsync(c => c.Tasks.CancelAsync(t => t.TaskId(taskId)))
					.RequestAsync(c => c.Tasks.CancelAsync(new CancelTasksRequest(taskId)))
				;

			var nodes = new[] { "node1", "node2" };
			var actions = new[] { "*reindex" };
			await POST($"/_tasks/_cancel?nodes=node1%2Cnode2&actions=%2Areindex")
					.Fluent(c => c.Tasks.Cancel(t => t.Nodes(nodes).Actions(actions)))
					.Request(c => c.Tasks.Cancel(new CancelTasksRequest { Nodes = nodes, Actions = actions }))
					.FluentAsync(c => c.Tasks.CancelAsync(t => t.Nodes(nodes).Actions(actions)))
					.RequestAsync(c => c.Tasks.CancelAsync(new CancelTasksRequest { Nodes = nodes, Actions = actions }))
				;
		}
	}
}
