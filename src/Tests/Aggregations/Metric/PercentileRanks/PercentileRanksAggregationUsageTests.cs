using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.PercentileRanks
{
	public class PercentileRanksAggregationUsageTests : AggregationUsageTestBase
	{
		public PercentileRanksAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				commits_outlier = new
				{
					percentile_ranks = new
					{
						field = "numberOfCommits",
						values = new [] { 15.0, 30.0 },
						tdigest = new
						{
							compression = 200.0
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
				.PercentileRanks("commits_outlier", pr => pr
					.Field(p => p.NumberOfCommits)
					.Values(15, 30)
					.Method(m => m
						.TDigest(td => td
							.Compression(200)
						)
					)
					.Script("doc['numberOfCommits'].value * 1.2")
					.Missing(0)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new PercentileRanksAggregation("commits_outlier", Field<Project>(p => p.NumberOfCommits))
				{
					Values = new List<double> { 15, 30 },
					Method = new TDigestMethod
					{
						Compression = 200
					},
					Script = (InlineScript)"doc['numberOfCommits'].value * 1.2",
					Missing = 0
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var commitsOutlier = response.Aggs.PercentileRanks("commits_outlier");
			commitsOutlier.Should().NotBeNull();
			commitsOutlier.Items.Should().NotBeNullOrEmpty();
			foreach (var item in commitsOutlier.Items)
				item.Should().NotBeNull();
		}
	}
}
