using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum LicenseStatus
	{
		[EnumMember(Value = "active")]
		Active,

		[EnumMember(Value = "valid")]
		Valid,

		[EnumMember(Value = "invalid")]
		Invalid,

		[EnumMember(Value = "expired")]
		Expired
	}
}
