using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.AliasManagement.DeleteAlias
{
	public class DeleteAliasUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			var index = "index";
			await DELETE($"/{index}/_alias/{hardcoded}")
				.Fluent(c=>c.DeleteAlias(index, hardcoded))
				.Request(c=>c.DeleteAlias(new DeleteAliasRequest(index, hardcoded)))
				.FluentAsync(c=>c.DeleteAliasAsync(index, hardcoded))
				.RequestAsync(c=>c.DeleteAliasAsync(new DeleteAliasRequest(index, hardcoded)))
				;

		}
	}
}
