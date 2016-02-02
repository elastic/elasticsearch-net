using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Histogram
{
	public class HistogramAggregationUsageTests : AggregationUsageTestBase
	{
		public HistogramAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				commits = new
				{
					histogram = new
					{
						field = "numberOfCommits",
						interval = 100.0,
						missing = 0.0,
						order = new
						{
							_key = "desc"
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Histogram("commits", h => h
					.Field(p => p.NumberOfCommits)
					.Interval(100)
					.Missing(0)
					.Order(HistogramOrder.KeyDescending)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new HistogramAggregation("commits")
				{
					Field = Field<Project>(p => p.NumberOfCommits),
					Interval = 100,
					Missing = 0,
					Order = HistogramOrder.KeyDescending
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var commits = response.Aggs.Histogram("commits");
			commits.Should().NotBeNull();
			foreach (var item in commits.Buckets)
				item.DocCount.Should().BeGreaterThan(0);
		}
	}
}