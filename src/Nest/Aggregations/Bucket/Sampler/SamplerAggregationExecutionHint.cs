using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
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
