using Newtonsoft.Json;

namespace Nest
{
    public interface IIndicesOperationResponse : IResponse
    {
        bool OK { get; }
        bool Acknowledged { get; }
    }

    [JsonObject]
	public class IndicesOperationResponse : BaseResponse, IIndicesOperationResponse
    {
		public IndicesOperationResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }

		[JsonProperty(PropertyName = "acknowledged")]
		public bool Acknowledged { get; internal set; }
	}
}