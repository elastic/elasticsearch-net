using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    public interface IPercolateResponse : IResponse
    {
        bool OK { get; }
        IEnumerable<string> Matches { get; }
    }

    [JsonObject]
	public class PercolateResponse : BaseResponse, IPercolateResponse
    {
		public PercolateResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }
		[JsonProperty(PropertyName = "matches")]
		public IEnumerable<string> Matches { get; internal set; }
	}
}