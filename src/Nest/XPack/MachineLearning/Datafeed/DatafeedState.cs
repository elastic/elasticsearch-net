using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum DatafeedState
	{
		[EnumMember(Value = "started")]
		Started,

		[EnumMember(Value = "stopped")]
		Stopped,

		[EnumMember(Value = "starting")]
		Starting,

		[EnumMember(Value = "stopping")]
		Stopping
	}
}
