// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Bucket.DiversifiedSampler
{
	[SkipVersion("<7.9.0", "introduced in 7.9.0")]
	public class DiversifiedSamplerAggregationUsageTests : AggregationUsageTestBase
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
