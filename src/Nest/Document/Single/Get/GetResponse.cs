using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetResponse<out TDocument> : IResponse where TDocument : class
	{
		[JsonProperty("fields")]
		FieldValues Fields { get; }

		[JsonProperty("found")]
		bool Found { get; }

		[JsonProperty("_id")]
		string Id { get; }

		[JsonProperty("_index")]
		string Index { get; }

		[JsonProperty("_parent")]
		[Obsolete("No longer returned on indices created in Elasticsearch 6.0")]
		string Parent { get; }

		[JsonProperty("_routing")]
		string Routing { get; }

		[JsonProperty("_source")]
		[JsonConverter(typeof(SourceConverter))]
		TDocument Source { get; }

		[JsonProperty("_type")]
		string Type { get; }

		[JsonProperty("_version")]
		long Version { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GetResponse<TDocument> : ResponseBase, IGetResponse<TDocument> where TDocument : class
	{
		public FieldValues Fields { get; internal set; } = FieldValues.Empty;
		public bool Found { get; internal set; }
		public string Id { get; internal set; }
		public string Index { get; internal set; }
		public string Parent { get; internal set; }
		public string Routing { get; internal set; }
		public TDocument Source { get; internal set; }
		public string Type { get; internal set; }
		public long Version { get; internal set; }
	}
}
