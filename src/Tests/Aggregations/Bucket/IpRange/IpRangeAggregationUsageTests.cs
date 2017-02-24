using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest_5_2_0.Infer;

namespace Tests.Aggregations.Bucket.IpRange
{
	[SkipVersion("5.0.0-alpha2", "broken in this release. error reason: Expected numeric type on field [leadDeveloper.iPAddress], but got [ip]")]
	public class IpRangeAggregationUsageTests : AggregationUsageTestBase
	{
		public IpRangeAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				ip_ranges = new
				{
					ip_range = new
					{
						field = "leadDeveloper.iPAddress",
						ranges = new object[]
						{
							new { to = "10.0.0.5" },
							new { from = "10.0.0.5" }
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.IpRange("ip_ranges", ip => ip
					.Field(p => p.LeadDeveloper.IPAddress)
					.Ranges(
						r => r.To("10.0.0.5"),
						r => r.From("10.0.0.5")
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new IpRangeAggregation("ip_ranges")
				{
					Field = Field((Project p) => p.LeadDeveloper.IPAddress),
					Ranges = new List<Nest_5_2_0.IpRange>
					{
						new Nest_5_2_0.IpRange { To = "10.0.0.5" },
						new Nest_5_2_0.IpRange { From = "10.0.0.5" }
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var ipRanges = response.Aggs.IpRange("ip_ranges");
			ipRanges.Should().NotBeNull();
			ipRanges.Buckets.Should().NotBeNull();
			ipRanges.Buckets.Count.Should().BeGreaterThan(0);
			foreach (var range in ipRanges.Buckets)
				range.DocCount.Should().BeGreaterThan(0);
		}
	}
}
