using System.Runtime.Serialization;


namespace Nest
{

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
