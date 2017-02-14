namespace Nest
{
	public class GeoShapeAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoShapeProperty
	{
		private IGeoShapeProperty Self => this;

		public GeoShapeAttribute() : base(FieldType.GeoShape) { }

		GeoTree? IGeoShapeProperty.Tree { get; set; }
		Distance IGeoShapeProperty.Precision { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		int? IGeoShapeProperty.TreeLevels { get; set; }
		GeoStrategy? IGeoShapeProperty.Strategy { get; set; }
		double? IGeoShapeProperty.DistanceErrorPercentage { get; set; }
		bool? IGeoShapeProperty.PointsOnly { get; set; }

		public GeoTree Tree { get { return Self.Tree.GetValueOrDefault(GeoTree.Geohash); } set { Self.Tree = value; } }
		public GeoOrientation Orientation { get { return Self.Orientation.GetValueOrDefault(GeoOrientation.CounterClockWise); } set { Self.Orientation = value; } }
		public int TreeLevels { get { return Self.TreeLevels.GetValueOrDefault(50); } set { Self.TreeLevels = value; } }
		public GeoStrategy Strategy {  get { return Self.Strategy.GetValueOrDefault(GeoStrategy.Recursive); } set { Self.Strategy = value; } }
		public double DistanceErrorPercentage { get { return Self.DistanceErrorPercentage.GetValueOrDefault(); } set { Self.DistanceErrorPercentage = value; } }
		public bool PointsOnly { get { return Self.PointsOnly.GetValueOrDefault(); } set { Self.PointsOnly = value; } }

	}
}
