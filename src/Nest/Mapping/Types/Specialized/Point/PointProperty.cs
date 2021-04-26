/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
