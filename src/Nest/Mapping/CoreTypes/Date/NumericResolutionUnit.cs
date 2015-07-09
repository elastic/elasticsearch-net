using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum NumericResolutionUnit
	{
		[EnumMember(Value = "milliseconds")]
		Milliseconds,
		[EnumMember(Value = "seconds")]
		Seconds
	}
}
