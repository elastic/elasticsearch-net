// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
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
