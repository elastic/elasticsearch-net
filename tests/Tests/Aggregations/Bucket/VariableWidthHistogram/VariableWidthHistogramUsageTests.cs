// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.VariableWidthHistogram
{
	/**
	 * A multi-bucket aggregation similar to Histogram. However, the width of each bucket is not specified. Rather, a target number of buckets is provided
	 * and bucket intervals are dynamically determined based on the document distribution. 
	 *
	 * See the Elasticsearch documentation on {ref_current}/search-aggregations-bucket-variablewidthhistogram-aggregation.html[multi terms aggregation] for more detail.
	 */
	[SkipVersion("<7.11.0", "Variable width aggregation added in 7.11.0")]
	public class VariableWidthHistogramUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public VariableWidthHistogramUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			commits = new
			{
				meta = new
				{
					foo = "bar"
				},
				variable_width_histogram = new
				{
					field = "numberOfCommits",
					buckets = 2,
					initial_buffer = 2,
					shard_size = 100
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.VariableWidthHistogram("commits", v => v
				.Field(f => f.NumberOfCommits)
				.Buckets(2)
				.InitialBuffer(2)
				.ShardSize(100)
				.Meta(m => m
					.Add("foo", "bar")
				));

		protected override AggregationDictionary InitializerAggs =>
			new VariableWidthHistogramAggregation("commits")
			{
				Field = Field<Project>(f => f.NumberOfCommits),
				Buckets = 2,
				InitialBuffer = 2,
				ShardSize = 100,
				Meta = new Dictionary<string, object>
				{
					{ "foo", "bar" }
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var counts = response.Aggregations.VariableWidthHistogram("commits");
			counts.Should().NotBeNull();
			counts.Buckets.Should().HaveCountGreaterThan(0);
			var firstBucket = counts.Buckets.First();
			firstBucket.Key.Should().BeGreaterOrEqualTo(0);
			firstBucket.Minimum.Should().BeGreaterOrEqualTo(0);
			firstBucket.Maximum.Should().BeGreaterOrEqualTo(0);
			firstBucket.DocCount.Should().BeGreaterOrEqualTo(1);
			counts.Meta.Should().NotBeNull().And.HaveCount(1);
			counts.Meta["foo"].Should().Be("bar");
		}
	}

	// hide
	[SkipVersion("<7.11.0", "Variable width aggregation added in 7.11.0")]
	public class VariableWidthHistogramWithScriptUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public VariableWidthHistogramWithScriptUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private const string Script = "Math.min(_value, -1)"; // use inline script to force the number of commits to be -1 for all documents

		protected override object AggregationJson => new
		{
			commits = new
			{
				meta = new
				{
					foo = "bar"
				},
				variable_width_histogram = new
				{
					field = "numberOfCommits",
					buckets = 2,
					initial_buffer = 2,
					shard_size = 100,
					script = Script
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.VariableWidthHistogram("commits", v => v
				.Field(f => f.NumberOfCommits)
				.Buckets(2)
				.InitialBuffer(2)
				.ShardSize(100)
				.Script(Script)
				.Meta(m => m
					.Add("foo", "bar")
				));

		protected override AggregationDictionary InitializerAggs =>
			new VariableWidthHistogramAggregation("commits")
			{
				Field = Field<Project>(f => f.NumberOfCommits),
				Buckets = 2,
				InitialBuffer = 2,
				ShardSize = 100,
				Script = Script,
				Meta = new Dictionary<string, object>
				{
					{ "foo", "bar" }
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var counts = response.Aggregations.VariableWidthHistogram("commits");
			counts.Should().NotBeNull();
			counts.Buckets.Should().HaveCount(1);
			var firstBucket = counts.Buckets.First();
			firstBucket.Key.Should().Be(-1);
			firstBucket.Minimum.Should().Be(-1);
			firstBucket.Maximum.Should().Be(-1);
			firstBucket.DocCount.Should().BeGreaterOrEqualTo(1);
			counts.Meta.Should().NotBeNull().And.HaveCount(1);
			counts.Meta["foo"].Should().Be("bar");
		}
	}
}
