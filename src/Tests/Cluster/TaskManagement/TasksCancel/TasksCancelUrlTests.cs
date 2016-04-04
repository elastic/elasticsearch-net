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
				.Fluent(c => c.TasksCancel())
				.Request(c => c.TasksCancel(new TasksCancelRequest()))
				.FluentAsync(c => c.TasksCancelAsync())
				.RequestAsync(c => c.TasksCancelAsync(new TasksCancelRequest()))
				;

			var taskId = "node:4";
			await POST($"/_tasks/node%3A4/_cancel")
				.Fluent(c => c.TasksCancel(t=>t.TaskId(taskId)))
				.Request(c => c.TasksCancel(new TasksCancelRequest(taskId)))
				.FluentAsync(c => c.TasksCancelAsync(t=>t.TaskId(taskId)))
				.RequestAsync(c => c.TasksCancelAsync(new TasksCancelRequest(taskId)))
				;

			var nodes = new []{  "node1", "node2" };
			var actions = new[] { "*reindex" };
			await POST($"/_tasks/_cancel?node_id=node1%2Cnode2&actions=%2Areindex")
				.Fluent(c => c.TasksCancel(t => t.NodeId(nodes).Actions(actions)))
				.Request(c => c.TasksCancel(new TasksCancelRequest { NodeId = nodes, Actions = actions }))
				.FluentAsync(c => c.TasksCancelAsync(t => t.NodeId(nodes).Actions(actions)))
				.RequestAsync(c => c.TasksCancelAsync(new TasksCancelRequest { NodeId = nodes, Actions = actions }))
				;
		}
	}
}
