using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum NodeRole
	{
		[EnumMember(Value = "master")]
		Master,

		[EnumMember(Value = "data")]
		Data,

		[EnumMember(Value = "client")]
		Client,

		[EnumMember(Value = "ingest")]
		Ingest
	}
}
