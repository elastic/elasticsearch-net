// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatPendingTasks
{
	public class CatPendingTasksUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/pending_tasks")
			.Fluent(c => c.Cat.PendingTasks())
			.Request(c => c.Cat.PendingTasks(new CatPendingTasksRequest()))
			.FluentAsync(c => c.Cat.PendingTasksAsync())
			.RequestAsync(c => c.Cat.PendingTasksAsync(new CatPendingTasksRequest()));
	}
}
