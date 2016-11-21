using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.WatcherInfo
{
	public class WatcherInfoUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_watcher/")
				.Fluent(c => c.WatcherInfo())
				.Request(c => c.WatcherInfo(new WatcherInfoRequest()))
				.FluentAsync(c => c.WatcherInfoAsync())
				.RequestAsync(c => c.WatcherInfoAsync(new WatcherInfoRequest()))
				;
		}
	}
}
