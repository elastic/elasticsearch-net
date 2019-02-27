using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBulkOperation
	{
		Type ClrType { get; }

		[JsonProperty("_id")]
		Id Id { get; set; }

		[JsonProperty("_index")]
		IndexName Index { get; set; }

		string Operation { get; }

		[JsonProperty("parent")]
		Id Parent { get; set; }

		[JsonProperty("retry_on_conflict")]
		int? RetriesOnConflict { get; set; }

		[JsonProperty("routing")]
		Routing Routing { get; set; }

		[JsonProperty("version")]
		long? Version { get; set; }

		[JsonProperty("version_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		VersionType? VersionType { get; set; }

		object GetBody();

		Id GetIdForOperation(Inferrer inferrer);

		Routing GetRoutingForOperation(Inferrer settingsInferrer);
	}
}
