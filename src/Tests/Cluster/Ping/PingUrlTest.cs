using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatAliases
{
	public class PingUrlTest : IUrlTest
	{
		[U] public async Task Urls()
		{
			await HEAD("/")
				.Fluent(c => c.Ping())
				.Request(c => c.Ping(new PingRequest()))
				.FluentAsync(c => c.PingAsync())
				.RequestAsync(c => c.PingAsync(new PingRequest()))
				;
		}
	}
}
