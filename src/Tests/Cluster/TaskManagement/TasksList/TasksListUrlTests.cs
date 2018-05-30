using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.Cluster.TaskManagement.TasksList
{
	public class TasksListUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.GET("/_tasks")
				.Fluent(c => c.ListTasks())
				.Request(c => c.ListTasks(new ListTasksRequest()))
				.FluentAsync(c => c.ListTasksAsync())
				.RequestAsync(c => c.ListTasksAsync(new ListTasksRequest()))
				;
		}
	}
}
