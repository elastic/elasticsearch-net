// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.Cluster.TaskManagement.GetTask
{
	public class GetTaskUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var id = new TaskId("fakeid:1");

			await UrlTester.GET($"/_tasks/fakeid%3A1")
					.Fluent(c => c.Tasks.GetTask(id))
					.Request(c => c.Tasks.GetTask(new GetTaskRequest(id)))
					.FluentAsync(c => c.Tasks.GetTaskAsync(id))
					.RequestAsync(c => c.Tasks.GetTaskAsync(new GetTaskRequest(id)))
				;
		}
	}
}
