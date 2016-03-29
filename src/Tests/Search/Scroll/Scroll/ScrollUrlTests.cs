using System;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Scroll.Scroll
{
	public class ScrollUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_search/scroll")
				.Fluent(c=>c.Scroll<CommitActivity>("1m", "scroll_id"))
				.Request(c=>c.Scroll<CommitActivity>(new ScrollRequest("scroll_id", TimeSpan.FromMinutes(1))))
				.FluentAsync(c=>c.ScrollAsync<CommitActivity>("1m", "scroll_id"))
				.RequestAsync(c=>c.ScrollAsync<CommitActivity>(new ScrollRequest("scroll_id", "1m")))
				;
		}
	}
}
