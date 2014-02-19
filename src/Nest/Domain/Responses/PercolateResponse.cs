using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPercolateResponse : IResponse
	{
		int Took { get; }
		long Total { get; }
		IEnumerable<PercolatorMatch> Matches { get; }
	}

	[JsonObject]
	public class PercolateResponse : BaseResponse, IPercolateResponse
	{
		public PercolateResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "took")]
		public int Took { get; internal set; }
		[JsonProperty(PropertyName = "total")]
		public long Total { get; internal set; }
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