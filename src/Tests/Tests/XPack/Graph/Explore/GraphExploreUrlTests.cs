using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Graph.Explore
{
	public class GraphExploreUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/project/_graph/explore")
					.Fluent(c => c.Graph.Explore<Project>(d => d))
					.Request(c => c.Graph.Explore(new GraphExploreRequest<Project>(typeof(Project))))
					.FluentAsync(c => c.Graph.ExploreAsync<Project>(d => d))
					.RequestAsync(c => c.Graph.ExploreAsync(new GraphExploreRequest<Project>(typeof(Project))))
				;

			var index = "another-index";
			await POST($"/{index}/_graph/explore")
					.Fluent(c => c.Graph.Explore<Project>(d => d.Index(index)))
					.Request(c => c.Graph.Explore(new GraphExploreRequest<Project>(index)))
					.FluentAsync(c => c.Graph.ExploreAsync<Project>(d => d.Index(index)))
					.RequestAsync(c => c.Graph.ExploreAsync(new GraphExploreRequest<Project>(index)))
				;
		}
	}
}
