using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

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
