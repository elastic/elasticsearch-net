using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.AliasManagement.Alias
{
	public class AliasUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_aliases")
				.Fluent(c=>c.Alias(b=>b))
				.Request(c=>c.Alias(new BulkAliasRequest()))
				.FluentAsync(c=>c.AliasAsync(b=>b))
				.RequestAsync(c=>c.AliasAsync(new BulkAliasRequest()))
				;

		}
	}
}
