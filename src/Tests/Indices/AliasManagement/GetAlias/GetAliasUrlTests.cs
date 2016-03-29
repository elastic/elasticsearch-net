using System.Threading.Tasks;
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
				.Fluent(c=>c.GetAlias())
				.Request(c=>c.GetAlias(new GetAliasRequest()))
				.FluentAsync(c=>c.GetAliasAsync())
				.RequestAsync(c=>c.GetAliasAsync(new GetAliasRequest()))
				;
			await GET($"/_alias/hardcoded")
				.Fluent(c=>c.GetAlias(b=>b.Name(name)))
				.Request(c=>c.GetAlias(new GetAliasRequest(name)))
				.FluentAsync(c=>c.GetAliasAsync(b=>b.Name(name)))
				.RequestAsync(c=>c.GetAliasAsync(new GetAliasRequest(name)))
				;
			await GET($"/index/_alias")
				.Fluent(c=>c.GetAlias(b=>b.Index(index)))
				.Request(c=>c.GetAlias(new GetAliasRequest(index)))
				.FluentAsync(c=>c.GetAliasAsync(b=>b.Index(index)))
				.RequestAsync(c=>c.GetAliasAsync(new GetAliasRequest(index)))
				;

			await GET($"/index/_alias/hardcoded")
				.Fluent(c=>c.GetAlias(b=>b.Index(index).Name(name)))
				.Request(c=>c.GetAlias(new GetAliasRequest(index, name)))
				.FluentAsync(c=>c.GetAliasAsync(b=>b.Index(index).Name(name)))
				.RequestAsync(c=>c.GetAliasAsync(new GetAliasRequest(index, name)))
				;

		}
	}
}
