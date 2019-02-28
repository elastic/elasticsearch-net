using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum ChildScoreMode
	{
		[EnumMember(Value = "none")]
		None,

		[EnumMember(Value = "avg")]
		Average,

		[EnumMember(Value = "sum")]
		Sum,

		[EnumMember(Value = "max")]
		Max,

		[EnumMember(Value = "min")]
		Min
	}
}
