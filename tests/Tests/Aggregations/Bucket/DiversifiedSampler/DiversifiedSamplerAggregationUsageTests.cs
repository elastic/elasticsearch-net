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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Bucket.DiversifiedSampler
{
	[SkipVersion("<7.9.0", "introduced in 7.9.0")]
	public class DiversifiedSamplerAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public DiversifiedSamplerAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			diversified_sample = new
			{
				diversified_sampler = new
				{
					execution_hint = "global_ordinals",
					field = "type",
					max_docs_per_value = 10,
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
			.DiversifiedSampler("diversified_sample", sm => sm
				.ExecutionHint(DiversifiedSamplerAggregationExecutionHint.GlobalOrdinals)
				.Field(doc => doc.Type)
				.MaxDocsPerValue(10)
				.ShardSize(200)
				.Aggregations(aa => aa
					.SignificantTerms("significant_names", st => st
						.Field(p => p.Name)
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new DiversifiedSamplerAggregation("diversified_sample")
			{
				ExecutionHint = DiversifiedSamplerAggregationExecutionHint.GlobalOrdinals,
				Field = new Field("type"),
				MaxDocsPerValue = 10,
				ShardSize = 200,
				Aggregations = new SignificantTermsAggregation("significant_names")
				{
					Field = "name"
				}
			};
	}
}
