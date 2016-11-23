using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System;

namespace Nest
{
	public interface IPercolateCountResponse : IResponse
	{
		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[Obsolete(@"returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
		int Took { get; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		long TookAsLong { get; }

		long Total { get; }
	}

	public interface IPercolateResponse : IPercolateCountResponse
	{
		IEnumerable<PercolatorMatch> Matches { get; }
	}

	[JsonObject]
	public class PercolateCountResponse : ResponseBase, IPercolateCountResponse
	{
		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[JsonProperty("took")]
		public long TookAsLong { get; internal set; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[Obsolete(@"returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
		[JsonIgnore]
		public int Took => TookAsLong > int.MaxValue? int.MaxValue : (int)TookAsLong;

		[JsonProperty("total")]
		public long Total { get; internal set; }

		[JsonProperty("_shards")]
		public ShardsMetaData Shards { get; internal set; }

		/// <summary>
		/// The individual error for separate requests on the _mpercolate API
		/// </summary>
		[JsonProperty("error")]
		internal ServerError Error { get; set; }

		public override ServerError ServerError => this.Error ?? base.ServerError;
	}

	[JsonObject]
	public class PercolateResponse : PercolateCountResponse, IPercolateResponse
	{
		[JsonProperty("matches")]
		public IEnumerable<PercolatorMatch> Matches { get; internal set; }
	}

	public class PercolatorMatch
	{
		[JsonProperty("highlight")]
		public Dictionary<string, IList<string>> Highlight { get; set; }

		[JsonProperty("_id")]
		public string Id { get; set; }

		[JsonProperty("_index")]
		public string Index { get; set; }

		[JsonProperty("_score")]
		public double Score { get; set; }
	}

}
