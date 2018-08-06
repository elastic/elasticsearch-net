using System;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Bucket.Missing
{
	public class MissingAggregationUsageTests : AggregationUsageTestBase
	{
		public MissingAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			projects_without_a_description = new
			{
				missing = new
				{
					field = "description.keyword"
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Missing("projects_without_a_description", m => m
				.Field(p => p.Description.Suffix("keyword"))
			);

		protected override AggregationDictionary InitializerAggs =>
			new MissingAggregation("projects_without_a_description")
			{
				Field = Field<Project>(p => p.Description.Suffix("keyword"))
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var projectsWithoutDesc = response.Aggregations.Missing("projects_without_a_description");
			projectsWithoutDesc.Should().NotBeNull();
		}
	}
}
