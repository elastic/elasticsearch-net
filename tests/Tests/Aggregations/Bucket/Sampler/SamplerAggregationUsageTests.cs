// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Bucket.Sampler
{
	public class SamplerAggregationUsageTests : AggregationUsageTestBase
	{
		public SamplerAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			sample = new
			{
				sampler = new
				{
					shard_size = 200
				},
				aggs = new
				{
					significant_names = new
					{
						significant_terms = new
						{
							field = "name"
						}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Sampler("sample", sm => sm
				.ShardSize(200)
				.Aggregations(aa => aa
					.SignificantTerms("significant_names", st => st
						.Field(p => p.Name)
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new SamplerAggregation("sample")
			{
				ShardSize = 200,
				Aggregations = new SignificantTermsAggregation("significant_names")
				{
					Field = "name"
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var sample = response.Aggregations.Sampler("sample");
			sample.Should().NotBeNull();
			var sigTags = sample.SignificantTerms("significant_names");
			sigTags.Should().NotBeNull();
			sigTags.DocCount.Should().BeGreaterThan(0);
			if (TestConfiguration.Instance.InRange(">=5.5.0"))
				sigTags.BgCount.Should().BeGreaterThan(0);
		}
	}
}
