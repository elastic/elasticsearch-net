using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.AliasManagement.DeleteAlias
{
	public class DeleteAliasUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			var index = "index";
			await DELETE($"/{index}/_alias/{hardcoded}")
					.Fluent(c => c.Indices.DeleteAlias(index, hardcoded))
					.Request(c => c.Indices.DeleteAlias(new DeleteAliasRequest(index, hardcoded)))
					.FluentAsync(c => c.Indices.DeleteAliasAsync(index, hardcoded))
					.RequestAsync(c => c.Indices.DeleteAliasAsync(new DeleteAliasRequest(index, hardcoded)))
				;
		}
	}
}
