using System.Runtime.Serialization;

namespace Nest
{
	public enum FollowerIndexStatus
	{
		[EnumMember(Value = "active")]
		Active,

		[EnumMember(Value = "paused")]
		Paused
	}
}
