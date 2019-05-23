using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatTasks
{
	public class CatTasksUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/tasks")
			.Fluent(c => c.Cat.Tasks())
			.Request(c => c.Cat.Tasks(new CatTasksRequest()))
			.FluentAsync(c => c.Cat.TasksAsync())
			.RequestAsync(c => c.Cat.TasksAsync(new CatTasksRequest()));
	}
}
