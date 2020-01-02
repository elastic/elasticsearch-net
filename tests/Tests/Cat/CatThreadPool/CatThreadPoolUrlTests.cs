using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatThreadPool
{
	public class CatThreadPoolUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/thread_pool")
			.Fluent(c => c.Cat.ThreadPool())
			.Request(c => c.Cat.ThreadPool(new CatThreadPoolRequest()))
			.FluentAsync(c => c.Cat.ThreadPoolAsync())
			.RequestAsync(c => c.Cat.ThreadPoolAsync(new CatThreadPoolRequest()));
	}
}
