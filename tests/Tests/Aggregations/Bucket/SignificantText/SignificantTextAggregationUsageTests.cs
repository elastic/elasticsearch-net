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
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Bucket.SignificantText
{
	/**
	 * An aggregation that returns interesting or unusual occurrences of free-text
	 * terms in a set. It is like the significant terms aggregation but differs in that:
	 *
	 * * It is specifically designed for use on type `text` fields
	 * * It does not require field data or doc-values
	 * * It re-analyzes text content on-the-fly meaning it can also filter duplicate sections of noisy text that otherwise tend to skew statistics.
	 *
	 * [WARNING]
	 * --
	 * Re-analyzing large result sets will require a lot of time and memory.
	 * It is recommended that the significant_text aggregation is used
	 * as a child of either the sampler or diversified sampler aggregation to
	 * limit the analysis to a small selection of top-matching documents
	 * e.g. 200. This will typically improve speed, memory use and quality
	 * of results.
	 * --
	 *
	 * See the Elasticsearch documentation on {ref_current}/search-aggregations-bucket-significanttext-aggregation.html[significant text aggregation] for more detail.
	 */
	public class SignificantTextAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		private readonly string _firstTenDescriptions =
			string.Join(" ", Project.First.Description.Split(' ').Distinct().Take(1024));

		public SignificantTextAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			significant_descriptions = new
			{
				significant_text = new
				{
					field = "description",
					filter_duplicate_text = true
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.SignificantText("significant_descriptions", st => st
				.Field(p => p.Description)
				.FilterDuplicateText()
			);

		protected override AggregationDictionary InitializerAggs =>
			new SignificantTextAggregation("significant_descriptions")
			{
				Field = Infer.Field<Project>(p => p.Description),
				FilterDuplicateText = true
			};

		protected override QueryContainer QueryScope => new MatchQuery
		{
			Field = Infer.Field<Project>(p => p.Description),
			Query = _firstTenDescriptions
		};

		protected override object QueryScopeJson => new
		{
			match = new
			{
				description = new
				{
					query = _firstTenDescriptions
				}
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var sigNames = response.Aggregations.SignificantText("significant_descriptions");
			sigNames.Should().NotBeNull();
			sigNames.DocCount.Should().BeGreaterThan(0);
			foreach (var bucket in sigNames.Buckets)
			{
				bucket.Key.Should().NotBeNullOrEmpty();
				bucket.BgCount.Should().BeGreaterThan(0);
				bucket.DocCount.Should().BeGreaterThan(0);
				bucket.Score.Should().BeGreaterThan(0);
			}
		}
	}
}
