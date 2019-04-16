using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum PhoneticRuleType
	{
		[EnumMember(Value = "approx")]
		Approximate,

		[EnumMember(Value = "exact")]
		Exact
	}
}
