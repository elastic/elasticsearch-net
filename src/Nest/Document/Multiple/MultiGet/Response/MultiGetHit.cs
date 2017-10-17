using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiGetHit<out T> where T : class
	{
		T Source { get; }

		string Index { get; }

		bool Found { get; }

		string Type { get; }

		long Version { get; }

		string Id { get; }

		string Parent { get; }

		string Routing { get; }

		ServerError Error { get; }

		long? Timestamp { get; }

		long? Ttl { get; }
	}

	[JsonObject]
	public class MultiGetHit<T> : IMultiGetHit<T>
		where T : class
	{
		public FieldValues Fields { get; internal set; }

		[JsonProperty("_source")]
		public T Source { get; internal set; }

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
		public long? Timestamp { get; internal set; }

		[JsonProperty("_ttl")]
		public long? Ttl { get; internal set; }
	}
}
