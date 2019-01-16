using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum TranslogDurability
	{
		[EnumMember(Value = "request")]
		Request,

		[EnumMember(Value = "async")]
		Async
	}
}
