using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.Cluster.TaskManagement.TasksList
{
	public class TasksListUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.GET("/_tasks")
			.Fluent(c => c.Tasks.ListTasks())
			.Request(c => c.Tasks.ListTasks(new ListTasksRequest()))
			.FluentAsync(c => c.Tasks.ListTasksAsync())
			.RequestAsync(c => c.Tasks.ListTasksAsync(new ListTasksRequest()));
	}
}
