using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatFielddata
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
				.Fluent(c => c.CatFielddata(f => f.Fields<Project>(p => p.Name, p => p.StartedOn)))
				.Request(c => c.CatFielddata(new CatFielddataRequest(fields)))
				.FluentAsync(c => c.CatFielddataAsync(f => f.Fields<Project>(p => p.Name, p => p.StartedOn)))
				.RequestAsync(c => c.CatFielddataAsync(new CatFielddataRequest(fields)))
				;
		}
	}
}
