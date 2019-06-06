using System;

namespace Nest
{
	/// <inheritdoc cref="IGeoShapeProperty" />
	public class GeoShapeAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoShapeProperty
	{
		public GeoShapeAttribute() : base(FieldType.GeoShape) { }

		/// <inheritdoc cref="IGeoShapeProperty.DistanceErrorPercentage" />
		[Obsolete("Removed in Elasticsearch 6.6")]
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
		[Obsolete("Removed in Elasticsearch 6.6")]
		public bool PointsOnly
		{
			get => Self.PointsOnly.GetValueOrDefault(false);
			set => Self.PointsOnly = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.Strategy" />
		[Obsolete("Removed in Elasticsearch 6.6")]
		public GeoStrategy Strategy
		{
			get => Self.Strategy.GetValueOrDefault(GeoStrategy.Recursive);
			set => Self.Strategy = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.Tree" />
		[Obsolete("Removed in Elasticsearch 6.6")]
		public GeoTree Tree
		{
			get => Self.Tree.GetValueOrDefault(GeoTree.Geohash);
			set => Self.Tree = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.TreeLevels" />
		[Obsolete("Removed in Elasticsearch 6.6")]
		public int TreeLevels
		{
			get => Self.TreeLevels.GetValueOrDefault(50);
			set => Self.TreeLevels = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.Coerce" />
		public bool Coerce
		{
			get => Self.Coerce.GetValueOrDefault(true);
			set => Self.Coerce = value;
		}

		[Obsolete("Removed in Elasticsearch 6.6")]
		double? IGeoShapeProperty.DistanceErrorPercentage { get; set; }

		bool? IGeoShapeProperty.IgnoreMalformed { get; set; }

		bool? IGeoShapeProperty.IgnoreZValue { get; set; }

		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }

		[Obsolete("Removed in Elasticsearch 6.6")]
		bool? IGeoShapeProperty.PointsOnly { get; set; }

		[Obsolete("Removed in Elasticsearch 6.6")]
		Distance IGeoShapeProperty.Precision { get; set; }

		private IGeoShapeProperty Self => this;

		[Obsolete("Removed in Elasticsearch 6.6")]
		GeoStrategy? IGeoShapeProperty.Strategy { get; set; }

		[Obsolete("Removed in Elasticsearch 6.6")]
		GeoTree? IGeoShapeProperty.Tree { get; set; }

		[Obsolete("Removed in Elasticsearch 6.6")]
		int? IGeoShapeProperty.TreeLevels { get; set; }

		bool? IGeoShapeProperty.Coerce { get; set; }
	}
}
