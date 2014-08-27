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
	public enum DateRounding
	{
		/// <summary>
		/// (the default), rounds to the lowest whole unit of this field.
		/// </summary>
		[EnumMember(Value = "minute")]
		Floor,
		/// <summary>
		/// Rounds to the highest whole unit of this field.
		/// </summary>
		[EnumMember(Value = "ceiling")]
		Ceiling,
		/// <summary>
		///  Round to the nearest whole unit of this field. If the given millisecond value is closer to the floor or is exactly halfway, this function behaves like floor. If the millisecond value is closer to the ceiling, this function behaves like ceiling.
		/// </summary>
		[EnumMember(Value = "half_floor")]
		HalfFloor,
		/// <summary>
		/// Round to the nearest whole unit of this field. If the given millisecond value is closer to the floor, this function behaves like floor. If the millisecond value is closer to the ceiling or is exactly halfway, this function behaves like ceiling.
		/// </summary>
		[EnumMember(Value = "half_ceiling")]
		HalfCeiling,
		/// <summary>
		/// Round to the nearest whole unit of this field. If the given millisecond value is closer to the floor, this function behaves like floor. If the millisecond value is closer to the ceiling, this function behaves like ceiling. If the millisecond value is exactly halfway between the floor and ceiling, the ceiling is chosen over the floor only if it makes this field’s value even.
		/// </summary>
		[EnumMember(Value = "half_even")]
		HalfEven
	}
}
