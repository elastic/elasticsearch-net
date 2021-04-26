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
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Data type mapping to map a property as a geopoint
	/// </summary>
	[InterfaceDataContract]
	public interface IGeoPointProperty : IDocValuesProperty
	{
		/// <summary>
		/// If true, malformed geo-points are ignored. If false (default), malformed
		/// geo-points throw an exception and reject the whole document.
		/// </summary>
		[DataMember(Name ="ignore_malformed")]
		bool? IgnoreMalformed { get; set; }


		/// <summary>
		/// If true (default) three dimension points will be accepted (stored in source) but only
		/// latitude and longitude values will be indexed; the third dimension is ignored. If false, geo-points
		/// containing any more than latitude and longitude (two dimensions) values
		/// throw an exception and reject the whole document.
		/// </summary>
		[DataMember(Name ="ignore_z_value")]
		bool? IgnoreZValue { get; set; }

		/// <summary>
		/// Accepts a geo_point value which is substituted for any explicit null values.
		/// Defaults to null, which means the field is treated as missing.
		/// </summary>
		[DataMember(Name ="null_value")]
		GeoLocation NullValue { get; set; }
	}

	/// <inheritdoc cref="IGeoPointProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoPointProperty : DocValuesPropertyBase, IGeoPointProperty
	{
		public GeoPointProperty() : base(FieldType.GeoPoint) { }

		/// <inheritdoc />
		public bool? IgnoreMalformed { get; set; }

		/// <inheritdoc />
		public bool? IgnoreZValue { get; set; }

		/// <inheritdoc />
		public GeoLocation NullValue { get; set; }
	}

	/// <inheritdoc cref="IGeoPointProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoPointPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<GeoPointPropertyDescriptor<T>, IGeoPointProperty, T>, IGeoPointProperty
		where T : class
	{
		public GeoPointPropertyDescriptor() : base(FieldType.GeoPoint) { }

		bool? IGeoPointProperty.IgnoreMalformed { get; set; }
		bool? IGeoPointProperty.IgnoreZValue { get; set; }
		GeoLocation IGeoPointProperty.NullValue { get; set; }

		/// <inheritdoc cref="IGeoPointProperty.IgnoreMalformed" />
		public GeoPointPropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) => Assign(ignoreMalformed, (a, v) => a.IgnoreMalformed = v);

		/// <inheritdoc cref="IGeoPointProperty.IgnoreZValue" />
		public GeoPointPropertyDescriptor<T> IgnoreZValue(bool? ignoreZValue = true) => Assign(ignoreZValue, (a, v) => a.IgnoreZValue = v);

		/// <inheritdoc cref="IGeoPointProperty.NullValue" />
		public GeoPointPropertyDescriptor<T> NullValue(GeoLocation defaultValue) => Assign(defaultValue, (a, v) => a.NullValue = v);
	}
}
