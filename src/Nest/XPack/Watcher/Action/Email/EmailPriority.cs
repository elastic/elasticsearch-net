using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum EmailPriority
	{
		[EnumMember(Value = "lowest")]
		Lowest,

		[EnumMember(Value = "low")]
		Low,

		[EnumMember(Value = "normal")]
		Normal,

		[EnumMember(Value = "high")]
		High,

		[EnumMember(Value = "highest")]
		Highest
	}
}
