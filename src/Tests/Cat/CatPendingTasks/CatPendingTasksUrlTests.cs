using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatPendingTasks
{
	public class CatPendingTasksUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/pending_tasks")
				.Fluent(c => c.CatPendingTasks())
				.Request(c => c.CatPendingTasks(new CatPendingTasksRequest()))
				.FluentAsync(c => c.CatPendingTasksAsync())
				.RequestAsync(c => c.CatPendingTasksAsync(new CatPendingTasksRequest()))
				;
		}
	}
}
