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

			var taskId = "node:4";
			await UrlTester.GET($"/_tasks/node%3A4")
				.Fluent(c => c.TasksList(t=>t.TaskId(taskId)))
				.Request(c => c.TasksList(new TasksListRequest(taskId)))
				.FluentAsync(c => c.TasksListAsync(t=>t.TaskId(taskId)))
				.RequestAsync(c => c.TasksListAsync(new TasksListRequest(taskId)))
				;
		}
	}
}
