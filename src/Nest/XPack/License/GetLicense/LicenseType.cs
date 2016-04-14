using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum LicenseType
	{
		[EnumMember(Value="trial")]
		Trial,
		[EnumMember(Value="basic")]
		Basic,
		[EnumMember(Value="silver")]
		Silver,
		[EnumMember(Value="dev")]
		Dev,
		[EnumMember(Value="gold")]
		Gold,
		[EnumMember(Value="platinum")]
		Platinum
	}
}
