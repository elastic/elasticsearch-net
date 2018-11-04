using System;
using Newtonsoft.Json;

namespace Nest
{
	[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
	public interface IRegisterPercolatorResponse : IResponse
	{
		bool Created { get; }
		string Id { get; }
		string Index { get; }
		string Type { get; }
		int Version { get; }
	}

	[JsonObject]
	[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
	public class RegisterPercolatorResponse : ResponseBase, IRegisterPercolatorResponse
	{
		[JsonProperty(PropertyName = "created")]
		public bool Created { get; internal set; }

		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }

		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }

		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }

		[JsonProperty(PropertyName = "_version")]
		public int Version { get; internal set; }
	}
}
