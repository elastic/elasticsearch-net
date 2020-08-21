// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.Cluster.TaskManagement.TasksList
{
	public class TasksListUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.GET("/_tasks")
			.Fluent(c => c.Tasks.List())
			.Request(c => c.Tasks.List(new ListTasksRequest()))
			.FluentAsync(c => c.Tasks.ListAsync())
			.RequestAsync(c => c.Tasks.ListAsync(new ListTasksRequest()));
	}
}
