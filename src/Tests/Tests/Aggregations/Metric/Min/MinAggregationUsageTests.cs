using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.Min
{
	public class MinAggregationUsageTests : AggregationUsageTestBase
	{
		public MinAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				min_last_activity = new
				{
					min = new
					{
						field = "lastActivity"
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Min("min_last_activity", m => m
					.Field(p => p.LastActivity)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new MinAggregation("min_last_activity", Field<Project>(p => p.LastActivity))
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var min = response.Aggs.Min("min_last_activity");
			min.Should().NotBeNull();
			min.Value.Should().BeGreaterThan(0);
			min.ValueAsString.Should().NotBeNullOrEmpty();
		}
	}
}
