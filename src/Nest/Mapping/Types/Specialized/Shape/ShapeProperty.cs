// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Maps a property as a shape field
	/// </summary>
	[InterfaceDataContract]
	public interface IShapeProperty : IDocValuesProperty
	{
		/// <summary>
		/// If <c>true</c>, malformed geojson shapes are ignored. If false (default),
		/// malformed geojson shapes throw an exception and reject the whole document.
		/// </summary>
		[DataMember(Name ="ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		/// <summary>
		/// If true (default) three dimension points will be accepted (stored in source) but
		/// only latitude and longitude values will be indexed; the third dimension is ignored. If false,
		/// geo-points containing any more than latitude and longitude (two dimensions) values throw
		/// an exception and reject the whole document.
		/// </summary>
		[DataMember(Name ="ignore_z_value")]
		bool? IgnoreZValue { get; set; }

		/// <summary>
		/// Defines how to interpret vertex order for polygons and multipolygons.
		/// Defaults to <see cref="ShapeOrientation.CounterClockWise" />
		/// </summary>
		[DataMember(Name ="orientation")]
		ShapeOrientation? Orientation { get; set; }

		/// <summary>
		/// Should the data be coerced into becoming a valid geo shape (for instance closing a polygon)
		/// </summary>
		[DataMember(Name ="coerce")]
		bool? Coerce { get; set; }
	}

	/// <inheritdoc cref="IShapeProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class ShapeProperty : DocValuesPropertyBase, IShapeProperty
	{
		public ShapeProperty() : base(FieldType.Shape) { }

		/// <inheritdoc />
		public bool? IgnoreMalformed { get; set; }

		/// <inheritdoc />
		public bool? IgnoreZValue { get; set; }

		/// <inheritdoc />
		public ShapeOrientation? Orientation { get; set; }

		/// <inheritdoc />
		public bool? Coerce { get; set; }
	}

	/// <inheritdoc cref="IShapeProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class ShapePropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<ShapePropertyDescriptor<T>, IShapeProperty, T>, IShapeProperty
		where T : class
	{
		public ShapePropertyDescriptor() : base(FieldType.Shape) { }

		bool? IShapeProperty.IgnoreMalformed { get; set; }
		bool? IShapeProperty.IgnoreZValue { get; set; }
		ShapeOrientation? IShapeProperty.Orientation { get; set; }
		bool? IShapeProperty.Coerce { get; set; }

		/// <inheritdoc cref="IShapeProperty.Orientation" />
		public ShapePropertyDescriptor<T> Orientation(ShapeOrientation? orientation) =>
			Assign(orientation, (a, v) => a.Orientation = v);

		/// <inheritdoc cref="IShapeProperty.IgnoreMalformed" />
		public ShapePropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) =>
			Assign(ignoreMalformed, (a, v) => a.IgnoreMalformed = v);

		/// <inheritdoc cref="IShapeProperty.IgnoreZValue" />
		public ShapePropertyDescriptor<T> IgnoreZValue(bool? ignoreZValue = true) =>
			Assign(ignoreZValue, (a, v) => a.IgnoreZValue = v);

		/// <inheritdoc cref="IShapeProperty.Coerce" />
		public ShapePropertyDescriptor<T> Coerce(bool? coerce = true) =>
			Assign(coerce, (a, v) => a.Coerce = v);
	}
}
