using System;
using Elasticsearch.Net;
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

		[JsonProperty("routing")]
		string Routing { get; set; }

		[JsonProperty("_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[JsonProperty("version")]
		long? Version { get; set; }

		[JsonProperty("version_type")]
		VersionType? VersionType { get; set; }

		Type ClrType { get; }

		bool CanBeFlattened { get; }
	}
}
