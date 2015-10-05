using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.AliasManagement
{
	public class AliasUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			var index = "index";
			await POST($"/_aliases")
				.Fluent(c=>c.Alias(b=>b))
				.Request(c=>c.Alias(new BulkAliasRequest()))
				.FluentAsync(c=>c.AliasAsync(b=>b))
				.RequestAsync(c=>c.AliasAsync(new BulkAliasRequest()))
				;

		}
	}
}
