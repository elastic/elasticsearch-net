using System;
using Elasticsearch.Net_5_2_0;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest_5_2_0
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBulkOperation
	{
		string Operation { get; }
		Type ClrType { get; }

		[JsonProperty("_index")]
		IndexName Index { get; set; }

		[JsonProperty("_type")]
		TypeName Type { get; set; }

		[JsonProperty("_id")]
		Id Id { get; set; }

		[JsonProperty("_version")]
		long? Version { get; set; }

		[JsonProperty("_version_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		VersionType? VersionType { get; set; }

		[JsonProperty("_routing")]
		string Routing { get; set; }

		[JsonProperty("_parent")]
		Id Parent { get; set; }

		[JsonProperty("_timestamp")]
		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 5.0.0 and up")]
		long? Timestamp { get; set; }

		[JsonProperty("_ttl")]
		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 5.0.0 and up")]
		Time Ttl { get; set; }

		[JsonProperty("_retry_on_conflict")]
		int? RetriesOnConflict { get; set; }

		object GetBody();

		Id GetIdForOperation(Inferrer inferrer);
	}
}
