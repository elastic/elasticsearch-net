using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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
