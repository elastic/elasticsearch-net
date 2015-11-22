using Newtonsoft.Json;

namespace Nest
{
	// TODO @Mpdreamz should this be replaced by IAcknowledResponse?
	public interface IIndicesOperationResponse : IResponse
	{
		bool Acknowledged { get; }
	}

	[JsonObject]
	public class IndicesOperationResponse : BaseResponse, IIndicesOperationResponse
	{
		[JsonProperty(PropertyName = "acknowledged")]
		public bool Acknowledged { get; internal set; }
	}
}