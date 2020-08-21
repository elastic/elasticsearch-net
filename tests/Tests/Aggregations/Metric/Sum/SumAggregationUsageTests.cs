// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.Sum
{
	public class SumAggregationUsageTests : AggregationUsageTestBase
	{
		public SumAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commits_sum = new
			{
				sum = new
				{
					field = "numberOfCommits"
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Sum("commits_sum", sm => sm
				.Field(p => p.NumberOfCommits)
			);

		protected override AggregationDictionary InitializerAggs =>
			new SumAggregation("commits_sum", Field<Project>(p => p.NumberOfCommits));

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commitsSum = response.Aggregations.Sum("commits_sum");
			commitsSum.Should().NotBeNull();
			commitsSum.Value.Should().BeGreaterThan(0);
		}
	}
}
