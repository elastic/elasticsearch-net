using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatThreadPool
{
	public class CatThreadPoolUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/thread_pool")
				.Fluent(c => c.CatThreadPool())
				.Request(c => c.CatThreadPool(new CatThreadPoolRequest()))
				.FluentAsync(c => c.CatThreadPoolAsync())
				.RequestAsync(c => c.CatThreadPoolAsync(new CatThreadPoolRequest()))
				;
		}
	}
}
