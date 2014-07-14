using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Define the type of field content.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FieldType
	{
		/// <summary>
		/// Default. Will be defined by the type of property return.
		/// </summary>
		[EnumMember(Value = "none")]
		None,
		/// <summary>
		/// Geo based points.
		/// </summary>
		[EnumMember(Value = "geo_point")]
		GeoPoint,
		/// <summary>
		/// Geo shape type.
		/// </summary>
		[EnumMember(Value = "geo_shape")]
		GeoShape,
		/// <summary>
		/// The attachment type allows to index different “attachment” type field (encoded as base64), for example, microsoft office formats, open document formats, ePub, HTML...
		/// </summary>
		[EnumMember(Value = "attachment")]
		Attachment,
		/// <summary>
		/// An ip mapping type allows to store ipv4 addresses in a numeric form allowing to easily sort, and range query it (using ip values).
		/// </summary>
		[EnumMember(Value = "ip")]
		Ip,
		/// <summary>
		/// The binary type is a base64 representation of binary data that can be stored in the index.
		/// </summary>
		[EnumMember(Value = "binary")]
		Binary,
		/// <summary>
		/// Text based string type.
		/// </summary>
		[EnumMember(Value = "string")]
		String,
		/// <summary>
		/// Integer type.
		/// </summary>
		[EnumMember(Value = "integer")]
		Integer,
		/// <summary>
		/// Long type.
		/// </summary>
		[EnumMember(Value = "long")]
		Long,
		/// <summary>
		/// Float type.
		/// </summary>
		[EnumMember(Value = "float")]
		Float,
		/// <summary>
		/// Double type.
		/// </summary>
		[EnumMember(Value = "double")]
		Double,
		/// <summary>
		/// Date type.
		/// </summary>
		[EnumMember(Value = "date")]
		Date,
		/// <summary>
		/// Boolean type.
		/// </summary>
		[EnumMember(Value = "boolean")]
		Boolean,
		/// <summary>
		/// Completion type.
		/// </summary>
		[EnumMember(Value = "completion")]
		Completion,
		/// <summary>
		/// Nested type.
		/// </summary>
		[EnumMember(Value = "nested")]
		Nested,
		/// <summary>
		/// object type, no need to set this manually if its not a value type this will be set.
		/// Only set this if you need to force a value type to be mapped to an elasticsearch object type.
		/// </summary>
		[EnumMember(Value = "object")]
		Object
	}
}
