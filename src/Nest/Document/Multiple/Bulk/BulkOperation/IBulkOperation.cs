using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
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

		[JsonProperty("version")]
		long? Version { get; set; }

		[JsonProperty("version_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		VersionType? VersionType { get; set; }

		[JsonProperty("routing")]
		Routing Routing { get; set; }

		[JsonProperty("parent")]
		Id Parent { get; set; }

		[JsonProperty("_timestamp")]
		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 5.0.0 and up")]
		long? Timestamp { get; set; }

		[JsonProperty("_ttl")]
		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 5.0.0 and up")]
		Time Ttl { get; set; }

		[JsonProperty("retry_on_conflict")]
		int? RetriesOnConflict { get; set; }

		object GetBody();

		Id GetIdForOperation(Inferrer inferrer);
		Routing GetRoutingForOperation(Inferrer settingsInferrer);
	}
}
