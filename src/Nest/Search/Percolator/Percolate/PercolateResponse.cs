using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPercolateCountResponse : IResponse
	{
		int Took { get; }
		long Total { get; }
	}

	public interface IPercolateResponse : IPercolateCountResponse
	{
		IEnumerable<PercolatorMatch> Matches { get; }
	}

	[JsonObject]
	public class PercolateCountResponse : BaseResponse, IPercolateCountResponse
	{

		[JsonProperty(PropertyName = "took")]
		public int Took { get; internal set; }
		[JsonProperty(PropertyName = "total")]
		public long Total { get; internal set; }
		
		/// <summary>
		/// The individual error for separate requests on the _mpercolate API
		/// </summary>
		[JsonProperty(PropertyName = "error")]
		internal string Error { get; set; }
	}

	[JsonObject]
	public class PercolateResponse : PercolateCountResponse, IPercolateResponse
	{

		[JsonProperty(PropertyName = "matches")]
		public IEnumerable<PercolatorMatch> Matches { get; internal set; }
	}

	public class PercolatorMatch
	{
		[JsonProperty(PropertyName = "_index")]
		public string Index { get; set; }
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; set; }
		[JsonProperty(PropertyName = "highlight")]
		public Dictionary<string, IList<string>> Highlight { get; set; }
		
	}

}