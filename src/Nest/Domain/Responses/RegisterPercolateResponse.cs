using Newtonsoft.Json;

namespace Nest
{
    public interface IRegisterPercolateResponse : IResponse
    {
        bool OK { get; }
        string Index { get; }
        string Type { get; }
        string Id { get; }
        int Version { get; }
    }

    [JsonObject]
	public class RegisterPercolateResponse : BaseResponse, IRegisterPercolateResponse
    {
		public RegisterPercolateResponse()
		{
			this.IsValid = true;
		}
		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }

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