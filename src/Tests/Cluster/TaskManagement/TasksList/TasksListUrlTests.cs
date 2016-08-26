using System.Threading.Tasks;
using Nest;
using Tests.Framework;

namespace Tests.Cluster.TaskManagement.TasksList
{
	public class TasksListUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.GET("/_tasks")
				.Fluent(c => c.TasksList())
				.Request(c => c.TasksList(new TasksListRequest()))
				.FluentAsync(c => c.TasksListAsync())
				.RequestAsync(c => c.TasksListAsync(new TasksListRequest()))
				;
		}
	}
}
