using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.GeoBounds
{
	public class GeoBoundsAggregationUsageTests : AggregationUsageTestBase
	{
		public GeoBoundsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			viewport = new
			{
				geo_bounds = new
				{
					field = "location",
					wrap_longitude = true
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.GeoBounds("viewport", gb => gb
				.Field(p => p.Location)
				.WrapLongitude(true)
			);

		protected override AggregationDictionary InitializerAggs =>
			new GeoBoundsAggregation("viewport", Field<Project>(p => p.Location))
			{
				WrapLongitude = true
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var viewport = response.Aggregations.GeoBounds("viewport");
			viewport.Should().NotBeNull();
			viewport.Bounds.Should().NotBeNull();

			var bottomRight = viewport.Bounds.BottomRight;
			bottomRight.Should().NotBeNull();
			bottomRight.Lat.Should().HaveValue();
			GeoLocation.IsValidLatitude(bottomRight.Lat.Value).Should().BeTrue();
			bottomRight.Lon.Should().HaveValue();
			GeoLocation.IsValidLongitude(bottomRight.Lon.Value).Should().BeTrue();

			var topLeft = viewport.Bounds.TopLeft;
			topLeft.Should().NotBeNull();
			topLeft.Lat.Should().HaveValue();
			GeoLocation.IsValidLatitude(topLeft.Lat.Value).Should().BeTrue();
			topLeft.Lon.Should().HaveValue();
			GeoLocation.IsValidLongitude(topLeft.Lon.Value).Should().BeTrue();
		}
	}
}
