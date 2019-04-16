using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum Day
	{
		[EnumMember(Value = "sunday")]
		Sunday,

		[EnumMember(Value = "monday")]
		Monday,

		[EnumMember(Value = "tuesday")]
		Tuesday,

		[EnumMember(Value = "wednesday")]
		Wednesday,

		[EnumMember(Value = "thursday")]
		Thursday,

		[EnumMember(Value = "friday")]
		Friday,

		[EnumMember(Value = "saturday")]
		Saturday
	}
}
