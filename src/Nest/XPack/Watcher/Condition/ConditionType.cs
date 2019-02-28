using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum ConditionType
	{
		[EnumMember(Value = "always")]
		Always,

		[EnumMember(Value = "never")]
		Never,

		[EnumMember(Value = "script")]
		Script,

		[EnumMember(Value = "compare")]
		Compare,

		[EnumMember(Value = "array_compare")]
		ArrayCompare
	}
}
