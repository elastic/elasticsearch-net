/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
	public class SamplerAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
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
