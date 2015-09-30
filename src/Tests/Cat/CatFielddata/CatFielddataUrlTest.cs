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
	public class CatFielddataUrlTest : IUrlTest
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/fielddata")
				.Fluent(c => c.CatFielddata())
				.Request(c => c.CatFielddata(new CatFielddataRequest()))
				.FluentAsync(c => c.CatFielddataAsync())
				.RequestAsync(c => c.CatFielddataAsync(new CatFielddataRequest()))
				;

			var fields = new[] { "name", "startedOn" };

			await GET("/_cat/fielddata/name,startedOn")
				.Fluent(c => c.CatFielddata(f => f.Fields(fields)))
				.Request(c => c.CatFielddata(new CatFielddataRequest { Fields = fields }))
				.FluentAsync(c => c.CatFielddataAsync(f => f.Fields(fields)))
				.RequestAsync(c => c.CatFielddataAsync(new CatFielddataRequest { Fields = fields }))
				;
		}
	}
}
