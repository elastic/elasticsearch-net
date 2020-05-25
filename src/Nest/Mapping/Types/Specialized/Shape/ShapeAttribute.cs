// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	/// <inheritdoc cref="IShapeProperty" />
	public class ShapeAttribute : ElasticsearchDocValuesPropertyAttributeBase, IShapeProperty
	{
		public ShapeAttribute() : base(FieldType.Shape) { }

		bool? IShapeProperty.IgnoreMalformed { get; set; }
		bool? IShapeProperty.IgnoreZValue { get; set; }
		ShapeOrientation? IShapeProperty.Orientation { get; set; }
		private IShapeProperty Self => this;
		bool? IShapeProperty.Coerce { get; set; }

		/// <inheritdoc cref="IShapeProperty.IgnoreMalformed" />
		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault(false);
			set => Self.IgnoreMalformed = value;
		}

		/// <inheritdoc cref="IShapeProperty.IgnoreZValue" />
		public bool IgnoreZValue
		{
			get => Self.IgnoreZValue.GetValueOrDefault(true);
			set => Self.IgnoreZValue = value;
		}

		/// <inheritdoc cref="IShapeProperty.Orientation" />
		public ShapeOrientation Orientation
		{
			get => Self.Orientation.GetValueOrDefault(ShapeOrientation.CounterClockWise);
			set => Self.Orientation = value;
		}

		/// <inheritdoc cref="IShapeProperty.Coerce" />
		public bool Coerce
		{
			get => Self.Coerce.GetValueOrDefault(true);
			set => Self.Coerce = value;
		}
	}
}
