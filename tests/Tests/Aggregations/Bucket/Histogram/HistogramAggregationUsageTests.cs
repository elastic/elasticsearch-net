// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Histogram
{
	public class HistogramAggregationUsageTests : AggregationUsageTestBase
	{
		public HistogramAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commits = new
			{
				histogram = new
				{
					field = "numberOfCommits",
					interval = 100.0,
					min_doc_count = 1,
					order = new
					{
						_key = "desc"
					},
					offset = 1.1
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Histogram("commits", h => h
				.Field(p => p.NumberOfCommits)
				.Interval(100)
				.MinimumDocumentCount(1)
				.Order(HistogramOrder.KeyDescending)
				.Offset(1.1)
			);

		protected override AggregationDictionary InitializerAggs =>
			new HistogramAggregation("commits")
			{
				Field = Field<Project>(p => p.NumberOfCommits),
				Interval = 100,
				MinimumDocumentCount = 1,
				Order = HistogramOrder.KeyDescending,
				Offset = 1.1
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var commits = response.Aggregations.Histogram("commits");
			commits.Should().NotBeNull();
			commits.Buckets.Should().NotBeNull();
			commits.Buckets.Count.Should().BeGreaterThan(0);
			foreach (var item in commits.Buckets)
				item.DocCount.Should().BeGreaterThan(0);
		}
	}
}
