using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest
{
	//TODO Remove this
	[JsonConverter(typeof(StringEnumConverter))]
	[Obsolete("Use GeoPrecisionUnit")]
	public enum GeoUnit
	{
		[EnumMember(Value = "km")]
		Kilometers,
		[EnumMember(Value = "mi")]
		Miles
	}
}
