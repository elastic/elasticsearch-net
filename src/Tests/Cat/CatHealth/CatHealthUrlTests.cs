using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatHealth
{
	public class CatHealthUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/health")
				.Fluent(c => c.CatHealth())
				.Request(c => c.CatHealth(new CatHealthRequest()))
				.FluentAsync(c => c.CatHealthAsync())
				.RequestAsync(c => c.CatHealthAsync(new CatHealthRequest()))
				;
		}
	}
}
