using System;
using Nest;

namespace Tests.Mapping.Types.Geo.GeoShape
{
	public class GeoShapeTest
	{
		[GeoShape(
			Tree = GeoTree.Quadtree,
			Orientation = GeoOrientation.ClockWise,
			Strategy = GeoStrategy.Recursive,
			TreeLevels = 3,
			PointsOnly = true,
			DistanceErrorPercentage = 1.0)]
		public object Full { get; set; }

		[GeoShape]
		public object Minimal { get; set; }
	}

	public class GeoShapeAttributeTests : AttributeTestsBase<GeoShapeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "geo_shape",
					tree = "quadtree",
					orientation = "cw",
					strategy = "recursive",
					tree_levels = 3,
					points_only = true,
					distance_error_pct = 1.0
				},
				minimal = new
				{
					type = "geo_shape"
				}
			}
		};
	}
}
