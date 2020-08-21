// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Search.Search.Collapsing
{
	public class FieldCollapseUsageTests : SearchUsageTestBase
	{
		public FieldCollapseUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			collapse = new
			{
				field = "state",
				max_concurrent_group_searches = 1000,
				inner_hits = new
				{
					from = 1,
					name = "stateofbeing",
					size = 5
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Index(DefaultSeeder.ProjectsAliasFilter)
			.Collapse(c => c
				.Field(f => f.State)
				.MaxConcurrentGroupSearches(1000)
				.InnerHits(i => i
					.Name(nameof(StateOfBeing).ToLowerInvariant())
					.Size(5)
					.From(1)
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>(DefaultSeeder.ProjectsAliasFilter)
		{
			Collapse = new FieldCollapse
			{
				Field = Field<Project>(p => p.State),
				MaxConcurrentGroupSearches = 1000,
				InnerHits = new InnerHits
				{
					Name = nameof(StateOfBeing).ToLowerInvariant(),
					Size = 5,
					From = 1
				}
			}
		};

		protected override string UrlPath => $"/{DefaultSeeder.ProjectsAliasFilter}/_search";

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			var numberOfStates = Enum.GetValues(typeof(StateOfBeing)).Length;
			response.HitsMetadata.Total.Value.Should().BeGreaterThan(numberOfStates);
			response.Hits.Count.Should().Be(numberOfStates);
			foreach (var hit in response.Hits)
			{
				var name = nameof(StateOfBeing).ToLowerInvariant();
				hit.InnerHits.Should().NotBeNull().And.ContainKey(name);
				var innerHits = hit.InnerHits[name];
				innerHits.Hits.Total.Should().NotBeNull();
				innerHits.Hits.Total.Value.Should().BeGreaterThan(0);
			}
		}
	}

	[SkipVersion("<6.4.0", "2nd level collapsing is a new feature in 6.4.0")]
	public class FieldCollapseSecondLevelUsageTests : SearchUsageTestBase
	{
		public FieldCollapseSecondLevelUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			_source = new { excludes = new[] { "*" } },
			collapse = new
			{
				field = "state",
				inner_hits = new
				{
					_source = new
					{
						excludes = new[] { "*" }
					},
					collapse = new
					{
						field = "name"
					},
					from = 1,
					name = "stateofbeing",
					size = 5
				},
				max_concurrent_group_searches = 1000
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Source(source => source.ExcludeAll())
			.Index(DefaultSeeder.ProjectsAliasFilter)
			.Collapse(c => c
				.Field(f => f.State)
				.MaxConcurrentGroupSearches(1000)
				.InnerHits(i => i
					.Source(source => source.ExcludeAll())
					.Name(nameof(StateOfBeing).ToLowerInvariant())
					.Size(5)
					.From(1)
					.Collapse(c2 => c2
						.Field(p => p.Name)
					)
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>(DefaultSeeder.ProjectsAliasFilter)
		{
			Source = SourceFilter.ExcludeAll,
			Collapse = new FieldCollapse
			{
				Field = Field<Project>(p => p.State),
				MaxConcurrentGroupSearches = 1000,
				InnerHits = new InnerHits
				{
					Source = SourceFilter.ExcludeAll,
					Name = nameof(StateOfBeing).ToLowerInvariant(),
					Size = 5,
					From = 1,
					Collapse = new FieldCollapse
					{
						Field = Field<Project>(p => p.Name)
					}
				}
			}
		};

		protected override string UrlPath => $"/{DefaultSeeder.ProjectsAliasFilter}/_search";

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			var numberOfStates = Enum.GetValues(typeof(StateOfBeing)).Length;
			response.HitsMetadata.Total.Value.Should().BeGreaterThan(numberOfStates);
			response.Hits.Count.Should().Be(numberOfStates);
			foreach (var hit in response.Hits)
			{
				var name = nameof(StateOfBeing).ToLowerInvariant();
				hit.InnerHits.Should().NotBeNull().And.ContainKey(name);
				var innerHits = hit.InnerHits[name];
				innerHits.Hits.Total.Should().NotBeNull();
				innerHits.Hits.Total.Value.Should().BeGreaterThan(0);

				var i = 0;
				foreach (var innerHit in innerHits.Hits.Hits)
				{
					i++;
					innerHit.Fields.Should()
						.NotBeEmpty()
						.And.ContainKey("name");
				}

				i.Should().NotBe(0, "we expect to inspect 2nd level collapsed fields");
			}
		}
	}
}
