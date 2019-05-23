using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.AliasManagement.GetAlias
{
	public class GetAliasUrlTests
	{
		[U] public async Task Urls()
		{
			Name name = "hardcoded";
			IndexName index = "index";
			await GET($"/_alias")
					.Fluent(c => c.Indices.GetAlias())
					.Request(c => c.Indices.GetAlias(new GetAliasRequest()))
					.FluentAsync(c => c.Indices.GetAliasAsync())
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest()))
				;
			await GET($"/_alias/hardcoded")
					.Fluent(c => c.Indices.GetAlias(b => b.Name(name)))
					.Request(c => c.Indices.GetAlias(new GetAliasRequest(name)))
					.FluentAsync(c => c.Indices.GetAliasAsync(b => b.Name(name)))
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(name)))
				;
			await GET($"/index/_alias")
					.Fluent(c => c.Indices.GetAlias(b => b.Index(index)))
					.Request(c => c.Indices.GetAlias(new GetAliasRequest(index)))
					.FluentAsync(c => c.Indices.GetAliasAsync(b => b.Index(index)))
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(index)))
				;

			await GET($"/index/_alias/hardcoded")
					.Fluent(c => c.Indices.GetAlias(b => b.Index(index).Name(name)))
					.Request(c => c.Indices.GetAlias(new GetAliasRequest(index, name)))
					.FluentAsync(c => c.Indices.GetAliasAsync(b => b.Index(index).Name(name)))
					.RequestAsync(c => c.Indices.GetAliasAsync(new GetAliasRequest(index, name)))
				;
		}
	}
}
