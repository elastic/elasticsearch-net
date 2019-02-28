using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum PhoneticNameType
	{
		[EnumMember(Value = "generic")]
		Generic,

		[EnumMember(Value = "ashkenazi")]
		Ashkenazi,

		[EnumMember(Value = "sephardic")]
		Sephardic
	}
}
