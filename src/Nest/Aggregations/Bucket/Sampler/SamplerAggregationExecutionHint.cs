using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum SamplerAggregationExecutionHint
	{
		[EnumMember(Value = "map")]
		Map,

		[EnumMember(Value = "global_ordinals")]
		GlobalOrdinals,

		[EnumMember(Value = "bytes_hash")]
		BytesHash
	}
}
