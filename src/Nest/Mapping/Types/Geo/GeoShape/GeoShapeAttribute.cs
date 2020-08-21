// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <inheritdoc cref="IGeoShapeProperty" />
	public class GeoShapeAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoShapeProperty
	{
		public GeoShapeAttribute() : base(FieldType.GeoShape) { }
		
		bool? IGeoShapeProperty.IgnoreMalformed { get; set; }
		bool? IGeoShapeProperty.IgnoreZValue { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		private IGeoShapeProperty Self => this;
		GeoStrategy? IGeoShapeProperty.Strategy { get; set; }
		bool? IGeoShapeProperty.Coerce { get; set; }

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

		/// <inheritdoc cref="IGeoShapeProperty.Strategy" />
		public GeoStrategy Strategy
		{
			get => Self.Strategy.GetValueOrDefault(GeoStrategy.Recursive);
			set => Self.Strategy = value;
		}

		/// <inheritdoc cref="IGeoShapeProperty.Coerce" />
		public bool Coerce
		{
			get => Self.Coerce.GetValueOrDefault(true);
			set => Self.Coerce = value;
		}

	}
}
