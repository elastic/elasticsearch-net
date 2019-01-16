using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum AccessTokenGrantType
	{
		[EnumMember(Value = "password")] Password
	}
}
