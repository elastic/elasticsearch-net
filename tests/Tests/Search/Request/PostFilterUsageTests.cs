// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Request
{
	public class PostFilterUsageTests : SearchUsageTestBase
	{
		public PostFilterUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new { post_filter = new { match_all = new { } } };

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.PostFilter(f => f.MatchAll());


		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>()
			{
				PostFilter = new QueryContainer(new MatchAllQuery())
			};

		[I]
		public async Task ShouldHaveHits() => await AssertOnAllResponses((r) => { r.Hits.Should().NotBeNull(); });
	}
}
