using Newtonsoft.Json;

namespace Nest
{
    public interface IIndicesOperationResponse : IResponse
    {
        bool Acknowledged { get; }
    }

    [JsonObject]
	public class IndicesOperationResponse : BaseResponse, IIndicesOperationResponse
    {
		public IndicesOperationResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "acknowledged")]
		public bool Acknowledged { get; internal set; }
	}
}