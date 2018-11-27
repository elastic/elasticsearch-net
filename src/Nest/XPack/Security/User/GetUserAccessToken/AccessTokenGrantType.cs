using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum AccessTokenGrantType
	{
		[EnumMember(Value = "password")] Password
	}
}
