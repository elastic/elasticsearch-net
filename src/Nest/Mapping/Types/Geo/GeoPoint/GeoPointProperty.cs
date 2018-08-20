using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Data type mapping to map a property as a geopoint
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoPointProperty : IDocValuesProperty
	{
		/// <summary>
		/// If true, malformed geo-points are ignored. If false (default), malformed
		/// geo-points throw an exception and reject the whole document.
		/// </summary>
		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set;  }


		/// <summary>
		/// If true (default) three dimension points will be accepted (stored in source) but only
		/// latitude and longitude values will be indexed; the third dimension is ignored. If false, geo-points
		/// containing any more than latitude and longitude (two dimensions) values
		/// throw an exception and reject the whole document.
		/// </summary>
		[JsonProperty("ignore_z_value")]
		bool? IgnoreZValue { get; set;  }

		/// <summary>
		/// Accepts a geo_point value which is substituted for any explicit null values.
		/// Defaults to null, which means the field is treated as missing.
		/// </summary>
		[JsonProperty("null_value")]
		GeoLocation NullValue { get; set; }
	}

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

	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoPointPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<GeoPointPropertyDescriptor<T>, IGeoPointProperty, T>, IGeoPointProperty
		where T : class
	{
		bool? IGeoPointProperty.IgnoreMalformed { get; set; }
		bool? IGeoPointProperty.IgnoreZValue { get; set; }
		GeoLocation IGeoPointProperty.NullValue { get; set; }

		public GeoPointPropertyDescriptor() : base(FieldType.GeoPoint) { }

		/// <inheritdoc cref="IGeoPointProperty.IgnoreMalformed" />
		public GeoPointPropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) => Assign(a => a.IgnoreMalformed = ignoreMalformed);

		/// <inheritdoc cref="IGeoPointProperty.IgnoreZValue" />
		public GeoPointPropertyDescriptor<T> IgnoreZValue(bool? ignoreZValue = true) => Assign(a => a.IgnoreZValue = ignoreZValue);

		/// <inheritdoc cref="IGeoPointProperty.NullValue" />
		public GeoPointPropertyDescriptor<T> NullValue(GeoLocation defaultValue) => Assign(a => a.NullValue = defaultValue);
	}
}
