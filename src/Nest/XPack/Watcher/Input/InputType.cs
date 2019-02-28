using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum InputType
	{
		[EnumMember(Value = "http")]
		Http,

		[EnumMember(Value = "search")]
		Search,

		[EnumMember(Value = "simple")]
		Simple
	}
}
