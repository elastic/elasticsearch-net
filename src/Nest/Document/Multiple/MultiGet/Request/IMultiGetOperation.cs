using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MultiGetOperationDescriptor<object>>))]
	public interface IMultiGetOperation
	{
		[JsonProperty("_index")]
		IndexName Index { get; set; }

		[JsonProperty("_type")]
		TypeName Type { get; set; }

		[JsonProperty("_id")]
		Id Id { get; set; }

		[JsonProperty("stored_fields")]
		Fields StoredFields { get; set; }

		[JsonProperty("_routing")]
		string Routing { get; set; }

		[JsonProperty("_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		Type ClrType { get; }

		bool CanBeFlattened { get; }
	}
}
