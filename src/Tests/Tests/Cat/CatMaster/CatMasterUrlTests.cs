using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatMaster
{
	public class CatMasterUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/master")
			.Fluent(c => c.Cat.Master())
			.Request(c => c.Cat.Master(new CatMasterRequest()))
			.FluentAsync(c => c.Cat.MasterAsync())
			.RequestAsync(c => c.Cat.MasterAsync(new CatMasterRequest()));
	}
}
