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

		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 6.x and up, use Routing instead.")]
		string Parent { get; }

		string Routing { get; }

		Error Error { get; }
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
		public Error Error { get; internal set; }
	}
}
