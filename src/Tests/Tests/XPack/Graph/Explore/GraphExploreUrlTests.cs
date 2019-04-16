using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Nest.Infer;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Graph.Explore
{
	public class GraphExploreUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/project/_graph/explore")
					.Fluent(c => c.GraphExplore<Project>(d => d))
					.Request(c => c.GraphExplore(new GraphExploreRequest<Project>(typeof(Project))))
					.FluentAsync(c => c.GraphExploreAsync<Project>(d => d))
					.RequestAsync(c => c.GraphExploreAsync(new GraphExploreRequest<Project>(typeof(Project))))
				;

			var index = "another-index";
			await POST($"/{index}/_graph/explore")
					.Fluent(c => c.GraphExplore<Project>(d => d.Index(index)))
					.Request(c => c.GraphExplore(new GraphExploreRequest<Project>(index)))
					.FluentAsync(c => c.GraphExploreAsync<Project>(d => d.Index(index)))
					.RequestAsync(c => c.GraphExploreAsync(new GraphExploreRequest<Project>(index)))
				;
		}
	}
}
