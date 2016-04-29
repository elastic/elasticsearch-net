using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Document.Multiple.ReindexRethrottle
{
	public class ReindexRethrottleUrlTests : IUrlTests
	{
		private readonly TaskId _taskId = "rhtoNesNR4aXVIY2bRR4GQ:13056";

		[U] public async Task Urls()
		{
			await POST($"/_reindex/rhtoNesNR4aXVIY2bRR4GQ%3A13056/_rethrottle")
				.Fluent(c => c.ReindexRethrottle(f=>f.TaskId(_taskId)))
				.Request(c => c.ReindexRethrottle(new ReindexRethrottleRequest(_taskId)))
				.FluentAsync(c => c.ReindexRethrottleAsync(f=>f.TaskId(_taskId)))
				.RequestAsync(c => c.ReindexRethrottleAsync(new ReindexRethrottleRequest(_taskId)))
				;

		}
	}
}
