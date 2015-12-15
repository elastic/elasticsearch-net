using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ZeroTermsQuery
	{
		[EnumMember(Value = "all")]
		All,
		[EnumMember(Value = "none")]
		None
	}
}