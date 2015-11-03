using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoOptimizeBBox
	{
		[EnumMember(Value = "memory")]
		Memory,
		[EnumMember(Value = "indexed")]
		Indexed,
		[EnumMember(Value = "none")]
		None
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoValidationMethod
	{
		[EnumMember(Value = "coerce")]
		Coerce,
		[EnumMember(Value = "ignore_malformed")]
		IgnoreMalformed,
		[EnumMember(Value = "strict")]
		Strict
	}
}
