namespace Nest
{
	public class GeoShapeAttribute : ElasticsearchPropertyAttributeBase, IGeoShapeProperty
	{
		IGeoShapeProperty Self => this;

		GeoTree? IGeoShapeProperty.Tree { get; set; }
		Distance IGeoShapeProperty.Precision { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		int? IGeoShapeProperty.TreeLevels { get; set; }
		double? IGeoShapeProperty.DistanceErrorPercentage { get; set; }
		bool? IGeoShapeProperty.PointsOnly { get; set; }

		public GeoTree Tree { get { return Self.Tree.GetValueOrDefault(); } set { Self.Tree = value; } }
		public GeoOrientation Orientation { get { return Self.Orientation.GetValueOrDefault(); } set { Self.Orientation = value; } }
		public int TreeLevels { get { return Self.TreeLevels.GetValueOrDefault(); } set { Self.TreeLevels = value; } }
		public double DistanceErrorPercentage { get { return Self.DistanceErrorPercentage.GetValueOrDefault(); } set { Self.DistanceErrorPercentage = value; } }
		public bool PointsOnly { get { return Self.PointsOnly.GetValueOrDefault(); } set { Self.PointsOnly = value; } }

		public GeoShapeAttribute() : base("geo_shape") { }
	}
}
