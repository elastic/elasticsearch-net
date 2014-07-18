using System.Collections.Generic;
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
		public PercolateCountResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "took")]
		public int Took { get; internal set; }
		[JsonProperty(PropertyName = "total")]
		public long Total { get; internal set; }
	
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
	}
}