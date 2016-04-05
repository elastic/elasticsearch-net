using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.Percentiles
{
	public class PercentilesAggregationUsageTests : AggregationUsageTestBase
	{
		public PercentilesAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				commits_outlier = new
				{
					percentiles = new
					{
						field = "numberOfCommits",
						percents = new[] { 95.0, 99.0, 99.9 },
						hdr = new
						{
							number_of_significant_value_digits = 3
						},
						script = new
						{
							inline = "doc['numberOfCommits'].value * 1.2"
						},
						missing = 0.0
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Percentiles("commits_outlier", pr => pr
					.Field(p => p.NumberOfCommits)
					.Percents(95, 99, 99.9)
					.Method(m => m
						.HDRHistogram(hdr => hdr
							.NumberOfSignificantValueDigits(3)
						)
					)
					.Script("doc['numberOfCommits'].value * 1.2")
					.Missing(0)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new PercentilesAggregation("commits_outlier", Field<Project>(p => p.NumberOfCommits))
				{
					Percents = new[] { 95, 99, 99.9 },
					Method = new HDRHistogramMethod
					{
						NumberOfSignificantValueDigits = 3
					},
					Script = new InlineScript("doc['numberOfCommits'].value * 1.2"),
					Missing = 0
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var commitsOutlier = response.Aggs.Percentiles("commits_outlier");
			commitsOutlier.Should().NotBeNull();
			commitsOutlier.Items.Should().NotBeNullOrEmpty();
			foreach (var item in commitsOutlier.Items)
				item.Value.Should().BeGreaterThan(0);
		}
	}
}
