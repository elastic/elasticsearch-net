using System.Runtime.Serialization;


namespace Nest
{

	public enum TranslogDurability
	{
		[EnumMember(Value = "request")]
		Request,

		[EnumMember(Value = "async")]
		Async
	}
}
