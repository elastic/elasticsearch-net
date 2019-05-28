using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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
					.Fluent(c => c.Indices.PutAlias(index, hardcoded))
					.Request(c => c.Indices.PutAlias(new PutAliasRequest(index, hardcoded)))
					.FluentAsync(c => c.Indices.PutAliasAsync(index, hardcoded))
					.RequestAsync(c => c.Indices.PutAliasAsync(new PutAliasRequest(index, hardcoded)))
				;
		}
	}
}
