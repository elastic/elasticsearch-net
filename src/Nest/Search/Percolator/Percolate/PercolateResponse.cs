using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
	public interface IPercolateCountResponse : IResponse
	{
		long Took { get; }
		long Total { get; }
	}

	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
	public interface IPercolateResponse : IPercolateCountResponse
	{
		IReadOnlyCollection<PercolatorMatch> Matches { get; }
	}

	[JsonObject]
	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
	public class PercolateCountResponse : ResponseBase, IPercolateCountResponse
	{
		public override ServerError ServerError => Error ?? base.ServerError;

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "took")]
		public long Took { get; internal set; }

		[JsonProperty(PropertyName = "total")]
		public long Total { get; internal set; }

		/// <summary>
		/// The individual error for separate requests on the _mpercolate API
		/// </summary>
		[JsonProperty(PropertyName = "error")]
		internal ServerError Error { get; set; }
	}

	[JsonObject]
	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
	public class PercolateResponse : PercolateCountResponse, IPercolateResponse
	{
		[JsonProperty(PropertyName = "matches")]
		public IReadOnlyCollection<PercolatorMatch> Matches { get; internal set; } = EmptyReadOnly<PercolatorMatch>.Collection;
	}

	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
	public class PercolatorMatch
	{
		[JsonProperty(PropertyName = "highlight")]
		public IReadOnlyDictionary<string, IList<string>> Highlight { get; internal set; } = EmptyReadOnly<string, IList<string>>.Dictionary;

		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }

		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }

		[JsonProperty(PropertyName = "_score")]
		public double Score { get; internal set; }
	}
}
