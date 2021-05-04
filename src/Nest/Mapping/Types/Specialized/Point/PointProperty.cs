// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The point datatype facilitates the indexing of and searching
	/// arbitrary `x, y` pairs that fall in a 2-dimensional planar
	/// coordinate system.
	/// <para />
	/// You can query documents using this type using <see cref="IShapeQuery"/>.
	/// <para />
	/// Available in Elasticsearch 7.8.0+ with at least basic license level
	/// </summary>
	[InterfaceDataContract]
	public interface IPointProperty : IProperty
	{
		/// <summary>
		/// If <c>true</c>, malformed geojson shapes are ignored. If false (default),
		/// malformed geojson shapes throw an exception and reject the whole document.
		/// </summary>
		[DataMember(Name ="ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		/// <summary>
		/// If true (default) three dimension points will be accepted (stored in source) but
		/// only x and y values will be indexed; the third dimension is ignored. If false,
		/// geo-points containing any more than x and y (two dimensions) values throw
		/// an exception and reject the whole document.
		/// </summary>
		[DataMember(Name ="ignore_z_value")]
		bool? IgnoreZValue { get; set; }

		/// <summary>
		/// Accepts an point value which is substituted for any explicit `null` values.
		/// Defaults to `null`, which means the field is treated as missing.
		/// </summary>
		[DataMember(Name = "null_value")]
		CartesianPoint NullValue { get; set; }
	}

	/// <inheritdoc cref="IPointProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class PointProperty : PropertyBase, IPointProperty
	{
		public PointProperty() : base(FieldType.Point) { }

		/// <inheritdoc />
		public bool? IgnoreMalformed { get; set; }

		/// <inheritdoc />
		public bool? IgnoreZValue { get; set; }

		/// <inheritdoc />
		public CartesianPoint NullValue { get; set; }
	}

	/// <inheritdoc cref="IPointProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class PointPropertyDescriptor<T>
		: PropertyDescriptorBase<PointPropertyDescriptor<T>, IPointProperty, T>, IPointProperty
		where T : class
	{
		public PointPropertyDescriptor() : base(FieldType.Point) { }

		bool? IPointProperty.IgnoreMalformed { get; set; }
		bool? IPointProperty.IgnoreZValue { get; set; }
		CartesianPoint IPointProperty.NullValue { get; set; }

		/// <inheritdoc cref="IPointProperty.IgnoreMalformed" />
		public PointPropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) =>
			Assign(ignoreMalformed, (a, v) => a.IgnoreMalformed = v);

		/// <inheritdoc cref="IPointProperty.IgnoreZValue" />
		public PointPropertyDescriptor<T> IgnoreZValue(bool? ignoreZValue = true) =>
			Assign(ignoreZValue, (a, v) => a.IgnoreZValue = v);

		/// <inheritdoc cref="IPointProperty.NullValue" />
		public PointPropertyDescriptor<T> NullValue(CartesianPoint nullValue) =>
			Assign(nullValue, (a, v) => a.NullValue = v);
	}
}
