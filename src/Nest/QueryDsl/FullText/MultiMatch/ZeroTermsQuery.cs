using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum ZeroTermsQuery
	{
		[EnumMember(Value = "all")]
		All,

		[EnumMember(Value = "none")]
		None
	}
}
