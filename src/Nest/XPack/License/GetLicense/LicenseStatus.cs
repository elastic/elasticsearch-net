using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
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
