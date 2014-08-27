
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
	public enum DateInterval
	{
		[EnumMember(Value = "second")]
		Second, 
		[EnumMember(Value = "minute")]
		Minute, 
		[EnumMember(Value = "hour")]
		Hour, 
		[EnumMember(Value = "day")]
		Day,
		[EnumMember(Value = "week")]
		Week, 
		[EnumMember(Value = "month")]
		Month, 
		[EnumMember(Value = "quarter")]
		Quarter, 
		[EnumMember(Value = "year")]
		Year
	}
}
