using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
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
