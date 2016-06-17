using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Infer;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Graph.Explore
{
	public class GraphExploreUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/project/project/_xpack/graph/_explore")
				.Fluent(c => c.GraphExplore<Project>(d => d))
				.Request(c => c.GraphExplore(new GraphExploreRequest<Project>(typeof(Project), typeof(Project))))
				.FluentAsync(c => c.GraphExploreAsync<Project>(d => d))
				.RequestAsync(c => c.GraphExploreAsync(new GraphExploreRequest<Project>(typeof(Project), typeof(Project))))
				;

			await POST("/project/_xpack/graph/_explore")
				.Fluent(c => c.GraphExplore<Project>(d => d.AllTypes()))
				.Request(c => c.GraphExplore(new GraphExploreRequest<Project>(typeof(Project))))
				.FluentAsync(c => c.GraphExploreAsync<Project>(d => d.AllTypes()))
				.RequestAsync(c => c.GraphExploreAsync(new GraphExploreRequest<Project>(typeof(Project))))
				;

			await POST($"_all/_xpack/graph/_explore")
				.Fluent(c => c.GraphExplore<Project>(d => d.AllTypes().AllIndices()))
				.Request(c => c.GraphExplore(new GraphExploreRequest<Project>(AllIndices, AllTypes)))
				.FluentAsync(c => c.GraphExploreAsync<Project>(d => d.AllTypes().AllIndices()))
				.RequestAsync(c => c.GraphExploreAsync(new GraphExploreRequest<Project>(AllIndices, AllTypes)))
				;

			var index = "another-index";
			await POST($"/{index}/_xpack/graph/_explore")
				.Fluent(c => c.GraphExplore<Project>(d => d.AllTypes().Index(index)))
				.Request(c => c.GraphExplore(new GraphExploreRequest<Project>(index, AllTypes)))
				.FluentAsync(c => c.GraphExploreAsync<Project>(d => d.AllTypes().Index(index)))
				.RequestAsync(c => c.GraphExploreAsync(new GraphExploreRequest<Project>(index, AllTypes)))
				;

			await POST($"/{index}/project/_xpack/graph/_explore")
				.Fluent(c => c.GraphExplore<Project>(d=>d.Type<Project>().Index(index)))
				.Request(c => c.GraphExplore(new GraphExploreRequest<Project>(index, Type<Project>())))
				.FluentAsync(c => c.GraphExploreAsync<Project>(d=>d.Type<Project>().Index(index)))
				.RequestAsync(c => c.GraphExploreAsync(new GraphExploreRequest<Project>(index, Type<Project>())))
				;
		}
	}
}
