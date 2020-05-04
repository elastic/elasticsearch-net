// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Maps a property as a geo_shape field
	/// </summary>
	[InterfaceDataContract]
	public interface IGeoShapeProperty : IDocValuesProperty
	{
		/// <summary>
		/// If <c>true</c>, malformed geojson shapes are ignored. If false (default),
		/// malformed geojson shapes throw an exception and reject the whole document.
		/// </summary>
		/// <remarks>
		/// Valid for Elasticsearch 6.1.0+
		/// </remarks>
		[DataMember(Name ="ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		/// <summary>
		/// If true (default) three dimension points will be accepted (stored in source) but
		/// only latitude and longitude values will be indexed; the third dimension is ignored. If false,
		/// geo-points containing any more than latitude and longitude (two dimensions) values throw
		/// an exception and reject the whole document.
		/// </summary>
		/// <remarks>
		/// Valid for Elasticsearch 6.3.0+
		/// </remarks>
		[DataMember(Name ="ignore_z_value")]
		bool? IgnoreZValue { get; set; }

		/// <summary>
		/// Defines how to interpret vertex order for polygons and multipolygons.
		/// Defaults to <see cref="GeoOrientation.CounterClockWise" />
		/// </summary>
		[DataMember(Name ="orientation")]
		GeoOrientation? Orientation { get; set; }

		/// <summary>
		/// Defines the approach for how to represent shapes at indexing and search time.
		/// It also influences the capabilities available so it is recommended to let
		/// Elasticsearch set this parameter automatically.
		/// </summary>
		[DataMember(Name ="strategy")]
		GeoStrategy? Strategy { get; set; }

		/// <summary>
		/// Should the data be coerced into becoming a valid geo shape (for instance closing a polygon)
		/// </summary>
		[DataMember(Name ="coerce")]
		bool? Coerce { get; set; }
	}

	/// <inheritdoc cref="IGeoShapeProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoShapeProperty : DocValuesPropertyBase, IGeoShapeProperty
	{
		public GeoShapeProperty() : base(FieldType.GeoShape) { }

		/// <inheritdoc />
		public bool? IgnoreMalformed { get; set; }

		/// <inheritdoc />
		public bool? IgnoreZValue { get; set; }

		/// <inheritdoc />
		public GeoOrientation? Orientation { get; set; }

		/// <inheritdoc />
		public GeoStrategy? Strategy { get; set; }

		/// <inheritdoc />
		public bool? Coerce { get; set; }
	}

	/// <inheritdoc cref="IGeoShapeProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoShapePropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<GeoShapePropertyDescriptor<T>, IGeoShapeProperty, T>, IGeoShapeProperty
		where T : class
	{
		public GeoShapePropertyDescriptor() : base(FieldType.GeoShape) { }


		bool? IGeoShapeProperty.IgnoreMalformed { get; set; }
		bool? IGeoShapeProperty.IgnoreZValue { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		GeoStrategy? IGeoShapeProperty.Strategy { get; set; }
		bool? IGeoShapeProperty.Coerce { get; set; }

		/// <inheritdoc cref="IGeoShapeProperty.Strategy" />
		public GeoShapePropertyDescriptor<T> Strategy(GeoStrategy? strategy) => Assign(strategy, (a, v) => a.Strategy = v);

		/// <inheritdoc cref="IGeoShapeProperty.Orientation" />
		public GeoShapePropertyDescriptor<T> Orientation(GeoOrientation? orientation) => Assign(orientation, (a, v) => a.Orientation = v);

		/// <inheritdoc cref="IGeoShapeProperty.IgnoreMalformed" />
		public GeoShapePropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) =>
			Assign(ignoreMalformed, (a, v) => a.IgnoreMalformed = v);

		/// <inheritdoc cref="IGeoShapeProperty.IgnoreZValue" />
		public GeoShapePropertyDescriptor<T> IgnoreZValue(bool? ignoreZValue = true) =>
			Assign(ignoreZValue, (a, v) => a.IgnoreZValue = v);

		/// <inheritdoc cref="IGeoShapeProperty.Coerce" />
		public GeoShapePropertyDescriptor<T> Coerce(bool? coerce = true) =>
			Assign(coerce, (a, v) => a.Coerce = v);
	}
}
