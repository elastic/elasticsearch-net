// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Bucket;

public class SignificantTermsAggregationUsageTests : AggregationUsageWithVerifyTestBase<ReadOnlyCluster>
{
	public SignificantTermsAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
		.SignificantTerms("significant_names", st => st
			.Field(p => p.Name)
			.MinDocCount(10)
			.MutualInformation(mi => mi
				.BackgroundIsSuperset()
				.IncludeNegatives()
			)
		);

	protected override AggregationDictionary InitializerAggregations =>
		new SignificantTermsAggregation("significant_names")
		{
			Field = Infer.Field<Project>(p => p.Name),
			MinDocCount = 10,
			MutualInformation = new MutualInformationHeuristic
			{
				BackgroundIsSuperset = true,
				IncludeNegatives = true
			}
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		response.Aggregations.Count.Should().Be(1);

		var sigNames = response.Aggregations.GetSignificantStringTerms("significant_names");

		sigNames.Should().NotBeNull();
		sigNames.DocCount.Should().BeGreaterThan(0);
	}
}
