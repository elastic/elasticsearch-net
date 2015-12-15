using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.AliasManagement.PutAlias
{
	public class PutAliasUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			var index = "index";
			await PUT($"/{index}/_alias/{hardcoded}")
				.Fluent(c=>c.PutAlias(index, hardcoded))
				.Request(c=>c.PutAlias(new PutAliasRequest(index, hardcoded)))
				.FluentAsync(c=>c.PutAliasAsync(index, hardcoded))
				.RequestAsync(c=>c.PutAliasAsync(new PutAliasRequest(index, hardcoded)))
				;

		}
	}
}
