using Newtonsoft.Json;

namespace Nest
{
    public interface IUnregisterPercolateResponse : IResponse
    {
        bool OK { get; }
        bool Found { get; }
        string Index { get; }
        string Type { get; }
        string Id { get; }
        int Version { get; }
    }

    [JsonObject]
	public class UnregisterPercolateResponse : BaseResponse, IUnregisterPercolateResponse
    {
		public UnregisterPercolateResponse()
		{
			this.IsValid = true;
		}
		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }
		
		[JsonProperty(PropertyName = "found")]
		public bool Found { get; internal set; }

		//todo: change mapping slightly see #ES-1518

		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }

		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }

		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }

		[JsonProperty(PropertyName = "_version")]
		public int Version { get; internal set; }
	}
}