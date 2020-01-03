using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Multiple.ReindexRethrottle
{
	public class ReindexRethrottleUrlTests : UrlTestsBase
	{
		private readonly TaskId _taskId = "rhtoNesNR4aXVIY2bRR4GQ:13056";

		[U] public override async Task Urls() => await POST($"/_reindex/{EscapeUriString(_taskId.ToString())}/_rethrottle")
			.Fluent(c => c.ReindexRethrottle(_taskId))
			.Request(c => c.ReindexRethrottle(new ReindexRethrottleRequest(_taskId)))
			.FluentAsync(c => c.ReindexRethrottleAsync(_taskId))
			.RequestAsync(c => c.ReindexRethrottleAsync(new ReindexRethrottleRequest(_taskId)));
	}
}
