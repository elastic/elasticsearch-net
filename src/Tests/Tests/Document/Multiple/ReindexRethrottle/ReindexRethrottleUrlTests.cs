using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Document.Multiple.ReindexRethrottle
{
	public class ReindexRethrottleUrlTests : UrlTestsBase
	{
		private readonly TaskId _taskId = "rhtoNesNR4aXVIY2bRR4GQ:13056";

		[U] public override async Task Urls()
		{
			await POST($"/_reindex/{EscapeUriString(_taskId.ToString())}/_rethrottle")
				.Fluent(c => c.Rethrottle(_taskId))
				.Request(c => c.Rethrottle(new ReindexRethrottleRequest(_taskId)))
				.FluentAsync(c => c.RethrottleAsync(_taskId))
				.RequestAsync(c => c.RethrottleAsync(new ReindexRethrottleRequest(_taskId)))
				;
		}
	}
}
