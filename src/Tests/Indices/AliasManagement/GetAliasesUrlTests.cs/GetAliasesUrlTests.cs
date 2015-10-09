using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.AliasManagement.GetAliases
{
	public class GetAliasesUrlTests
	{
		[U] public async Task Urls()
		{
			Name name = "hardcoded";
			IndexName index = "index";
			await GET($"/_aliases")
				.Fluent(c=>c.GetAliases())
				.Request(c=>c.GetAliases(new GetAliasesRequest()))
				.FluentAsync(c=>c.GetAliasesAsync())
				.RequestAsync(c=>c.GetAliasesAsync(new GetAliasesRequest()))
				;
			await GET($"/_aliases/hardcoded")
				.Fluent(c=>c.GetAliases(b=>b.Name(name)))
				.Request(c=>c.GetAliases(new GetAliasesRequest(name)))
				.FluentAsync(c=>c.GetAliasesAsync(b=>b.Name(name)))
				.RequestAsync(c=>c.GetAliasesAsync(new GetAliasesRequest(name)))
				;
			await GET($"/index/_aliases")
				.Fluent(c=>c.GetAliases(b=>b.Index(index)))
				.Request(c=>c.GetAliases(new GetAliasesRequest(index)))
				.FluentAsync(c=>c.GetAliasesAsync(b=>b.Index(index)))
				.RequestAsync(c=>c.GetAliasesAsync(new GetAliasesRequest(index)))
				;

			await GET($"/index/_aliases/hardcoded")
				.Fluent(c=>c.GetAliases(b=>b.Index(index).Name(name)))
				.Request(c=>c.GetAliases(new GetAliasesRequest(index, name)))
				.FluentAsync(c=>c.GetAliasesAsync(b=>b.Index(index).Name(name)))
				.RequestAsync(c=>c.GetAliasesAsync(new GetAliasesRequest(index, name)))
				;

		}
	}
}
