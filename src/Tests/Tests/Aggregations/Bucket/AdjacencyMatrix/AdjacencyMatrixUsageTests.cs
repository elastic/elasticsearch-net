using System;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Aggregations.Bucket.AdjacencyMatrix
{
	[SkipVersion("<=5.3.0", "new feature")]
	public class AdjacencyMatrixUsageTests : AggregationUsageTestBase
	{
		public AdjacencyMatrixUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override object ExpectJson => new
		{
			size = 0,
			aggs = new
			{
				interactions = new
				{
					adjacency_matrix = new
					{
						filters = new
						{
							grpA = new { term = new { state = new { value = "BellyUp"} } },
							grpB = new { term = new { state = new { value = "Stable"} } },
							grpC = new { term = new { state = new { value = "VeryActive"} } }
						}
					}
				}
			}
		};

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Size =  0,
				Aggregations = new AdjacencyMatrixAggregation("interactions")
				{
					Filters = new NamedFiltersContainer
					{
						{ "grpA", new TermQuery { Field = "state", Value = StateOfBeing.BellyUp } },
						{ "grpB", new TermQuery { Field = "state", Value = StateOfBeing.Stable } },
						{ "grpC", new TermQuery { Field = "state", Value = StateOfBeing.VeryActive } },
					}
				}
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(0)
			.Aggregations(aggs => aggs
				.AdjacencyMatrix("interactions", am => am
					.Filters(fs => fs
						.Filter("grpA", f => f.Term(p => p.State, StateOfBeing.BellyUp))
						.Filter("grpB", f => f.Term(p => p.State, StateOfBeing.Stable))
						.Filter("grpC", f => f.Term(p => p.State, StateOfBeing.VeryActive))
					)
				)
			);

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var interactions = response.Aggs.AdjacencyMatrix("interactions");
			interactions.Should().NotBeNull();
			var buckets = interactions.Buckets;
			buckets.Should().NotBeNullOrEmpty();
			foreach (var bucket in buckets)
			{
				bucket.Key.Should().NotBeNullOrEmpty();
				bucket.DocCount.Should().BeGreaterThan(0);
			}
		}
	}
}
