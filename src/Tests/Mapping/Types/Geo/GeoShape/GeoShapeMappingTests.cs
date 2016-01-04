using System;
using Nest;

namespace Tests.Mapping.Types.Geo.GeoShape
{
	public class GeoShapeTest
	{
		[GeoShape(
			Tree = GeoTree.Quadtree,
			Orientation = GeoOrientation.ClockWise,
			TreeLevels = 3,
			PointsOnly = true,
			DistanceErrorPercentage = 1.0)]
		public object Full { get; set; }

		[GeoShape]
		public object Minimal { get; set; }
	}

	public class GeoShapeMappingTests : TypeMappingTestBase<GeoShapeTest>
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

		protected override Func<PropertiesDescriptor<GeoShapeTest>, IPromise<IProperties>> FluentProperties => p => p
			.GeoShape(s => s
				.Name(o => o.Full)
				.Tree(GeoTree.Quadtree)
				.Orientation(GeoOrientation.ClockWise)
				.TreeLevels(3)
				.PointsOnly()
				.DistanceErrorPercentage(1)
			)
			.GeoShape(b => b
				.Name(o => o.Minimal)
			);
	}
}
