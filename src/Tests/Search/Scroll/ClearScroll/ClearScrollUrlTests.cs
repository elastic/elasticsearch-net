using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Scroll.ClearScroll
{
	public class ClearScrollUrlTests
	{
		[U] public async Task Urls()
		{
			await DELETE("/_search/scroll")
				.Fluent(c=>c.ClearScroll(cs => cs.ScrollId("scrollid1, scrollid2")))
				.Request(c=>c.ClearScroll(new ClearScrollRequest(new[] { "scrollid1, scrollid2" })))
				.FluentAsync(c=>c.ClearScrollAsync(cs => cs.ScrollId("scrollid1, scrollid2")))
				.RequestAsync(c=>c.ClearScrollAsync(new ClearScrollRequest(new[] { "scrollid1, scrollid2" })))
				;
		}
	}
}
