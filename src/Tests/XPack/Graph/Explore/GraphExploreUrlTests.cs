using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.XPack.Graph.Explore
{
	public class GraphExploreUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.POST("/project/_graph/explore")
				.Fluent(c => c.GraphExplore<Project>(d => d))
				.Request(c => c.GraphExplore(new GraphExploreRequest<Project>("project")))
				.FluentAsync(c => c.GraphExploreAsync<Project>(d => d))
				.RequestAsync(c => c.GraphExploreAsync(new GraphExploreRequest<Project>("project")))
				;


			var index = "another-index";
			await UrlTester.POST($"/{index}/project/_graph/explore")
				.Fluent(c => c.GraphExplore<Project>(d=>d.Type<Project>().Index(index)))
				.Request(c => c.GraphExplore(new GraphExploreRequest<Project>(index, Type<Project>())))
				.FluentAsync(c => c.GraphExploreAsync<Project>(d=>d.Type<Project>().Index(index)))
				.RequestAsync(c => c.GraphExploreAsync(new GraphExploreRequest<Project>(index, Type<Project>())))
				;
		}
	}
}
