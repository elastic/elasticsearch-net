using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum ZeroTermsQuery
	{
		[EnumMember(Value = "all")]
		All,

		[EnumMember(Value = "none")]
		None
	}
}
