using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiGetHit<out TDocument> where TDocument : class
	{
		TDocument Source { get; }

		string Index { get; }

		bool Found { get; }

		string Type { get; }

		long Version { get; }

		string Id { get; }

		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 6.x and up")]
		string Parent { get; }

		string Routing { get; }

		ServerError Error { get; }

		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 5.x and up")]
		long? Timestamp { get; }

		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 5.x and up")]
		long? Ttl { get; }
	}

	[JsonObject]
	public class MultiGetHit<TDocument> : IMultiGetHit<TDocument>
		where TDocument : class
	{
		public FieldValues Fields { get; internal set; }

		[JsonProperty("_source")]
		[JsonConverter(typeof(SourceConverter))]
		public TDocument Source { get; internal set; }

		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("found")]
		public bool Found { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_version")]
		public long Version { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_parent")]
		public string Parent { get; internal set; }

		[JsonProperty("_routing")]
		public string Routing { get; internal set; }

		[JsonProperty("error")]
		public ServerError Error { get; internal set; }

		[JsonProperty("_timestamp")]
		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 5.x and up")]
		public long? Timestamp { get; internal set; }

		[JsonProperty("_ttl")]
		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 5.x and up")]
		public long? Ttl { get; internal set; }
	}
}
