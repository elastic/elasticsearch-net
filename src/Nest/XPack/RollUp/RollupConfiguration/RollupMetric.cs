using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RollupMetric
	{
		[EnumMember(Value = "min")] Min,
		[EnumMember(Value = "max")] Max,
		[EnumMember(Value = "sum")] Sum,
		[EnumMember(Value = "avg")] Average,
		[EnumMember(Value = "value_count")] ValueCount
	}
}
