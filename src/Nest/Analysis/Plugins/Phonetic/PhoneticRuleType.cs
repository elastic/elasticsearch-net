using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum PhoneticRuleType
	{
		[EnumMember(Value = "approx")]
		Approximate,

		[EnumMember(Value = "exact")]
		Exact
	}
}
