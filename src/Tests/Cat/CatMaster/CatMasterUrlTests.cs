using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatMaster
{
	public class CatMasterUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/master")
				.Fluent(c => c.CatMaster())
				.Request(c => c.CatMaster(new CatMasterRequest()))
				.FluentAsync(c => c.CatMasterAsync())
				.RequestAsync(c => c.CatMasterAsync(new CatMasterRequest()))
				;
		}
	}
}
