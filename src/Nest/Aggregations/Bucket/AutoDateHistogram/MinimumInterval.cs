using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum MinimumInterval
	{
		[EnumMember(Value = "second")]
		Second,

		[EnumMember(Value = "minute")]
		Minute,

		[EnumMember(Value = "hour")]
		Hour,

		[EnumMember(Value = "day")]
		Day,

		[EnumMember(Value = "month")]
		Month,

		[EnumMember(Value = "year")]
		Year
	}
}
