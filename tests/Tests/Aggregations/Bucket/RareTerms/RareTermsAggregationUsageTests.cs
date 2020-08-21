// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Bucket.RareTerms
{
	/**
	 * A multi-bucket value source based aggregation which finds "rare" terms — terms that are at the long-tail of the
	 * distribution and are not frequent. Conceptually, this is like a terms aggregation that is sorted by _count ascending.
	 * As noted in the terms aggregation docs, actually ordering a terms agg by count ascending has unbounded error.
	 * Instead, you should use the rare_terms aggregation.
	 *
	 * NOTE: Valid only in Elasticsearch 7.3.0+
	 *
	 * See the Elasticsearch documentation on {ref_current}/search-aggregations-bucket-rare-terms-aggregation.html[rare terms aggregation] for more detail.
	 */
	[SkipVersion("<7.3.0", "Introduced in 7.3.0")]
	public class RareTermsAggregationUsageTests : AggregationUsageTestBase
	{
		public RareTermsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			names = new
			{
				meta = new
				{
					foo = "bar"
				},
				rare_terms = new
				{
					field = "name",
					max_doc_count = 5,
					missing = "n/a",
					precision = 0.001
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.RareTerms("names", st => st
				.Field(p => p.Name)
				.Missing("n/a")
				.MaximumDocumentCount(5)
				.Precision(0.001)
				.Meta(m => m
					.Add("foo", "bar")
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new RareTermsAggregation("names")
			{
				Field = Infer.Field<Project>(p => p.Name),
				MaximumDocumentCount = 5,
				Precision = 0.001,
				Missing = "n/a",
				Meta = new Dictionary<string, object> { { "foo", "bar" } }
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var rareTerms = response.Aggregations.RareTerms("names");
			rareTerms.Should().NotBeNull();
			rareTerms.Buckets.Should().NotBeNull();
			rareTerms.Buckets.Count.Should().BeGreaterThan(0);
			foreach (var item in rareTerms.Buckets)
			{
				item.Key.Should().NotBeNullOrEmpty();
				item.DocCount.Should().BeGreaterOrEqualTo(1);
			}
			rareTerms.Meta.Should().NotBeNull().And.HaveCount(1);
			rareTerms.Meta["foo"].Should().Be("bar");
		}
	}
}
