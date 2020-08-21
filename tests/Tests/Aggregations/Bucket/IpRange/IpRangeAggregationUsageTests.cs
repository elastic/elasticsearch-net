// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.IpRange
{
	public class IpRangeAggregationUsageTests : AggregationUsageTestBase
	{
		public IpRangeAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			ip_ranges = new
			{
				ip_range = new
				{
					field = "leadDeveloper.ipAddress",
					ranges = new object[]
					{
						new { to = "127.0.0.1" },
						new { from = "127.0.0.1" }
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.IpRange("ip_ranges", ip => ip
				.Field(p => p.LeadDeveloper.IpAddress)
				.Ranges(
					r => r.To("127.0.0.1"),
					r => r.From("127.0.0.1")
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new IpRangeAggregation("ip_ranges")
			{
				Field = Field((Project p) => p.LeadDeveloper.IpAddress),
				Ranges = new List<IpRangeAggregationRange>
				{
					new IpRangeAggregationRange { To = "127.0.0.1" },
					new IpRangeAggregationRange { From = "127.0.0.1" }
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var ipRanges = response.Aggregations.IpRange("ip_ranges");
			ipRanges.Should().NotBeNull();
			ipRanges.Buckets.Should().NotBeNull();
			ipRanges.Buckets.Count.Should().Be(2);
			ipRanges.Buckets.First().To.Should().Be("127.0.0.1");
			ipRanges.Buckets.Last().From.Should().Be("127.0.0.1");
			foreach (var range in ipRanges.Buckets)
			{
				range.Key.Should().NotBeNullOrEmpty();
				range.DocCount.Should().BeGreaterThan(0);
			}
		}
	}
}
