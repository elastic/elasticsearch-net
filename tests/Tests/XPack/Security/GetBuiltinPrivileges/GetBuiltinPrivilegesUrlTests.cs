using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.GetBuiltinPrivileges
{
	public class GetBuiltinPrivilegesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await GET("/_security/privilege/_builtin")
				.Fluent(c => c.Security.GetBuiltinPrivileges())
				.Request(c => c.Security.GetBuiltinPrivileges(new GetBuiltinPrivilegesRequest()))
				.FluentAsync(c => c.Security.GetBuiltinPrivilegesAsync())
				.RequestAsync(c => c.Security.GetBuiltinPrivilegesAsync(new GetBuiltinPrivilegesRequest()));
	}
}
