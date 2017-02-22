using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest_5_2_0
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DynamicMapping
	{
		/// <summary>
		/// If new unmapped fields are passed, the whole document WON'T be added/updated
		/// </summary>
		[EnumMember(Value = "strict")]
		Strict
	}
}
