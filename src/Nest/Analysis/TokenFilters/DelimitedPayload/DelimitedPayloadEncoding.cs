using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum DelimitedPayloadEncoding
	{
		[EnumMember(Value = "int")]
		Integer,

		[EnumMember(Value = "float")]
		Float,

		[EnumMember(Value = "identity")]
		Identity,
	}
}
