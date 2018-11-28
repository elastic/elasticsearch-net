using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

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
