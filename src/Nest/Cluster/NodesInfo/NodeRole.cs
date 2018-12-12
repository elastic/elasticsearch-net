using System.Runtime.Serialization;

namespace Nest
{
	public enum NodeRole
	{
		[EnumMember(Value = "master")]
		Master,

		[EnumMember(Value = "data")]
		Data,

		[EnumMember(Value = "client")]
		Client,

		[EnumMember(Value = "ingest")]
		Ingest
	}
}
