using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatNodes
{
	public class CatNodesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/nodes")
			.Fluent(c => c.Cat.Nodes())
			.Request(c => c.Cat.Nodes(new CatNodesRequest()))
			.FluentAsync(c => c.Cat.NodesAsync())
			.RequestAsync(c => c.Cat.NodesAsync(new CatNodesRequest()));
	}
}
