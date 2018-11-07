namespace Nest
{
	/// <inheritdoc cref="IGeoShapeProperty" />
	public class GeoShapeAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoShapeProperty
	{
		public GeoShapeAttribute() : base(FieldType.GeoShape) { }

		/// <inheritdoc cref="IGeoShapeProperty.DistanceErrorPercentage" />
		public double DistanceErrorPercentage
		{
			get => (Self.Precision != null) | (Self.TreeLevels != null)
				? Self.DistanceErrorPercentage.GetValueOrDefault(0)
				: Self.DistanceErrorPercentage.GetValueOrDefault(0.025);
			set => Self.DistanceErrorPercentage = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.IgnoreMalformed" />
		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault(false);
			set => Self.IgnoreMalformed = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.IgnoreZValue" />
		public bool IgnoreZValue
		{
			get => Self.IgnoreZValue.GetValueOrDefault(true);
			set => Self.IgnoreZValue = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.Orientation" />
		public GeoOrientation Orientation
		{
			get => Self.Orientation.GetValueOrDefault(GeoOrientation.CounterClockWise);
			set => Self.Orientation = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.PointsOnly" />
		public bool PointsOnly
		{
			get => Self.PointsOnly.GetValueOrDefault(false);
			set => Self.PointsOnly = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.Strategy" />
		public GeoStrategy Strategy
		{
			get => Self.Strategy.GetValueOrDefault(GeoStrategy.Recursive);
			set => Self.Strategy = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.Tree" />
		public GeoTree Tree
		{
			get => Self.Tree.GetValueOrDefault(GeoTree.Geohash);
			set => Self.Tree = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.TreeLevels" />
		public int TreeLevels
		{
			get => Self.TreeLevels.GetValueOrDefault(50);
			set => Self.TreeLevels = value;
		}

		double? IGeoShapeProperty.DistanceErrorPercentage { get; set; }
		bool? IGeoShapeProperty.IgnoreMalformed { get; set; }
		bool? IGeoShapeProperty.IgnoreZValue { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		bool? IGeoShapeProperty.PointsOnly { get; set; }
		Distance IGeoShapeProperty.Precision { get; set; }
		private IGeoShapeProperty Self => this;
		GeoStrategy? IGeoShapeProperty.Strategy { get; set; }

		GeoTree? IGeoShapeProperty.Tree { get; set; }
		int? IGeoShapeProperty.TreeLevels { get; set; }
	}
}
