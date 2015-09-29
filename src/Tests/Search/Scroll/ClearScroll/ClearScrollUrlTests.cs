using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Scroll.ClearScroll
{
	public class ClearScrollUrlTests
	{
		[U] public async Task Urls()
		{
			await DELETE("/_search/scroll")
				.Fluent(c=>c.ClearScroll("scrollid1, scrollid2"))
				.Request(c=>c.ClearScroll(new ClearScrollRequest("scrollid1,scrollid2")))
				.FluentAsync(c=>c.ClearScrollAsync("scrollid1, scrollid2"))
				.RequestAsync(c=>c.ClearScrollAsync(new ClearScrollRequest("scrollid1,scrollid2")))
				;
		}
	}
}
