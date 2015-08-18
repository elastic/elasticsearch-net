using System;

namespace Nest
{
	public class GeoShapeAttribute : ElasticsearchPropertyAttribute 
	{
		public GeoTree Tree { get; set; }
		public GeoOrientation Orientation { get; set; }
		public int TreeLevels { get; set; }
		public double DistanceErrorPercentage { get; set; }

		public override IElasticsearchProperty ToProperty() => new GeoShapeProperty(this);
	}
}
