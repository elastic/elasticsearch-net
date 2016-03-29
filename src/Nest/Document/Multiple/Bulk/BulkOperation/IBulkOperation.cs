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

		[JsonProperty(PropertyName = "_index")]
		IndexName Index { get; set; }

		[JsonProperty(PropertyName = "_type")]
		TypeName Type { get; set; }

		[JsonProperty(PropertyName = "_id")]
		Id Id { get; set; }

		[JsonProperty(PropertyName = "_version")]
		long? Version { get; set; }

		[JsonProperty(PropertyName = "_version_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		VersionType? VersionType { get; set; }

		[JsonProperty(PropertyName = "_routing")]
		string Routing { get; set; }

		[JsonProperty(PropertyName = "_parent")]
		Id Parent { get; set; }

		[JsonProperty("_timestamp")]
		long? Timestamp { get; set; }

		[JsonProperty("_ttl")]
		Time Ttl { get; set; }

		[JsonProperty("_retry_on_conflict")]
		int? RetriesOnConflict { get; set; }

		object GetBody();

		Id GetIdForOperation(Inferrer inferrer);
	}
}