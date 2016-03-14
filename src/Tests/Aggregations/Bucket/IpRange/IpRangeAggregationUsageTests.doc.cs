using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.IpRange
{
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
					Ranges = new List<Nest.IpRange>
					{
						new Nest.IpRange { To = "10.0.0.5" },
						new Nest.IpRange { From = "10.0.0.5" }
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var ipRanges = response.Aggs.IpRange("ip_ranges");
			ipRanges.Should().NotBeNull();
			ipRanges.Buckets.Should().NotBeNull();
			ipRanges.Buckets.Count.Should().BeGreaterThan(0);
			foreach (var range in ipRanges.Buckets)
				range.DocCount.Should().BeGreaterThan(0);
		}
	}
}