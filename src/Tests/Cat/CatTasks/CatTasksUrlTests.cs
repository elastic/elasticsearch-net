using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatTasks
{
	public class CatTasksUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/tasks")
				.Fluent(c => c.CatTasks())
				.Request(c => c.CatTasks(new CatTasksRequest()))
				.FluentAsync(c => c.CatTasksAsync())
				.RequestAsync(c => c.CatTasksAsync(new CatTasksRequest()))
				;
		}
	}
}
