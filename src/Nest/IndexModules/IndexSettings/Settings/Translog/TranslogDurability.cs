using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TranslogDurability
	{
		[EnumMember(Value = "request")]
		Request,
		[EnumMember(Value = "async")]
		Async
	}
}