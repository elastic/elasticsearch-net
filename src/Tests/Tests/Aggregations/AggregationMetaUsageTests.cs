using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Aggregations
{
	/**
	*=== Aggregation Metadata
	* Metadata can be provided per aggregation, and will be returned in the aggregation response
	*/
	public class AggregationMetaUsageTests : AggregationUsageTestBase
	{
		public AggregationMetaUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			min_last_activity = new
			{
				min = new
				{
					field = "lastActivity"
				},
				meta = new
				{
					meta_1 = "value_1",
					meta_2 = 2,
					meta_3 = new
					{
						meta_3 = "value_3"
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Min("min_last_activity", m => m
				.Field(p => p.LastActivity)
				.Meta(d => d
					.Add("meta_1", "value_1")
					.Add("meta_2", 2)
					.Add("meta_3", new { meta_3 = "value_3" })
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new MinAggregation("min_last_activity", Infer.Field<Project>(p => p.LastActivity))
			{
				Meta = new Dictionary<string,object>
				{
					{ "meta_1", "value_1" },
					{ "meta_2", 2 },
					{ "meta_3", new { meta_3 = "value_3" } }
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var min = response.Aggregations.Min("min_last_activity");
			min.Meta.Should().NotBeNull().And.ContainKeys("meta_1", "meta_2", "meta_3");
		}
	}
}
