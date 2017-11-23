using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.TaskManagement.TasksCancel
{
	public class TasksCancelUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_tasks/_cancel")
				.Fluent(c => c.CancelTasks())
				.Request(c => c.CancelTasks(new CancelTasksRequest()))
				.FluentAsync(c => c.CancelTasksAsync())
				.RequestAsync(c => c.CancelTasksAsync(new CancelTasksRequest()))
				;

			var taskId = "node:4";
			await POST($"/_tasks/node%3A4/_cancel")
				.Fluent(c => c.CancelTasks(t=>t.TaskId(taskId)))
				.Request(c => c.CancelTasks(new CancelTasksRequest(taskId)))
				.FluentAsync(c => c.CancelTasksAsync(t=>t.TaskId(taskId)))
				.RequestAsync(c => c.CancelTasksAsync(new CancelTasksRequest(taskId)))
				;

			var nodes = new []{  "node1", "node2" };
			var actions = new[] { "*reindex" };
			await POST($"/_tasks/_cancel?nodes=node1%2Cnode2&actions=%2Areindex")
				.Fluent(c => c.CancelTasks(t => t.Nodes(nodes).Actions(actions)))
				.Request(c => c.CancelTasks(new CancelTasksRequest { Nodes = nodes, Actions = actions }))
				.FluentAsync(c => c.CancelTasksAsync(t => t.Nodes(nodes).Actions(actions)))
				.RequestAsync(c => c.CancelTasksAsync(new CancelTasksRequest { Nodes = nodes, Actions = actions }))
				;
		}
	}
}
