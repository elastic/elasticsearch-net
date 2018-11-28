using System.Runtime.Serialization;
using System.Runtime.Serialization;


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
