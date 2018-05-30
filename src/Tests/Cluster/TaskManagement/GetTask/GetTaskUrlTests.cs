using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.Cluster.TaskManagement.GetTask
{
	public class GetTaskUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			var id = new TaskId("fakeid:1");

			await UrlTester.GET($"/_tasks/fakeid%3A1")
				.Fluent(c => c.GetTask(id))
				.Request(c => c.GetTask(new GetTaskRequest(id)))
				.FluentAsync(c => c.GetTaskAsync(id))
				.RequestAsync(c => c.GetTaskAsync(new GetTaskRequest(id)))
				;
		}
	}
}
