#pragma warning disable 612, 618
using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types.Core.GeoShape
{
	public class GeoShapePropertyTests : PropertyTestsBase
	{
		public GeoShapePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				location = new
				{
					type = "geo_shape",
					tree = "quadtree",
					orientation = "cw",
					strategy = "recursive",
					tree_levels = 3,
					points_only = true,
					distance_error_pct = 1.0,
					coerce = true
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.GeoShape(s => s
				.Name(p => p.Location)
				.Tree(GeoTree.Quadtree)
				.Orientation(GeoOrientation.ClockWise)
				.Strategy(GeoStrategy.Recursive)
				.TreeLevels(3)
				.PointsOnly()
				.DistanceErrorPercentage(1.0)
				.Coerce()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"location", new GeoShapeProperty
				{
					Tree = GeoTree.Quadtree,
					Orientation = GeoOrientation.ClockWise,
					Strategy = GeoStrategy.Recursive,
					TreeLevels = 3,
					PointsOnly = true,
					DistanceErrorPercentage = 1.0,
					Coerce = true
				}
			}
		};
	}
}
#pragma warning restore 612, 618
