// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Clients.Elasticsearch.Infer;

namespace Tests.Aggregations.Metric;

public class BoxplotAggregationUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
{
	public BoxplotAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

	protected override object AggregationJson => new
	{
		boxplot_commits = new
		{
			meta = new
			{
				foo = "bar"
			},
			boxplot = new
			{
				field = "numberOfCommits",
				missing = 10.0,
				compression = 100.0
			}
		}
	};

	protected override Action<AggregationDescriptor<Project>> FluentAggs => a => a
		.Boxplot("boxplot_commits", plot => plot
			.Meta(m => m
				.Add("foo", "bar")
			)
			.Field(p => p.NumberOfCommits)
			.Missing(10)
			.Compression(100)
		);

	protected override AggregationDictionary InitializerAggs =>
		new BoxplotAggregation("boxplot_commits", Field<Project>(p => p.NumberOfCommits))
		{
			Meta = new Dictionary<string, object>
			{
				{ "foo", "bar" }
			},
			Missing = 10,
			Compression = 100
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		var boxplot = response.Aggregations.GetBoxplot("boxplot_commits");
		boxplot.Should().NotBeNull();
		boxplot.Min.Should().BeGreaterOrEqualTo(0);
		boxplot.Max.Should().BeGreaterOrEqualTo(0);
		boxplot.Q1.Should().BeGreaterOrEqualTo(0);
		boxplot.Q2.Should().BeGreaterOrEqualTo(0);
		boxplot.Q3.Should().BeGreaterOrEqualTo(0);
		boxplot.Lower.Should().BeGreaterOrEqualTo(0);
		boxplot.Upper.Should().BeGreaterOrEqualTo(0);
		boxplot.Meta.Should().NotBeNull().And.HaveCount(1);
		boxplot.Meta["foo"].Should().Be("bar");
	}
}
