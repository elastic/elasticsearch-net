// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Aggregations;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;
using System.Collections.Generic;

namespace Tests.Aggregations.Bucket;

[SkipVersion("<8.1.0", "IP prefix aggregations were introduced in 8.1.0")]
public class IpPrefixAggregationUsageTests : AggregationUsageWithVerifyTestBase<ReadOnlyCluster>
{
	public IpPrefixAggregationUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override Action<AggregationDescriptor<Project>> FluentAggregations => a => a
		.IpPrefix("ipv4-subnets", i => i
			.Field(f => f.LeadDeveloper.IpAddress)
			.PrefixLength(24)
			.Meta(m => m
				.Add("foo", "bar")
			));

	protected override AggregationDictionary InitializerAggregations =>
		new IpPrefixAggregation("ipv4-subnets")
		{
			Field = Infer.Field((Project p) => p.LeadDeveloper.IpAddress),
			PrefixLength = 24,
			Meta = new Dictionary<string, object>
			{
				{ "foo", "bar" }
			}
		};

	protected override void ExpectResponse(SearchResponse<Project> response)
	{
		response.ShouldBeValid();
		response.Aggregations.Count.Should().Be(1);

		var ipPrefix = response.Aggregations.GetIpPrefix("ipv4-subnets");

		ipPrefix.Should().NotBeNull();
		ipPrefix.Buckets.Should().NotBeNull();
		ipPrefix.Buckets.Count.Should().BeGreaterThan(0);
		foreach (var item in ipPrefix.Buckets)
		{
			item.Key.Should().NotBeNullOrEmpty();
			item.DocCount.Should().BeGreaterOrEqualTo(1);
			item.PrefixLength.Should().Be(24);
			item.Netmask.Should().Be("255.255.255.0");
			item.IsIpv6.Should().BeFalse();
		}
		ipPrefix.Meta.Should().NotBeNull().And.HaveCount(1);
		ipPrefix.Meta["foo"].Should().Be("bar");
	}
}
