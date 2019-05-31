using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.AliasManagement.Alias
{
	public class AliasUrlTests
	{
		[U] public async Task Urls() => await POST($"/_aliases")
			.Fluent(c => c.Indices.BulkAlias(b => b))
			.Request(c => c.Indices.BulkAlias(new BulkAliasRequest()))
			.FluentAsync(c => c.Indices.BulkAliasAsync(b => b))
			.RequestAsync(c => c.Indices.BulkAliasAsync(new BulkAliasRequest()));
	}
}
