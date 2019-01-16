using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum HttpInputMethod
	{
		[EnumMember(Value = "head")]
		Head,

		[EnumMember(Value = "get")]
		Get,

		[EnumMember(Value = "post")]
		Post,

		[EnumMember(Value = "put")]
		Put,

		[EnumMember(Value = "delete")]
		Delete
	}
}
