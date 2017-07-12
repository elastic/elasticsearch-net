using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum AccessTokenGrantType
	{
		[EnumMember(Value = "password")] Password
	}
}
