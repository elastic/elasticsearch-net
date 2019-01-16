using System.Runtime.Serialization;


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
