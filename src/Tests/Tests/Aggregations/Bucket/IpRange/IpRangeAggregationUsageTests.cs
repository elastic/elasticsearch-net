using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
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
			ipRanges.Buckets.Count.Should().BeGreaterThan(0);
			foreach (var range in ipRanges.Buckets)
			{
				if (TestConfiguration.Instance.InRange("6.4.0"))
					range.Key.Should().NotBeNullOrEmpty();

				range.DocCount.Should().BeGreaterThan(0);
			}
		}
	}
}
